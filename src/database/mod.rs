mod generator;
pub mod functions;
pub mod models;

use mysql::{Error, Pool, PooledConn, prelude::Queryable};

use crate::config::{get_database_connection_string, get_database_name};

use self::generator::{database::gen_database, procedores::gen_procedores, seeding::gen_seeding};

static mut CONNECTION: Option<Pool> = None;

pub async fn get_database_connection() -> Result<PooledConn, Error>{
    unsafe {
        match &CONNECTION {
            Some(pool) => Ok(pool.get_conn()?),
            None => {
                let connection_string = format!("{}/{}", get_database_connection_string(), get_database_name());
                let pool = Pool::new(connection_string)?;
                let conn = pool.get_conn()?;
                CONNECTION = Some(pool);
                Ok(conn)
            }
        }
    }
}

pub async fn crate_database() -> Result<(), Error> {
    let mut conn = match get_database_connection().await {
        Ok(conn) => conn,
        Err(err) => {
            if let Error::MySqlError(err) = err {
                if err.code == 1049 {
                    let pool = Pool::new(get_database_connection_string())?;
                    let mut conn = pool.get_conn()?;
                    conn.query_drop(r"CREATE DATABASE IF NOT EXISTS kurosawa_dia")?;
                    
                    get_database_connection().await?
                } else {
                    return Err(err.into());
                }
            } else {
                return Err(err);
            }
        }
    };

    gen_database(&mut conn).await?;
    gen_procedores(&mut conn).await?;
    gen_seeding(&mut conn).await?;

    Ok(())
}
