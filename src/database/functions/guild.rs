use serenity::{framework::standard::CommandResult, model::id::GuildId};

use crate::database::get_database_connection;

pub async fn register_guild(_guild: GuildId) -> CommandResult {
    let _conn = get_database_connection().await?;

    Ok(())
}
