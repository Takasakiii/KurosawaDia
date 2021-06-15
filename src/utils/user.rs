use serenity::{
    client::Context,
    framework::standard::{Args, CommandError},
    model::{guild::Guild, prelude::User},
    utils::parse_username,
};

use crate::KurosawaResult;

pub async fn get_user_from_args(ctx: &Context, args: &mut Args) -> KurosawaResult<User> {
    let id = match args.single::<String>() {
        Ok(id) => id,
        Err(_) => return Err("falha ao converter o id".into()),
    };

    let user_id = match parse_username(&id) {
        Some(id) => id,
        None => match id.parse::<u64>() {
            Ok(id) => id,
            Err(_) => return Err("falha ao converter o id".into()),
        },
    };

    get_user_from_id(ctx, user_id).await
}

pub async fn get_user_role_position(
    ctx: &Context,
    guild: &Guild,
    user: &User,
) -> Result<i64, CommandError> {
    let member = guild.member(ctx, user.id).await?;
    let mut position = match member.highest_role_info(ctx).await {
        Some((_, position)) => position,
        None => 0,
    };

    if guild.owner_id == user.id {
        position = i64::MAX;
    }

    Ok(position)
}

pub async fn get_user_from_id(ctx: &Context, id: u64) -> KurosawaResult<User> {
    match ctx.cache.user(id).await {
        Some(user) => Ok(user),
        None => Ok(ctx.http.get_user(id).await?),
    }
}
