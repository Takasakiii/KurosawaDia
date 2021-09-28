mod about;
mod config;
mod custom_reaction;
mod image;
mod moderation;
mod nsfw;
mod owner;
mod util;
mod weeb;

use std::collections::HashSet;

use chrono::{SecondsFormat, Utc};
use rand::{thread_rng, Rng};
use serenity::{
    builder::{CreateEmbed, CreateEmbedFooter},
    client::Context,
    framework::{
        standard::{
            macros::{help, hook},
            Args, CommandGroup, CommandResult, DispatchError, HelpOptions,
        },
        StandardFramework,
    },
    model::{channel::Message, id::UserId},
};
use tokio::spawn;

use crate::{
    components::embed::{Embed, IsEmbed},
    config::KurosawaConfig,
    database::{
        functions::{
            custom_reaction::get_custom_reaction,
            guild::{get_db_guild, register_guild},
        },
        models::guild::DbGuildType,
    },
    errors::error_permission,
    utils::constants::colors,
};

pub fn crete_framework() -> StandardFramework {
    StandardFramework::new()
        .configure(|x| {
            x.dynamic_prefix(move |ctx, msg| {
                Box::pin(async move {
                    if let Some(guild) = msg.guild(ctx).await {
                        if let Ok(db_guild) = get_db_guild(guild).await {
                            return Some(db_guild.prefix);
                        }
                    }
                    Some(KurosawaConfig::get_default_prefix())
                })
            })
            .prefix("")
            .on_mention(Some(KurosawaConfig::get_id_mention()))
            .no_dm_prefix(true)
            .case_insensitivity(true)
            .owners(vec![UserId(203713369927057408)].into_iter().collect())
        })
        .group(&util::UTIL_GROUP)
        .group(&moderation::MODERATION_GROUP)
        .group(&weeb::WEEB_GROUP)
        .group(&config::CONFIG_GROUP)
        .group(&image::IMAGE_GROUP)
        .group(&nsfw::NSFW_GROUP)
        .group(&about::ABOUT_GROUP)
        .group(&owner::OWNER_GROUP)
        .group(&custom_reaction::CUSTOMREACTION_GROUP)
        .before(before_command)
        .after(after_command)
        .normal_message(normal_message)
        .on_dispatch_error(dispatch_error)
        .help(&HELP)
}

#[help]
async fn help(
    ctx: &Context,
    msg: &Message,
    mut args: Args,
    _: &'static HelpOptions,
    groups: &[&'static CommandGroup],
    _: HashSet<UserId>,
) -> CommandResult {
    if args.is_empty() {
        let mut embed = CreateEmbed::default();
        embed.title("Comandos atacaaaaar 😁");
        embed.description("Para mais informações sobre um módulo ou comando, digite `help {Comando}` que eu lhe informarei mais sobre ele");
        embed.image("https://i.imgur.com/mQVFSrP.gif");
        embed.color(colors::PURPLE);

        for group in groups.iter() {
            if !group.options.help_available {
                continue;
            }

            let group_description = group.options.description.unwrap_or(group.name);
            let group_cmds = group.options.commands;

            let mut group_cmds_name = "".to_string();

            for cmd in group_cmds.iter() {
                if cmd.options.help_available {
                    group_cmds_name
                        .push_str(format!(" `{}`", cmd.options.names.first().unwrap()).as_str());
                }
            }

            embed.field(group_description, group_cmds_name, false);
        }

        msg.channel_id
            .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
            .await?;
    } else {
        let cmd_name = args.single::<String>()?;

        let prefix = if let Some(guild) = msg.guild(ctx).await {
            if let Ok(db_guild) = get_db_guild(guild).await {
                db_guild.prefix
            } else {
                KurosawaConfig::get_default_prefix()
            }
        } else {
            "".to_string()
        };

        let mut embed = CreateEmbed::default();
        embed.color(colors::PURPLE);

        if cmd_name == "help" {
            embed.image("https://i.imgur.com/vg0z9yT.jpg");
            embed.title("Mais informações para help");
            embed.description("Com esse comando eu posso te fornecer informações, como se comunicar comigo e as tarefas que realizo");
            embed.field("Uso", format!("`{0}help <comando>*`", prefix), false);
            embed.field(
                "Exemplos",
                format!("`{0}help prefix`\n`{0}help`", prefix),
                false,
            );
        } else {
            let mut cmd = None;

            for group in groups.iter() {
                for cmds in group.options.commands.iter() {
                    if cmds.options.names.iter().any(|x| x == &cmd_name) {
                        cmd = Some(cmds);
                    }
                }
            }

            match cmd {
                None => {
                    embed.title("Comando não encontrado");
                }
                Some(cmd) => {
                    if !cmd.options.help_available {
                        let db_guild = if let Some(guild) = msg.guild(ctx).await {
                            if let Ok(guild) = get_db_guild(guild).await {
                                Some(guild)
                            } else {
                                None
                            }
                        } else {
                            None
                        };

                        match db_guild {
                            Some(db_guild) => {
                                if db_guild.guild_type == DbGuildType::Normal as u32 {
                                    return Ok(());
                                }
                            }
                            None => {
                                return Ok(());
                            }
                        }
                    }

                    let images = vec![
                        "https://i.imgur.com/vg0z9yT.jpg",
                        "https://i.imgur.com/AUpMkBP.jpg",
                    ];

                    let random = thread_rng().gen_range(0..images.len());

                    embed.image(images[random]);
                    embed.title(format!("Mais informações para {}", cmd.options.names[0]));
                    let mut footer = CreateEmbedFooter::default();
                    footer.text("<> indica que é obrigatório, [] indica que é opcional");
                    embed.set_footer(footer);
                    if let Some(description) = cmd.options.desc {
                        embed.description(description);
                    }
                    if cmd.options.names.len() > 1 {
                        let aliases = cmd
                            .options
                            .names
                            .iter()
                            .skip(1)
                            .fold("".to_string(), |result, item| {
                                format!("{}\n- {}", result, item)
                            });
                        embed.field("Outros jeitos que eu entendo", aliases, false);
                    }
                    if let Some(usage) = cmd.options.usage {
                        embed.field("Uso", format!("`{}{}`", prefix, usage), false);
                    }
                    if !cmd.options.examples.is_empty() {
                        let examples = cmd
                            .options
                            .examples
                            .iter()
                            .fold("".to_string(), |result, item| {
                                format!("{}\n`{}{}`", result, prefix, item)
                            });
                        embed.field(
                            if cmd.options.examples.len() > 1 {
                                "Exemplos"
                            } else {
                                "Exemplo"
                            },
                            examples,
                            false,
                        );
                    }
                }
            };
        }

        msg.channel_id
            .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
            .await?;
    }

    Ok(())
}

#[hook]
async fn dispatch_error(ctx: &Context, msg: &Message, err: DispatchError) {
    match err {
        DispatchError::LackingPermissions(permissions) => {
            error_permission(ctx, msg, permissions).await
        }
        _ => return,
    }
}

#[hook]
async fn normal_message(ctx: &Context, msg: &Message) {
    if let Some(guild) = msg.guild(ctx).await {
        let content = &msg.content;

        match get_custom_reaction(guild, content).await {
            Ok(Some(cr)) => {
                let is_embed = Embed::from_str(ctx, msg, &cr.reply).await;
                match is_embed {
                    IsEmbed::Embed(embed, result) => {
                        let msg_send = msg
                            .channel_id
                            .send_message(ctx, move |x| {
                                if let Some(text) = &embed.plain_text {
                                    x.content(text);
                                }
                                x.set_embed(embed.into())
                            })
                            .await;

                        if msg_send.is_err() {
                            msg.channel_id
                                .send_message(ctx, |x| x.content(result))
                                .await
                                .ok();
                        }
                    }
                    IsEmbed::Message(text) => {
                        msg.channel_id
                            .send_message(ctx, |x| x.content(text))
                            .await
                            .ok();
                    }
                }
            }
            Err(err) => {
                println!("{:?}", err)
            }
            _ => {}
        }
    }
}

#[hook]
async fn before_command(ctx: &Context, msg: &Message, name: &str) -> bool {
    match msg.guild_id {
        Some(guild_id) => {
            let guild = guild_id.to_guild_cached(ctx).await;
            let thread = spawn(async move {
                if let Some(guild) = guild {
                    register_guild(guild).await
                } else {
                    Err("Falha em pegar a guild".into())
                }
            });

            if name == "prefix" || name == "loli" {
                return match thread.await {
                    Ok(result) => result.is_ok(),
                    Err(_) => false,
                };
            }

            true
        }
        None => true,
    }
}

#[hook]
async fn after_command(ctx: &Context, msg: &Message, name: &str, why: CommandResult) {
    if let Err(why) = why {
        let date = Utc::now();

        println!(
            "Time: {} User: {} Command: {} Error: {:?}",
            date.to_rfc3339_opts(SecondsFormat::Secs, false),
            msg.author.tag(),
            name,
            why
        );
        let _ = msg.react(ctx, '❌').await;

        // let api = get_violet_api();
        // if api.send_error(VioletError::error(why, name)).await.is_err() {
        //     eprintln!("Falha ao enviar o erro para a violet")
        // }
    }
}
