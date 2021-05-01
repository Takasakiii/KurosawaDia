use serenity::{client::Context, framework::standard::Args, model::{prelude::User}, utils::parse_username};

pub async fn get_user_from_args(ctx: &Context, args: &mut Args) -> Option<User> {
    let id = match args.single::<String>() {
        Ok(id) => id,
        Err(_) => return None
    };

    let user = match parse_username(&id) {
        Some(id) => id,
        None => match id.parse::<u64>() {
            Ok(id) => id,
            Err(_) => return None
        }
    };

    ctx.cache.user(user).await
}