use serenity::{client::Context, framework::standard::{CommandResult, macros::{command, group}}, model::channel::Message};

#[group]
#[commands(limpar_chat)]
pub struct Moderation;

#[command("limparchat")]
#[aliases("clear", "prune")]
#[only_in("guilds")]
async fn limpar_chat(ctx: &Context, msg: &Message) -> CommandResult {
    

    Ok(())
}