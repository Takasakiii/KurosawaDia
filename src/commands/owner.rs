use serenity::{client::Context, framework::standard::{Args, CommandResult, macros::{command, group}}, model::channel::Message};

use crate::database::{functions::guild::set_especial, models::guild::DbGuildType};

#[group]
#[commands(especial)]
#[help_available(false)]
pub struct Owner;

#[command("setespecial")]
#[owners_only]
#[max_args(1)]
#[min_args(1)]
#[only_in("guilds")]
async fn especial(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let new_type = args.single::<u32>()?;

    let guild = msg.guild(ctx).await.unwrap();

    set_especial(guild, DbGuildType::from(new_type)).await?;

    Ok(())
}
