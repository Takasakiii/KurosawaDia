use serenity::{framework::standard::CommandResult, model::id::GuildId};

use crate::database::get_db_connection;

pub async fn register_guild(guild: GuildId) -> CommandResult {
    let mut conn = get_db_connection().await?;

    Ok(())
}