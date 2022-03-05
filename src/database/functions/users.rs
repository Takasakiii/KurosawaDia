use mysql::{params, prelude::Queryable};
use serenity::{model::prelude::User, framework::standard::CommandResult};

use crate::database::get_database_connection;

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
