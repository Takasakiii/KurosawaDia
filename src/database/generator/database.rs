use mysql::{prelude::Queryable, Error, PooledConn};

use crate::config::get_default_prefix;

pub async fn gen_database(conn: &mut PooledConn) -> Result<(), Error> {
    conn.query_drop(format!(
        r"
        CREATE TABLE IF NOT EXISTS guilds (
            discord_id BIGINT UNSIGNED PRIMARY KEY,
            name VARCHAR(255) NOT NULL,
            prefix VARCHAR(15) NOT NULL DEFAULT '{}',
            guild_type INT UNSIGNED NOT NULL DEFAULT 0
        )
    ",
        get_default_prefix()
    ))?;

    conn.query_drop(
        r"
        CREATE TABLE IF NOT EXISTS users (
            discord_id BIGINT UNSIGNED PRIMARY KEY,
            name VARCHAR(255) NOT NULL
        )
    ",
    )?;

    conn.query_drop(
        r"
        CREATE TABLE IF NOT EXISTS fans (
            guild_id BIGINT UNSIGNED NOT NULL,
            user_id BIGINT UNSIGNED NOT NULL
        )
    ",
    )?;

    conn.query_drop(
        r"
        CREATE TABLE IF NOT EXISTS status (
            id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
            status VARCHAR(100) NOT NULL
        )
    ",
    )?;

    conn.query_drop(
        r"
        CREATE TABLE IF NOT EXISTS custom_reactions (
            id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
            question TEXT NOT NULL,
            reply TEXT NOT NULL,
            cr_type INT UNSIGNED NOT NULL DEFAULT 0,
            guild_id BIGINT UNSIGNED NOT NULL,
            FOREIGN KEY (guild_id) REFERENCES guilds(discord_id)
        )
    ",
    )?;

    Ok(())
}
