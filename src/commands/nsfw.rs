use rand::{Rng, thread_rng};
use serenity::{builder::CreateEmbed, client::Context, framework::standard::{CommandError, CommandResult, macros::{command, group}}, model::channel::{Channel, Message}};

use crate::{apis::get_nekoslife_api, utils::constants::colors};

#[group]
#[commands(hentai)]
pub struct Nsfw;

#[command("hentai")]
async fn hentai(ctx: &Context, msg: &Message) -> CommandResult {
    let channel = msg.channel(ctx).await;

    if let Some(Channel::Guild(channel)) = channel {
        if !channel.nsfw {
            return Ok(());
        }
    }

    let image = get_hentai_url().await?;

    let mut embed = CreateEmbed::default();
    embed.image(image);
    embed.color(colors::LILAC);

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    ).await?;

    Ok(())
}

async fn get_hentai_url() -> Result<String, CommandError> {
    let num = thread_rng().gen_range(0..1);

    if num == 0 {
        let image = get_nekoslife_api()
            .gen_hentai()
            .await?;

        return Ok(image.url);
    }

    Ok("foda".into())
}
