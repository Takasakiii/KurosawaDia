mod config;
mod util;
mod moderation;
mod weeb;

use chrono::{SecondsFormat, Utc};
use serenity::{client::Context, framework::{StandardFramework, standard::{CommandResult, macros::hook}}, model::channel::Message};

pub fn crete_framework() -> StandardFramework {
    StandardFramework::new()
        .configure(|x| x.prefix("k."))
        .group(&util::UTIL_GROUP)
        .group(&moderation::MODERATION_GROUP)
        .group(&weeb::WEEB_GROUP)
        .after(erro_handle)
}

#[hook]
async fn erro_handle(ctx: &Context, msg: &Message, name: &str, why: CommandResult) {
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