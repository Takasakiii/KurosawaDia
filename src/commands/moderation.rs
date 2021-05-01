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
    count += 1;

    match get_user_from_args(ctx, &mut args).await {
        Some(user) => {

        },
        None => {
            'send_messages: loop {
                let count_messages = if count == 0 {
                    break 'send_messages;
                } else if count <= 100 {
                    let temp = count;
                    count -= count;
                    temp
                } else {
                    count -= 100;
                    100
                };

                let messages = msg.channel_id.messages(ctx, |x| x
                    .limit(count_messages)
                ).await;

                match messages {
                    Ok(messages) => {
                        msg.channel_id.delete_messages(ctx, messages).await?;
                    },
                    Err(_) => {
                        break 'send_messages;
                    }
                }
            }
        }
    }

    Ok(())
}