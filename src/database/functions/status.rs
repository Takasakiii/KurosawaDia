use mysql::prelude::Queryable;
use serenity::framework::standard::CommandError;

use crate::database::{get_database_connection, models::status::DbStatus};

pub async fn get_db_status() -> Result<Vec<DbStatus>, CommandError> {
    let mut conn = get_database_connection().await?;

    let result: Vec<DbStatus> = conn.query_map(
        r"
        SELECT * FROM status
    ",
        |(id, status)| DbStatus { id, status },
    )?;

    Ok(result)
}
