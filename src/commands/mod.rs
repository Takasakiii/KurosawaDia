mod config;
mod util;
mod moderation;
mod weeb;
mod image;
mod nsfw;
mod about;
mod owner;
mod custom_reaction;

use std::collections::HashSet;

use chrono::{SecondsFormat, Utc};
use serenity::{builder::CreateEmbed, client::Context, framework::{StandardFramework, standard::{Args, CommandGroup, CommandResult, DispatchError, HelpOptions, macros::{hook, help}}}, model::{channel::Message, id::UserId}};
use tokio::spawn;

use crate::{apis::{get_violet_api, violet::data_error::VioletError}, config::{get_default_prefix, get_id_mention}, database::functions::{custom_reaction::get_custom_reaction, guild::{get_db_guild, register_guild}}, utils::constants::colors};

pub fn crete_framework() -> StandardFramework {
    StandardFramework::new()
        .configure(|x| x
            .dynamic_prefix(|ctx, msg| Box::pin(async move {
                if let Some(guild) = msg.guild(ctx).await {
                    if let Ok(db_guild) = get_db_guild(guild).await {
                        return Some(db_guild.prefix);
                    }
                }
                Some(get_default_prefix())
            }))
            .prefix("")
            .on_mention(get_id_mention())
            .no_dm_prefix(true)
            .case_insensitivity(true)
            .owners(vec![
                UserId(203713369927057408)
            ].into_iter().collect())
        )
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
    _: HashSet<UserId>
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
            let group_cmds = group.options.commands
                .iter()
                .map(|cmds| cmds.options.names.first().unwrap())
                .fold("".to_string(), |init, item|
                    format!("{} `{}`", init, item)
                );

            embed.field(group_description, group_cmds, false);
        }

        msg.channel_id.send_message(ctx, |x| x
            .set_embed(embed)
            .reference_message(msg)
        ).await?;
    } else {
        let cmd_name = args.single::<String>()?;

        let prefix = if let Some(guild) = msg.guild(ctx).await {
            if let Ok(db_guild) = get_db_guild(guild).await {
                db_guild.prefix
            } else {
                get_default_prefix()
            }
        } else {
            get_default_prefix()
        };

        let mut embed = CreateEmbed::default();
        embed.color(colors::PURPLE);

        if cmd_name == "help" {

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
                },
                Some(cmd) => {
                    embed.image("https://i.imgur.com/vg0z9yT.jpg");
                    embed.title(format!("Mais informações para {}", cmd.options.names[0]));
                    if let Some(description) = cmd.options.desc {
                        embed.description(description);
                    }
                    if let Some(usage) = cmd.options.usage {
                        embed.field("Uso", format!("`{}{}`", prefix, usage), false);
                    }
                    if !cmd.options.examples.is_empty() {
                        let examples = cmd.options.examples.iter().fold("".to_string(), |result, item|
                            format!("{}\n`{}{}`", result, prefix, item)
                        );
                        embed.field("Exemplos", examples, false);
                    }
                }
            };
        }

        msg.channel_id.send_message(ctx, |x| x
            .set_embed(embed)
            .reference_message(msg)
        ).await?;
    }

    Ok(())
}

#[hook]
async fn dispatch_error(_ctx: &Context, _msg: &Message, _err: DispatchError) {
}

#[hook]
async fn normal_message(ctx: &Context, msg: &Message) {
    if let Some(guild) = msg.guild(ctx).await {
        let content = &msg.content;

        match get_custom_reaction(guild, content).await {
            Ok(Some(cr)) => {
                msg.channel_id.send_message(ctx, |x| x
                    .content(cr.reply)
                ).await.ok();
            },
            Err(err) => {
                println!("{:?}", err)
            },
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
                    Ok(result) => {
                        result.is_ok()
                    },
                    Err(_) => false
                };
            }

            true
        },
        None => {
            true
        }
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
            why);
        let _ = msg.react(ctx, '❌').await;

        let api = get_violet_api();
        if api.send_error(VioletError::error(why, name)).await.is_err() {
            print!("Falha ao enviar o erro para a violet")
        }
    }
}
