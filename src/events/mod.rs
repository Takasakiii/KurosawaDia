mod status;

use serenity::{async_trait, client::{Context, EventHandler}, model::prelude::Ready};

use crate::events::status::loop_status_update;

pub struct Handler;

#[async_trait]
impl EventHandler for Handler {
    async fn ready(&self, ctx: Context, data_about_bot: Ready) {
        println!("{} está online", data_about_bot.user.tag());

        loop_status_update(ctx).await;
    }
}
