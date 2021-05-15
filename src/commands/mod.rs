mod config;
mod util;
mod moderation;
mod weeb;

use chrono::{SecondsFormat, Utc};
use serenity::{client::Context, framework::{StandardFramework, standard::{CommandResult, macros::hook}}, model::channel::Message};
use tokio::spawn;

use crate::database::functions::guild::register_guild;

pub fn crete_framework() -> StandardFramework {
    StandardFramework::new()
        .configure(|x| x.prefix("k."))
        .group(&util::UTIL_GROUP)
        .group(&moderation::MODERATION_GROUP)
        .group(&weeb::WEEB_GROUP)
        .before(before_command)
        .after(after_command)
        .normal_message(normal_message)
}

#[hook]
async fn normal_message(_ctx: &Context, _msg: &Message) {

}

#[hook]
async fn before_command(_ctx: &Context, msg: &Message, _name: &str) -> bool {
    match msg.guild_id {
        Some(guild_id) => {
            let thread = spawn(async move {
                register_guild(guild_id).await
            });

            let result = match thread.await {
                Ok(result) => result,
                Err(_) => return false
            };

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
        msg.react(ctx, '❌').await.unwrap();
    }
}