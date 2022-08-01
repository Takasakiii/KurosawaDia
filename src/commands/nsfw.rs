use serenity::{
    builder::CreateEmbed,
    client::Context,
    framework::standard::{
        macros::{command, group},
        Args, CommandResult,
    },
    model::channel::{Channel, Message},
};

use crate::{
    apis::{danbooru::danbooru_image::DanbooruImage, get_danbooru_api},
    database::{functions::guild::get_db_guild, models::guild::DbGuildType},
    utils::{constants::colors, guild::get_guild_from_id},
};

#[group]
#[commands(hentai, hdanbooru, danbooru)]
#[description("Nsfw 🔞- Esse módulo possui coisas para você dar orgulho para sua família")]
pub struct Nsfw;

#[command("hentai")]
#[description("Consiga uma imagem que faça com que sua família se orgulhe aqui")]
async fn hentai(ctx: &Context, msg: &Message) -> CommandResult {
    let channel = msg.channel(ctx).await;

    if let Ok(Channel::Guild(channel)) = channel {
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
#[max_args(6)]
#[description("Consiga imagens maravilhosas diretamente do danbooru")]
#[usage("hdanbooru [tag] [tag] [tag] [tag] [tag] [tag]")]
#[example("hdanbooru love_live!")]
#[example("hdanbooru love_live! solo")]
#[example("hdanbooru love_live! solo highres")]
#[example("hdanbooru love_live! solo highres skirt")]
#[example("hdanbooru love_live! solo highres skirt kurosawa_dia")]
async fn hdanbooru(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let channel = msg.channel(ctx).await;

    let channel = if let Ok(Channel::Guild(channel)) = channel {
        if !channel.nsfw {
            return Ok(());
        } else {
            channel
        }
    } else {
        return Ok(());
    };

    let mut tags: Vec<String> = Vec::new();

    while let Ok(arg) = args.single::<String>() {
        tags.push(arg);
    }

    let guild = get_db_guild(get_guild_from_id(ctx, channel.guild_id.0).await?).await?;

    if guild.guild_type < DbGuildType::Loli.into() {
        tags = tags
            .into_iter()
            .filter(|tag| *tag != "loli" && *tag != "shota" && *tag != "toddlercon")
            .collect::<Vec<String>>();
    }

    let mut tentativas = 5;

    loop {
        tentativas -= 1;
        if tentativas == 0 {
            send_danbooru_image(ctx, msg, &None).await?;
            break;
        }

        let image = get_danbooru_api()
            .get_hentai_tags(
                tags.iter()
                    .map(String::as_str)
                    .collect::<Vec<&str>>()
                    .as_slice(),
            )
            .await?;

        let tags = &image.tag_string.split(' ').collect::<Vec<&str>>();

        if !tags.contains(&"loli") && !tags.contains(&"shota") && !tags.contains(&"toddlercon") {
            send_danbooru_image(ctx, msg, &Some(image)).await?;
            break;
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

    let channel = if let Ok(Channel::Guild(channel)) = channel {
        if !channel.nsfw {
            return Ok(());
        } else {
            channel
        }
    } else {
        return Ok(());
    };

    let mut tags: Vec<String> = Vec::new();

    while let Ok(arg) = args.single::<String>() {
        tags.push(arg);
    }

    let guild = get_db_guild(get_guild_from_id(ctx, channel.guild_id.0).await?).await?;

    if guild.guild_type < 1 {
        tags = tags
            .into_iter()
            .filter(|tag| *tag != "loli" && *tag != "shota" && *tag != "toddlercon")
            .collect::<Vec<String>>();
    }

    let mut tentativas = 5;

    loop {
        tentativas -= 1;
        if tentativas == 0 {
            send_danbooru_image(ctx, msg, &None).await?;
            break;
        }

        let image = get_danbooru_api()
            .get_tags(
                tags.iter()
                    .map(String::as_str)
                    .collect::<Vec<&str>>()
                    .as_slice(),
            )
            .await?;

        let tags = &image.tag_string.split(' ').collect::<Vec<&str>>();

        if !tags.contains(&"loli") && !tags.contains(&"shota") && !tags.contains(&"toddlercon") {
            send_danbooru_image(ctx, msg, &Some(image)).await?;
            break;
        }
    }

    Ok(())
}

async fn send_danbooru_image(
    ctx: &Context,
    msg: &Message,
    image: &Option<DanbooruImage>,
) -> CommandResult {
    match image {
        Some(image) => {
            let mut embed = CreateEmbed::default();
            embed.image(&image.file_url);
            embed.color(colors::LILAC);
            embed.description(format!("[Link da imagem]({})", image.file_url));

            msg.channel_id
                .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                .await?;
        }
        None => {
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
