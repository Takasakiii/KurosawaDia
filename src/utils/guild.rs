use serenity::{client::Context, model::guild::Guild};

use crate::KurosawaResult;

pub async fn get_guild_from_id(ctx: &Context, id: u64) -> KurosawaResult<Guild> {
    match ctx.cache.guild(id) {
        Some(guild) => Ok(guild),
        None => Err("Guild fora do cache".into()),
    }
}
