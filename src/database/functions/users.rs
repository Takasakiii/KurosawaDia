use mysql::{params, prelude::Queryable};
use serenity::{model::{prelude::User, id::UserId}, framework::standard::CommandResult};

use crate::database::{get_database_connection, models::user::DbUser};

use super::DbResult;

pub async fn register_user(user: User) -> CommandResult {
    let mut conn = get_database_connection().await?;

    conn.exec_drop(
        r"INSERT IGNORE INTO users (discord_id, name) VALUES (:discord_id, :name)",
        params! {
            "discord_id" => user.id.to_string(),
            "name" => user.name
        },
    )?;

    Ok(())
}

pub async fn get_db_user(discord_id: UserId) -> DbResult<DbUser> {
    let mut conn = get_database_connection().await?;

    let mut result: Vec<DbUser> = conn.exec_map(r"
        SELECT * FROM users u
        WHERE u.discord_id = :discord_id
        LIMIT 1
    ", params! {
        "discord_id" => discord_id.to_string()
    }, |(discord_id, name, enable_cr)| DbUser {
        discord_id,
        name,
        enable_cr
    })?;

    if let Some(db_user) = result.pop() {
        Ok(db_user)
    } else {
        Err("Guild não registrada".into())
    }
}
