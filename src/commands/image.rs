use rand::{Rng, thread_rng};
use serenity::{builder::CreateEmbed, client::Context, framework::standard::{CommandResult, macros::{command, group}}, model::channel::Message};

use crate::{apis::{get_nekoslife_api, get_woof_api}, utils::{constants::colors, user::get_user_from_id}};

#[group]
#[commands(cat, dog)]
pub struct Image;

#[command("cat")]
async fn cat(ctx: &Context, msg: &Message) -> CommandResult {
    let api = get_nekoslife_api();
    let image = api.get_cat().await?;

    let mut embed = CreateEmbed::default();
    embed.title("Meow");
    embed.description(format!("[Link direto]({})", image.url));
    embed.image(image.url);
    embed.color(colors::TURQUOISE);

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    ).await?;

    Ok(())
}

#[command("dog")]
async fn dog(ctx: &Context, msg: &Message) -> CommandResult {
    let rng = thread_rng().gen_range(0..100);

    if rng != 24 {
        let api = get_woof_api();
        let image = api.get_random().await?;

        let mut embed = CreateEmbed::default();
        embed.title("Woof");
        embed.description(format!("[Link direto]({})", image.message));
        embed.image(image.message);
        embed.color(colors::TURQUOISE);

        msg.channel_id.send_message(ctx, |x| x
            .set_embed(embed)
            .reference_message(msg)
        ).await?;
    } else {
        let user = get_user_from_id(ctx, 355750436424384524).await;
        if let Some(user) = user {
            let mut embed = CreateEmbed::default();
            embed.title("Você está procurando um cachorinho? Me adote");
            embed.description(format!("Me adote no Discord, `{}`! Woof Woof", user.tag()));
            embed.thumbnail(user.avatar_url().unwrap());
            embed.color(colors::TURQUOISE);

            msg.channel_id.send_message(ctx, |x| x
                .set_embed(embed)
                .reference_message(msg)
            ).await?;
        }

    }

    Ok(())
}
