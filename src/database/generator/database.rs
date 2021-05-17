use mysql::{Error, PooledConn, prelude::Queryable};

use crate::config::get_default_prefix;

pub async fn gen_database(conn: &mut PooledConn) -> Result<(), Error> {
    conn.query_drop(format!(r"
        CREATE TABLE IF NOT EXISTS guilds (
            discord_id BIGINT UNSIGNED PRIMARY KEY,
            name VARCHAR(255) NOT NULL,
            prefix VARCHAR(15) NOT NULL DEFAULT '{}',
            guild_type INT UNSIGNED NOT NULL DEFAULT 0
        )
    ", get_default_prefix()))?;

    conn.query_drop(r"
        CREATE TABLE IF NOT EXISTS users (
            discord_id BIGINT UNSIGNED PRIMARY KEY,
            name VARCHAR(255) NOT NULL
        )
    ")?;

    conn.query_drop(r"
        CREATE TABLE IF NOT EXISTS fans (
            guild_id BIGINT UNSIGNED NOT NULL,
            user_id BIGINT UNSIGNED NOT NULL
        )
    ")?;

    Ok(())
}
