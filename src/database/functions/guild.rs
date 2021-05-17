use mysql::{params, prelude::Queryable};
use serenity::{framework::standard::CommandResult, model::guild::Guild};

use crate::database::get_database_connection;

pub async fn register_guild(guild: Guild) -> CommandResult {
    let mut conn = get_database_connection().await?;

    conn.exec_drop(r"INSERT IGNORE INTO guilds (discord_id, name) VALUES (:discord_id, :name)", params! {
        "discord_id" => guild.id.to_string(),
        "name" => guild.name
    })?;

    Ok(())
}
