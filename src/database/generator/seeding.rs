use mysql::{Error, PooledConn};

pub async fn gen_seeding(_conn: &mut PooledConn) -> Result<(), Error> {
    Ok(())
}
