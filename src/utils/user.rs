use serenity::{client::Context, framework::standard::Args, model::{prelude::User}, utils::parse_username};

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