mod config;
mod util;
mod moderation;
mod weeb;

use chrono::{SecondsFormat, Utc};
use serenity::{client::Context, framework::{StandardFramework, standard::{CommandResult, macros::hook}}, model::channel::Message};

use crate::database::functions::guild::register_guild;

pub fn crete_framework() -> StandardFramework {
    StandardFramework::new()
        .configure(|x| x.prefix("k."))
        .group(&util::UTIL_GROUP)
        .group(&moderation::MODERATION_GROUP)
        .group(&weeb::WEEB_GROUP)
        .before(before_command)
        .after(after_command)
}

#[hook]
async fn before_command(ctx: &Context, msg: &Message, name: &str) -> bool {
    match msg.guild_id {
        Some(guild) => {
            match register_guild(guild).await {
                Ok(_) => true,
                Err(_) => true
            }
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