use serenity::{
    builder::CreateEmbed,
    client::Context,
    framework::standard::{
        macros::{command, group},
        Args, CommandResult,
    },
    model::{channel::Message, prelude::User},
};

use crate::utils::{
    constants::colors,
    user::{get_user_from_args, get_user_role_position},
};

#[group]
#[commands(limpar_chat, ban, kick, softban)]
#[description("Moderação ⚖️- Esse módulo possui coisas para te ajudar a moderar seu servidor")]
#[only_in("guilds")]
pub struct Moderation;

#[command("limparchat")]
#[aliases("clear", "prune")]
#[required_permissions("MANAGE_MESSAGES")]
#[max_args(2)]
#[description("Limpa as mensagens de até 13 dias atrás\n\n(Observação: você precisa da permissão de gerenciar mensagens para poder usar esse comando)")]
#[usage("clear [quantidade = 10] [usuario]")]
#[example("clear")]
#[example("clear 100")]
#[example("clear 100 @Vulcan")]
#[example("clear 100 203713369927057408")]
async fn limpar_chat(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let mut count = args.single::<u64>().unwrap_or(10);

    match get_user_from_args(ctx, &mut args).await.ok() {
        Some(user) => {
            let mut message_ref = msg.id;

            'delete_messages_user: loop {
                let count_messages = if count == 0 {
                    break 'delete_messages_user;
                } else if count <= 100 {
                    count
                } else {
                    100
                };

                let messages = msg
                    .channel_id
                    .messages(ctx, |x| x.limit(count_messages).before(message_ref))
                    .await;

                match messages {
                    Ok(messages) => {
                        message_ref = messages.last().unwrap().id;

                        let messages = messages
                            .iter()
                            .filter(|x| x.author.id == user.id)
                            .collect::<Vec<&Message>>();

                        match msg.channel_id.delete_messages(ctx, &messages).await {
                            Ok(_) => count -= messages.len() as u64,
                            Err(_) => break 'delete_messages_user,
                        }
                    }
                    Err(_) => break 'delete_messages_user,
                }
            }
        }
        None => {
            let mut message_ref = msg.id;

            'delete_messages: loop {
                let count_messages = if count == 0 {
                    break 'delete_messages;
                } else if count <= 100 {
                    count
                } else {
                    100
                };

                let messages = msg
                    .channel_id
                    .messages(ctx, |x| x.limit(count_messages).before(message_ref))
                    .await;

                match messages {
                    Ok(messages) => {
                        message_ref = messages.last().unwrap().id;

                        match msg.channel_id.delete_messages(ctx, &messages).await {
                            Ok(_) => count -= messages.len() as u64,
                            Err(_) => break 'delete_messages,
                        }
                    }
                    Err(_) => break 'delete_messages,
                }
            }
        }
    }

    msg.delete(ctx).await?;

    Ok(())
}

#[command("ban")]
#[required_permissions("BAN_MEMBERS")]
#[min_args(1)]
#[description("Bane um usuário\n\n(Observação: você precisa da permissão de banir membros para poder usar esse comando)")]
#[usage("ban <usuario> [motivo]")]
#[example("ban @Vulcan")]
#[example("ban @Vulcan fez baderna")]
#[example("ban 203713369927057408")]
#[example("ban 203713369927057408 fez baderna")]
async fn ban(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let user = get_user_from_args(ctx, &mut args).await?;

    let reason = args.remains().unwrap_or("Não informado");
    let guild = msg.guild_id.unwrap().to_guild_cached(ctx).unwrap();

    let member_role = get_user_role_position(ctx, &guild, &user).await?;

    let author_role = get_user_role_position(ctx, &guild, &msg.author).await?;

    let bot_role = get_user_role_position(ctx, &guild, &ctx.cache.current_user().into()).await?;

    if author_role > member_role && bot_role > member_role {
        send_alert(ctx, msg, &user, "banido", &guild.name, reason).await;
        match guild.ban_with_reason(ctx, &user, 7, reason).await {
            Ok(_) => {
                let mut embed = CreateEmbed::default();
                embed.description(format!(
                    "Prontinhooo! O {} foi banido do servidor 😀",
                    user.tag()
                ));
                embed.color(colors::PURPLE);

                msg.channel_id
                    .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                    .await?;
            }
            Err(err) => return Err(err.into()),
        };
    } else {
        let mut embed = CreateEmbed::default();
        embed.title("Sem permissão para banir o membro");
        embed.color(colors::YELLOW);

        msg.channel_id
            .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
            .await?;
    }

    Ok(())
}

#[command("kick")]
#[required_permissions("KICK_MEMBERS")]
#[min_args(1)]
#[description("Expulsa um usuário\n\n(Observação: você precisa da permissão de expulsar membros para poder usar esse comando)")]
#[usage("kick <usuario> [motivo]")]
#[example("kick @Vulcan")]
#[example("kick @Vulcan fez baderna")]
#[example("kick 203713369927057408")]
#[example("kick 203713369927057408 fez baderna")]
async fn kick(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let user = get_user_from_args(ctx, &mut args).await?;

    let reason = args.remains().unwrap_or("Não informado");
    let guild = msg.guild_id.unwrap().to_guild_cached(ctx).unwrap();

    let member_role = get_user_role_position(ctx, &guild, &user).await?;

    let author_role = get_user_role_position(ctx, &guild, &msg.author).await?;

    let bot_role = get_user_role_position(ctx, &guild, &ctx.cache.current_user().into()).await?;

    if author_role > member_role && bot_role > member_role {
        send_alert(ctx, msg, &user, "expulso", &guild.name, reason).await;
        match guild.kick_with_reason(ctx, &user, reason).await {
            Ok(_) => {
                let mut embed = CreateEmbed::default();
                embed.description(format!(
                    "Prontinhooo! O {} foi expulso do servidor 😀",
                    user.tag()
                ));
                embed.color(colors::PURPLE);

                msg.channel_id
                    .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                    .await?;
            }
            Err(err) => return Err(err.into()),
        };
    } else {
        let mut embed = CreateEmbed::default();
        embed.title("Sem permissão para expulsar o membro");
        embed.color(colors::YELLOW);

        msg.channel_id
            .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
            .await?;
    }

    Ok(())
}

#[command("softban")]
#[required_permissions("BAN_MEMBERS")]
#[min_args(1)]
#[description("Expulsa um usuário e apaga suas mensagens\n\n(Observação: você precisa da permissão de banir membros para poder usar esse comando)")]
#[usage("softban <usuario> [motivo]")]
#[example("softban @Vulcan")]
#[example("softban @Vulcan fez baderna")]
#[example("softban 203713369927057408")]
#[example("softban 203713369927057408 fez baderna")]
async fn softban(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let user = get_user_from_args(ctx, &mut args).await?;

    let reason = args.remains().unwrap_or("Não informado");
    let guild = msg.guild_id.unwrap().to_guild_cached(ctx).unwrap();

    let member_role = get_user_role_position(ctx, &guild, &user).await?;

    let author_role = get_user_role_position(ctx, &guild, &msg.author).await?;

    let bot_role = get_user_role_position(ctx, &guild, &ctx.cache.current_user().into()).await?;

    if author_role > member_role && bot_role > member_role {
        send_alert(ctx, msg, &user, "removido", &guild.name, reason).await;
        match guild.ban_with_reason(ctx, &user, 7, reason).await {
            Ok(_) => {
                let mut embed = CreateEmbed::default();
                embed.description(format!(
                    "Prontinhooo! O {} foi removido do servidor 😀",
                    user.tag()
                ));
                embed.color(colors::PURPLE);

                msg.channel_id
                    .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                    .await?;

                guild.unban(ctx, &user).await?;
            }
            Err(err) => return Err(err.into()),
        };
    } else {
        let mut embed = CreateEmbed::default();
        embed.title("Sem permissão para remover o membro");
        embed.color(colors::YELLOW);

        msg.channel_id
            .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
            .await?;
    }

    Ok(())
}

async fn send_alert(
    ctx: &Context,
    msg: &Message,
    user: &User,
    tipo: &str,
    guild_name: &str,
    reason: &str,
) {
    let mut embed = CreateEmbed::default();
    embed.title("**Buuuu buuuu desu waaaa!!!!!**");
    embed.description(format!("Você foi {} do servidor **{}**", tipo, guild_name));
    embed.image("https://i.imgur.com/bwifre6.jpg");
    embed.color(colors::PURPLE);
    embed.field("Motivo: ", reason, false);
    embed.field("Responsável: ", msg.author.tag(), false);

    let dm = user.create_dm_channel(ctx).await;

    if dm.is_ok() {
        let dm = dm.unwrap();
        dm.send_message(ctx, |x| x.set_embed(embed)).await.ok();
    }
}
