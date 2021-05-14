use mysql_async::{Error, Conn, prelude::Queryable};

pub async fn gen_database(conn: &mut Conn) -> Result<(), Error> {
    conn.query_drop(r"CREATE TABLE IF NOT EXISTS servidores (
        discord_id BIGINT UNSIGNED PRIMARY KEY,
        name VARCHAR(255) NOT NULL,
        prefix VARCHAR(15),
        type INT NOT NULL DEFAULT 0
    )").await?;

    conn.query_drop(r"CREATE TABLE IF NOT EXISTS usuarios (
        discord_id BIGINT UNSIGNED PRIMARY KEY,
        name VARCHAR(255) NOT NULL
    )").await?;

    Ok(())
}