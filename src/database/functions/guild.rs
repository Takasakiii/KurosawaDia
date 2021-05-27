use mysql::{params, prelude::Queryable};
use serenity::{framework::standard::{CommandError, CommandResult}, model::guild::Guild};

use crate::database::{get_database_connection, models::guild::DbGuild};

pub async fn register_guild(guild: Guild) -> CommandResult {
    let mut conn = get_database_connection().await?;

    conn.exec_drop(r"INSERT IGNORE INTO guilds (discord_id, name) VALUES (:discord_id, :name)", params! {
        "discord_id" => guild.id.to_string(),
        "name" => guild.name
    })?;

    Ok(())
}

pub async fn get_db_guild(guild: Guild) -> Result<DbGuild, CommandError> {
    let mut conn = get_database_connection().await?;

    let mut result: Vec<DbGuild> = conn.exec_map(r"
        SELECT * FROM guilds g
        WHERE g.discord_id = :discord_id
        LIMIT 1
    ", params! {
        "discord_id" => guild.id.to_string()
    }, |(discord_id, name, prefix, guild_type)| {
        DbGuild {
            discord_id,
            name,
            prefix,
            guild_type
        }
    })?;

    if let Some(db_guild) = result.pop() {
        Ok(db_guild)
    } else {
        Err("Guild não registrada".into())
    }
}

pub async fn set_prefix(guild: Guild, new_prefix: &String) -> CommandResult {
    let mut conn = get_database_connection().await?;

    conn.exec_drop(r"
        UPDATE guilds
        SET prefix = :prefix
        WHERE discord_id = :discord_id 
    ", params! {
        "discord_id" => guild.id.to_string(),
        "prefix" => new_prefix
    })?;

    let rows = conn.affected_rows();

    if rows == 1 {
        Ok(())
    } else {
        Err("Falha em atualizar o prefixo na db".into())
    }
}
