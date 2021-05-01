use serenity::{client::Context, framework::standard::{Args, CommandResult, macros::{command, group}}, model::channel::Message};

use crate::utils::user::get_user_from_args;

#[group]
#[commands(limpar_chat)]
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