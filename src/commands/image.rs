use serenity::{builder::CreateEmbed, client::Context, framework::standard::{CommandResult, macros::{command, group}}, model::channel::Message};

use crate::{apis::get_nekoslife_api, utils::constants::colors};

#[group]
#[commands(cat)]
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
