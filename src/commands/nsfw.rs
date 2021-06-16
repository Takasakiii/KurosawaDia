use serenity::{
    builder::CreateEmbed,
    client::Context,
    framework::standard::{
        macros::{command, group},
        Args, CommandResult,
    },
    model::channel::{Channel, Message},
};

use crate::{apis::get_danbooru_api, utils::constants::colors};

#[group]
#[commands(hentai, hdanbooru, danbooru)]
#[description("Nsfw 🔞- Esse módulo possui coisas para você dar orgulho para sua família")]
pub struct Nsfw;

#[command("hentai")]
#[description("Consiga uma imagem que faça com que sua família se orgulhe aqui")]
async fn hentai(ctx: &Context, msg: &Message) -> CommandResult {
    let channel = msg.channel(ctx).await;

    if let Some(Channel::Guild(channel)) = channel {
        if !channel.nsfw {
            return Ok(());
        }
    }

    let image = get_danbooru_api().get_hentai_random().await?;

    let mut embed = CreateEmbed::default();
    embed.image(image.large_file_url);
    embed.color(colors::LILAC);

    msg.channel_id
        .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
        .await?;

    Ok(())
}

#[command("hdanbooru")]
#[max_args(5)]
#[description("Consiga imagens maravilhosas diretamente do danbooru")]
#[usage("hdanbooru [tag] [tag] [tag] [tag] [tag]")]
#[example("hdanbooru love_live!")]
#[example("hdanbooru love_live! solo")]
#[example("hdanbooru love_live! solo highres")]
#[example("hdanbooru love_live! solo highres skirt")]
#[example("hdanbooru love_live! solo highres skirt kurosawa_dia")]
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
        .get_hentai_tags(
            tags.iter()
                .map(String::as_str)
                .collect::<Vec<&str>>()
                .as_slice(),
        )
        .await;

    match image {
        Ok(image) => {
            let mut embed = CreateEmbed::default();
            embed.image(image.large_file_url);
            embed.color(colors::LILAC);

            msg.channel_id
                .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                .await?;
        }
        Err(_) => {
            let mut embed = CreateEmbed::default();
            embed.title("Tags não encontradas");
            embed.color(colors::YELLOW);

            msg.channel_id
                .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                .await?;
        }
    }

    Ok(())
}

#[command("danbooru")]
#[max_args(6)]
#[description("Consiga imagens maravilhosas diretamente do danbooru")]
#[usage("danbooru [tag] [tag] [tag] [tag] [tag] [tag]")]
#[example("danbooru rating:s love_live!")]
#[example("danbooru rating:s love_live! solo")]
#[example("danbooru rating:s love_live! solo highres")]
#[example("danbooru rating:s love_live! solo highres skirt")]
#[example("danbooru rating:s love_live! solo highres skirt kurosawa_dia")]
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
        .get_tags(
            tags.iter()
                .map(String::as_str)
                .collect::<Vec<&str>>()
                .as_slice(),
        )
        .await;

    match image {
        Ok(image) => {
            let mut embed = CreateEmbed::default();
            embed.image(image.large_file_url);
            embed.color(colors::LILAC);

            msg.channel_id
                .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                .await?;
        }
        Err(_) => {
            let mut embed = CreateEmbed::default();
            embed.title("Nenhuma imagem encontrada com essas tags");
            embed.color(colors::YELLOW);

            msg.channel_id
                .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                .await?;
        }
    }

    Ok(())
}
