pub mod functions;
mod generator;
pub mod models;

use std::convert::TryFrom;

use mysql::{prelude::Queryable, Error, Opts, Pool, PooledConn};

use crate::config::KurosawaConfig;

use self::generator::{database::gen_database, procedores::gen_procedores};

static mut CONNECTION: Option<Pool> = None;

pub async fn get_database_connection() -> Result<PooledConn, Error> {
    unsafe {
        match &CONNECTION {
            Some(pool) => Ok(pool.get_conn()?),
            None => {
                let connection_string = format!(
                    "{}/{}",
                    KurosawaConfig::get_database_connection_string(),
                    KurosawaConfig::get_database_name()
                );
                let opts = Opts::try_from(connection_string.as_str())?;
                let pool = Pool::new(opts)?;
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
                    let opts =
                        Opts::try_from(KurosawaConfig::get_database_connection_string().as_str())?;
                    let pool = Pool::new(opts)?;
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

    Ok(())
}
