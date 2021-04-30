mod config;
mod util;
mod moderation;

use serenity::{client::Context, framework::{StandardFramework, standard::{CommandResult, macros::hook}}, model::channel::Message};

pub fn crete_framework() -> StandardFramework {
    StandardFramework::new()
        .configure(|x| x.prefix("k."))
        .group(&util::UTIL_GROUP)
        .group(&moderation::MODERATION_GROUP)
        .after(erro_handle)
}

#[hook]
async fn erro_handle(_: &Context, _: &Message, name: &str, why: CommandResult) {
    if let Err(why) = why {
        println!("Command: {} Error: {:?}", name, why)
    }
}