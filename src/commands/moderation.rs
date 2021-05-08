use serenity::{builder::CreateEmbed, client::Context, framework::standard::{Args, CommandResult, macros::{command, group}}, model::{channel::Message, prelude::User}};

use crate::utils::{constants::colors, user::{get_user_from_args, get_user_role_position}};

#[group]
#[commands(limpar_chat, ban, kick, softban)]
pub struct Moderation;

#[command("limparchat")]
#[aliases("clear", "prune")]
#[only_in("guilds")]
#[required_permissions("MANAGE_MESSAGES")]
#[max_args(2)]
async fn limpar_chat(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let mut count = args.single::<u64>().unwrap_or(10);

    match get_user_from_args(ctx, &mut args).await {
        Some(user) => {
            let mut message_ref = msg.id;

            'delete_messages_user: loop {

                let count_messages = if count == 0 {
                    break 'delete_messages_user
                } else if count <= 100 {
                    count
                } else {
                    100
                };

                let messages = msg.channel_id.messages(ctx, |x| x
                    .limit(count_messages)
                    .before(message_ref)
                ).await;

                match messages {
                    Ok(messages) => {
                        message_ref = messages.last().unwrap().id;

                        let messages = messages
                            .iter()
                            .filter(|x| x.author.id == user.id)
                            .collect::<Vec<&Message>>();

                        match msg.channel_id.delete_messages(ctx, &messages).await {
                            Ok(_) => count -= messages.len() as u64,
                            Err(_) => break 'delete_messages_user
                        }
                    },
                    Err(_) => break 'delete_messages_user
                }
            }
        },
        None => {
            let mut message_ref = msg.id;

            'delete_messages: loop {
                let count_messages = if count == 0 {
                    break 'delete_messages
                } else if count <= 100 {
                    count
                } else {
                    100
                };

                let messages = msg.channel_id.messages(ctx, |x| x
                    .limit(count_messages)
                    .before(message_ref)
                ).await;

                match messages {
                    Ok(messages) => {
                        message_ref = messages.last().unwrap().id;

                        match msg.channel_id.delete_messages(ctx, &messages).await {
                            Ok(_) => count -= messages.len() as u64,
                            Err(_) => break 'delete_messages
                        }
                    },
                    Err(_) => break 'delete_messages
                }
            }
        }
    }

    msg.delete(ctx).await?;

    Ok(())
}


#[command("ban")]
#[only_in("guilds")]
#[required_permissions("BAN_MEMBERS")]
#[min_args(1)]
async fn ban(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let user = match get_user_from_args(ctx, &mut args).await {
        Some(user) => user,
        None => return Err("Usuario não encontrado".into())
    };

    let reason = args.remains().unwrap_or("Não informado");
    let guild = msg.guild_id.unwrap().to_guild_cached(ctx).await.unwrap();

    let member_role = get_user_role_position(ctx, &guild, &user).await?;
    
    let author_role = get_user_role_position(ctx, &guild, &msg.author).await?;

    let bot_role = get_user_role_position(ctx, &guild, &ctx.cache.current_user().await.into()).await?;

    if author_role > member_role && bot_role > member_role {
        send_alert(&ctx, &msg, &user, "banido", &guild.name, &reason).await;
        match guild.ban_with_reason(ctx, &user, 7, reason).await {
            Ok(_) => {
                let mut embed = CreateEmbed::default();
                embed.description(format!("Prontinhooo! O {} foi banido do servidor 😀", user.tag()));
                embed.color(colors::PURPLE);

                msg.channel_id.send_message(ctx, |x| x
                    .set_embed(embed)
                    .reference_message(msg)
                ).await?;
            },
            Err(err) => return Err(err.into())
        };
    } else {
        return Err("Sem permissão para banir o membro".into())
    }

    Ok(())
}

#[command("kick")]
#[only_in("guilds")]
#[required_permissions("KICK_MEMBERS")]
#[min_args(1)]
async fn kick(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let user = match get_user_from_args(ctx, &mut args).await {
        Some(user) => user,
        None => return Err("Usuario não encontrado".into())
    };

    let reason = args.remains().unwrap_or("Não informado");
    let guild = msg.guild_id.unwrap().to_guild_cached(ctx).await.unwrap();

    let member_role = get_user_role_position(ctx, &guild, &user).await?;
    
    let author_role = get_user_role_position(ctx, &guild, &msg.author).await?;

    let bot_role = get_user_role_position(ctx, &guild, &ctx.cache.current_user().await.into()).await?;

    if author_role > member_role && bot_role > member_role {
        send_alert(&ctx, &msg, &user, "expulso", &guild.name, &reason).await;
        match guild.kick_with_reason(ctx, &user, reason).await {
            Ok(_) => {
                let mut embed = CreateEmbed::default();
                embed.description(format!("Prontinhooo! O {} foi expulso do servidor 😀", user.tag()));
                embed.color(colors::PURPLE);

                msg.channel_id.send_message(ctx, |x| x
                    .set_embed(embed)
                    .reference_message(msg)
                ).await?;
            },
            Err(err) => return Err(err.into())
        };
    } else {
        return Err("Sem permissão para expulsar o membro".into())
    }

    Ok(())
}

#[command("softban")]
#[only_in("guilds")]
#[required_permissions("BAN_MEMBERS")]
#[min_args(1)]
async fn softban(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let user = match get_user_from_args(ctx, &mut args).await {
        Some(user) => user,
        None => return Err("Usuario não encontrado".into())
    };

    let reason = args.remains().unwrap_or("Não informado");
    let guild = msg.guild_id.unwrap().to_guild_cached(ctx).await.unwrap();

    let member_role = get_user_role_position(ctx, &guild, &user).await?;
    
    let author_role = get_user_role_position(ctx, &guild, &msg.author).await?;

    let bot_role = get_user_role_position(ctx, &guild, &ctx.cache.current_user().await.into()).await?;

    if author_role > member_role && bot_role > member_role {
        send_alert(&ctx, &msg, &user, "removido", &guild.name, &reason).await;
        match guild.ban_with_reason(ctx, &user, 7, reason).await {
            Ok(_) => {
                let mut embed = CreateEmbed::default();
                embed.description(format!("Prontinhooo! O {} foi removido do servidor 😀", user.tag()));
                embed.color(colors::PURPLE);

                msg.channel_id.send_message(ctx, |x| x
                    .set_embed(embed)
                    .reference_message(msg)
                ).await?;

                guild.unban(ctx, &user).await?;
            },
            Err(err) => return Err(err.into())
        };
    } else {
        return Err("Sem permissão para remover o membro".into())
    }

    Ok(())
}

async fn send_alert(ctx: &Context, msg: &Message, user: &User, tipo: &str, guild_name: &String, reason: &str) {
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
        dm.send_message(ctx, |x| x
            .set_embed(embed)
        ).await.ok();
    }
}