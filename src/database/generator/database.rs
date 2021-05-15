use mysql::{Error, PooledConn, prelude::Queryable};

pub async fn gen_database(conn: &mut PooledConn) -> Result<(), Error> {
    conn.query_drop(r"
        CREATE TABLE IF NOT EXISTS guild_types (
            id INT UNSIGNED PRIMARY KEY,
            name VARCHAR(15) NOT NULL
        )
    ")?;
    
    conn.query_drop(r"
        CREATE TABLE IF NOT EXISTS guilds (
            discord_id BIGINT UNSIGNED PRIMARY KEY,
            name VARCHAR(255) NOT NULL,
            prefix VARCHAR(15),
            guild_type INT UNSIGNED DEFAULT 0, 
            CONSTRAINT fk_guild_type FOREIGN KEY (guild_type) REFERENCES guild_types (id)
        )
    ")?;

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
