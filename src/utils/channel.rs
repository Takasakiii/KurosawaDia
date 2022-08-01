use serenity::{client::Context, model::channel::Channel};

use crate::KurosawaResult;

pub async fn get_channel_from_id(ctx: &Context, id: u64) -> KurosawaResult<Channel> {
    match ctx.cache.channel(id) {
        Some(channnel) => Ok(channnel),
        None => Ok(ctx.http.get_channel(id).await?),
    }
}
