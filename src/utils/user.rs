use serenity::{client::Context, framework::standard::{Args, CommandError}, model::{id::GuildId, prelude::User}, utils::parse_username};

pub async fn get_user_from_args(ctx: &Context, args: &mut Args) -> Option<User> {
    let id = match args.single::<String>() {
        Ok(id) => id,
        Err(_) => return None
    };

    let user_id = match parse_username(&id) {
        Some(id) => id,
        None => match id.parse::<u64>() {
            Ok(id) => id,
            Err(_) => return None
        }
    };

    match ctx.cache.user(user_id).await {
        Some(user) => Some(user),
        None => ctx.http.get_user(user_id).await.ok()
    }
}

pub async fn get_user_role_position(ctx: &Context, guild: &GuildId, user: &User) -> Result<i64, CommandError> {
    let member = guild.member(ctx, user.id).await?;
    match member.highest_role_info(ctx).await {
        Some((_, position)) => Ok(position),
        None => Ok(0)
    }
}