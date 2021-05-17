use serenity::{client::Context, framework::standard::{Args, CommandResult, macros::{command, group}}, model::channel::Message};

#[group]
#[commands(prefix)]
pub struct Config;

#[command("prefix")]
#[aliases("setprefix")]
async fn prefix(_ctx: &Context, _msg: &Message, _args: Args) -> CommandResult {
    Ok(())
}
