use std::time::Duration;

use serenity::{client::Context, model::prelude::Activity};
use tokio::time::interval;

use crate::database::functions::status::get_db_status;

pub async fn loop_status_update(ctx: Context) {
    let mut interval = interval(Duration::from_secs(30));

    loop {
        let status = get_db_status().await;

        if let Ok(status_vec) = status {
            if status_vec.is_empty() {
                interval.tick().await;
            } else {
                for status in &status_vec {
                    ctx.set_activity(Activity::playing(&status.status)).await;

                    interval.tick().await;
                }
            }
        } else {
            interval.tick().await;
        }
    }
}
