use serenity::{client::Context, framework::standard::{Args, CommandResult, macros::{command, group}}, model::channel::Message};

use crate::utils::user::{get_user_from_args, get_user_role_position};

#[group]
#[commands(limpar_chat, ban)]
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
async fn ban(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let user = match get_user_from_args(ctx, &mut args).await {
        Some(user) => user,
        None => return Err("Usuario não encontrado".into())
    };

    let reason = args.remains().unwrap_or("");
    let guild = msg.guild_id.unwrap();

    let member_role = get_user_role_position(ctx, &guild, &user).await?;
    
    let author_role = get_user_role_position(ctx, &guild, &msg.author).await?;

    let bot_role = get_user_role_position(ctx, &guild, &ctx.cache.current_user().await.into()).await?;

    if author_role > member_role && bot_role > member_role {
        match guild.ban_with_reason(ctx, user, 0, reason).await {
            Ok(_) => {
                println!("foda");
            },
            Err(_) => return Err("Tentou banir o dono".into())
        };
    } else {
        return Err("Sem permissão para banir o membro".into())
    }

    Ok(())
} 