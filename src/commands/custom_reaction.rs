use std::time::Duration;

use serenity::{
    builder::{CreateEmbed, CreateEmbedFooter},
    client::Context,
    framework::standard::{
        macros::{command, group},
        Args, CommandResult,
    },
    model::{
        channel::{Message, ReactionType},
        guild::Guild,
    },
};

use crate::{
    database::{
        functions::custom_reaction::{
            add_custom_reaction, count_custom_reactions, list_custom_reaction,
            remove_custom_reaction,
        },
        models::custom_reaction::{DbCustomReaction, DbCustomReactionType},
    },
    utils::constants::colors,
};

#[group]
#[commands(acr, aecr, dcr, lcr)]
#[description("Reações Customizadas 💬- Esse módulo possui comandos para você controlar as minhas Reações Customizadas")]
pub struct CustomReaction;

#[command("adicionarrc")]
#[aliases("acr", "adicionarcr", "arc")]
#[only_in("guilds")]
#[min_args(3)]
#[required_permissions("MANAGE_GUILD")]
#[description("Adiciona uma reação customizada ao servidor\n\n(Observação: você precisa ter a permissão de gerenciar servidor)")]
#[usage("acr <mensagem> | <resposta>")]
#[example("acr oi | olá")]
async fn acr(ctx: &Context, msg: &Message, args: Args) -> CommandResult {
    let args = args.remains().unwrap().split('|').collect::<Vec<&str>>();

    if args.len() != 2 {
        return Ok(());
    }

    let guild = msg.guild(ctx).await.unwrap();

    create_custom_reaction(
        guild,
        args[0].trim_end().to_string(),
        args[1].trim_start().to_string(),
        DbCustomReactionType::Normal,
    )
    .await?;

    let mut embed = CreateEmbed::default();
    embed.title("Reação customizada adicionada com sucesso 😃");
    embed.color(colors::ORANGE);

    msg.channel_id
        .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
        .await?;

    Ok(())
}

#[command("adicionarrcespecial")]
#[aliases("aecr", "arce", "adicionarcrespecial")]
#[only_in("guilds")]
#[min_args(3)]
#[required_permissions("MANAGE_GUILD")]
#[description("Adiciona uma reação customizada especial ao servidor\n\n(Observação: você precisa ter a permissão de gerenciar servidor)")]
#[usage("aecr <mensagem> | <resposta>")]
#[example("aecr oi | olá")]
async fn aecr(ctx: &Context, msg: &Message, args: Args) -> CommandResult {
    let args = args.remains().unwrap().split('|').collect::<Vec<&str>>();

    if args.len() != 2 {
        return Ok(());
    }

    let guild = msg.guild(ctx).await.unwrap();

    create_custom_reaction(
        guild,
        args[0].trim_end().to_string(),
        args[1].trim_start().to_string(),
        DbCustomReactionType::Especial,
    )
    .await?;

    let mut embed = CreateEmbed::default();
    embed.title("Reação customizada especial adicionada com sucesso 😃");
    embed.color(colors::ORANGE);

    msg.channel_id
        .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
        .await?;

    Ok(())
}

async fn create_custom_reaction(
    guild: Guild,
    question: String,
    reply: String,
    cr_type: DbCustomReactionType,
) -> CommandResult {
    add_custom_reaction(guild, question, reply, cr_type).await?;

    Ok(())
}

#[command("deletecr")]
#[aliases("dcr")]
#[only_in("guilds")]
#[min_args(1)]
#[max_args(1)]
#[required_permissions("MANAGE_GUILD")]
#[description("Remove uma reação customizada especifica\n\n(Observação: você precisa ter a permissão de gerenciar servidor)")]
#[usage("dcr <id da reação>")]
#[example("dcr 1258")]
async fn dcr(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let id = args.single::<u32>()?;

    let guild = msg.guild(ctx).await.unwrap();

    let mut embed = CreateEmbed::default();
    if remove_custom_reaction(guild, id).await? {
        embed.title("Reação customizada removida com sucesso 😊");
        embed.color(colors::ORANGE);

        msg.channel_id
            .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
            .await?;
    } else {
        embed.title("Reação customizada não encontrada 😔");
        embed.color(colors::YELLOW);

        msg.channel_id
            .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
            .await?;
    }

    Ok(())
}

#[command("listrc")]
#[aliases("lcr", "listarrc", "listcr")]
#[only_in("guilds")]
#[description("Lista as reações customizadas")]
#[usage("lcr [pesquisa]")]
#[example("lcr")]
#[example("lcr oi")]
async fn lcr(ctx: &Context, msg: &Message, args: Args) -> CommandResult {
    let find = args.remains().unwrap_or("");

    let guild = msg.guild(ctx).await.unwrap();
    let mut page = 0;

    let mut message_cache: Option<Message> = None;

    let build_embed = |page: u8, custom_reactions: &Vec<DbCustomReaction>| {
        let mut embed = CreateEmbed::default();
        embed.title("Lista de reações customizadas");
        embed.color(colors::ORANGE);
        embed.field(
            format!("Página {}", page + 1),
            format!(
                "```md\n{}\n```",
                custom_reactions.iter().fold("".to_string(), |result, cr| {
                    format!("{}\n{}", result, cr.format())
                })
            ),
            false,
        );

        let mut embed_footer = CreateEmbedFooter::default();
        embed_footer.text("As reações customizadas marcadas *assim* são as especiais");

        embed.set_footer(embed_footer);

        embed
    };

    let crs_count = count_custom_reactions(&guild, find).await?;
    let mut edit_page = true;
    let mut custom_reactions;

    loop {
        if edit_page {
            custom_reactions = list_custom_reaction(&guild, find, page).await?;

            match message_cache.as_mut() {
                Some(message) => {
                    message
                        .edit(ctx, |x| x.set_embed(build_embed(page, &custom_reactions)))
                        .await?;
                }
                None => {
                    if custom_reactions.is_empty() {
                        msg.channel_id
                            .send_message(ctx, |x| {
                                x.reference_message(msg).embed(|e| {
                                    e.color(colors::ORANGE)
                                        .title("Não há nenhuma reação customizada.")
                                })
                            })
                            .await?;
                        return Ok(());
                    }
                    let message = msg
                        .channel_id
                        .send_message(ctx, |x| {
                            x.reference_message(msg)
                                .set_embed(build_embed(page, &custom_reactions))
                        })
                        .await?;

                    message
                        .react(ctx, ReactionType::Unicode("◀".into()))
                        .await
                        .ok();
                    message
                        .react(ctx, ReactionType::Unicode("▶".into()))
                        .await
                        .ok();

                    message_cache = Some(message);
                }
            }

            edit_page = false;
        }

        let message = message_cache.as_mut().unwrap();

        let collector = message
            .await_reaction(ctx)
            .timeout(Duration::from_secs(30))
            .author_id(msg.author.id)
            .removed(true)
            .await;

        match collector {
            Some(reaction) => {
                let reaction = reaction.as_inner_ref();

                if reaction.emoji.as_data() == "▶" && crs_count > ((page + 1) * 10) as u32 {
                    page += 1;
                    edit_page = true;
                } else if reaction.emoji.as_data() == "◀" && page > 0 {
                    page -= 1;
                    edit_page = true;
                }
            }
            None => {
                message.delete_reactions(ctx).await.ok();
                break;
            }
        }
    }

    Ok(())
}
