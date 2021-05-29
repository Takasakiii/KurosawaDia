use serenity::{builder::CreateEmbed, client::Context, framework::standard::{Args, CommandResult, macros::{command, group}}, model::channel::{Channel, Message}};

use crate::{apis::get_danbooru_api, utils::constants::colors};

#[group]
#[commands(hentai, hdanbooru, danbooru)]
pub struct Nsfw;

#[command("hentai")]
async fn hentai(ctx: &Context, msg: &Message) -> CommandResult {
    let channel = msg.channel(ctx).await;

    if let Some(Channel::Guild(channel)) = channel {
        if !channel.nsfw {
            return Ok(());
        }
    }

    let image = get_danbooru_api()
        .get_hentai_random()
        .await?;

    let mut embed = CreateEmbed::default();
    embed.image(image.large_file_url);
    embed.color(colors::LILAC);

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    ).await?;

    Ok(())
}

#[command("hdanbooru")]
#[max_args(5)]
async fn hdanbooru(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let channel = msg.channel(ctx).await;

    if let Some(Channel::Guild(channel)) = channel {
        if !channel.nsfw {
            return Ok(());
        }
    }

    let mut tags: Vec<String> = Vec::new();

    while let Ok(arg) = args.single::<String>() {
        tags.push(arg);
    }

    let image = get_danbooru_api()
        .get_hentai_tags(tags.iter()
            .map(String::as_str)
            .collect::<Vec<&str>>()
            .as_slice())
        .await;

    match image {
        Ok(image) => {
            let mut embed = CreateEmbed::default();
            embed.image(image.large_file_url);
            embed.color(colors::LILAC);
        
            msg.channel_id.send_message(ctx, |x| x
                .set_embed(embed)
                .reference_message(msg)
            ).await?;
        },
        Err(_) => {
            let mut embed = CreateEmbed::default();
            embed.title("Tags não encontradas");
            embed.color(colors::YELLOW);
        
            msg.channel_id.send_message(ctx, |x| x
                .set_embed(embed)
                .reference_message(msg)
            ).await?;
        }
    }


    Ok(())
}

#[command("danbooru")]
#[max_args(6)]
async fn danbooru(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let channel = msg.channel(ctx).await;

    if let Some(Channel::Guild(channel)) = channel {
        if !channel.nsfw {
            return Ok(());
        }
    }

    let mut tags: Vec<String> = Vec::new();

    while let Ok(arg) = args.single::<String>() {
        tags.push(arg);
    }

    let image = get_danbooru_api()
        .get_tags(tags.iter()
            .map(String::as_str)
            .collect::<Vec<&str>>()
            .as_slice())
        .await;

    match image {
        Ok(image) => {
            let mut embed = CreateEmbed::default();
            embed.image(image.large_file_url);
            embed.color(colors::LILAC);
        
            msg.channel_id.send_message(ctx, |x| x
                .set_embed(embed)
                .reference_message(msg)
            ).await?;
        },
        Err(_) => {
            let mut embed = CreateEmbed::default();
            embed.title("Tags não encontradas");
            embed.color(colors::YELLOW);
        
            msg.channel_id.send_message(ctx, |x| x
                .set_embed(embed)
                .reference_message(msg)
            ).await?;
        }
    }

    
    Ok(())
}
