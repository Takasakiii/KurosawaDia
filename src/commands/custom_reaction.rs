use serenity::{client::Context, framework::standard::{Args, CommandResult, macros::{command, group}}, model::channel::Message};

use crate::database::{functions::custom_reaction::add_custom_reaction, models::custom_reaction::DbCustomReactionType};

#[group]
#[commands(acr)]
pub struct CustomReaction;

#[command("adicionarrc")]
#[aliases("acr", "adicionarcr", "arc")]
#[only_in("guilds")]
#[min_args(3)]
async fn acr(ctx: &Context, msg: &Message, args: Args) -> CommandResult {
    let args = args.remains().unwrap().split('|').collect::<Vec<&str>>();

    if args.len() != 2 {
        return Ok(());
    }

    let guild = msg.guild(ctx).await.unwrap();

    add_custom_reaction(
        guild, 
        args[0].trim_end().to_string(), 
        args[1].trim_start().to_string(), 
        DbCustomReactionType::Normal
    ).await?;

    Ok(())
}
