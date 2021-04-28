use serenity::{builder::CreateEmbed, client::Context, framework::standard::{CommandResult, macros::{command, group}}, model::channel::Message};

use crate::utils::user::get_user_from_id_or_mention;

#[group]
#[commands(avatar)]
pub struct Util;

#[command]
pub async fn avatar(ctx: &Context, msg: &Message) -> CommandResult {
    let user = get_user_from_id_or_mention(msg, ctx).await;
    let user = match user {
        Some(user) => user,
        None => msg.author.clone()
    };

    let avatar = match user.avatar_url() {
        Some(avatar) => avatar,
        None => user.default_avatar_url()
    };

    let mut embed = CreateEmbed::default();
    embed.color(0x5fad47);
    embed.title("Uiui que gatinho");
    embed.image(avatar);

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    ).await?;

    Ok(())
}