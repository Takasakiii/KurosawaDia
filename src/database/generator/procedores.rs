use mysql::{Error, PooledConn, prelude::Queryable};

pub async fn gen_procedores(conn: &mut PooledConn) -> Result<(), Error> {
    conn.query_drop(r"
        CREATE PROCEDURE IF NOT EXISTS register_guild (
            IN discord_id BIGINT UNSIGNED,
            IN name VARCHAR(255)
        )
        BEGIN
            DECLARE rows INT DEFAULT (SELECT count(*) FROM guilds WHERE discord_id == @discord_id)
            IF (rows = 0) THEN
                INSERT INTO guilds (discord_id, name) VALUES (@discord_id, @name)
            END IF
        END
    ")?;

    Ok(())
}
