use rand::{thread_rng, Rng};
use serenity::{
    builder::CreateEmbed,
    client::Context,
    framework::standard::{
        macros::{command, group},
        CommandResult,
    },
    model::channel::{Channel, Message},
};

use crate::{
    apis::{get_danbooru_api, get_nekoslife_api, get_woof_api},
    database::{functions::guild::get_db_guild, models::guild::DbGuildType},
    utils::{constants::colors, user::get_user_from_id},
};

#[group]
#[commands(cat, dog, loli)]
#[description("Image 🖼️- Esse módulo possui imagens fofinhas para agraciar seu computador")]
pub struct Image;

#[command("cat")]
#[description("Mostra uma imagem aleatória de um gato")]
async fn cat(ctx: &Context, msg: &Message) -> CommandResult {
    let api = get_nekoslife_api();
    let image = api.get_cat().await?;

    let mut embed = CreateEmbed::default();
    embed.title("Meow");
    embed.description(format!("[Link direto]({})", image.url));
    embed.image(image.url);
    embed.color(colors::TURQUOISE);

    msg.channel_id
        .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
        .await?;

    Ok(())
}

#[command("dog")]
#[description("Mostra uma imagem aleatória de um cachorro")]
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

        msg.channel_id
            .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
            .await?;
    } else {
        let user = get_user_from_id(ctx, 355750436424384524).await.ok();
        if let Some(user) = user {
            let mut embed = CreateEmbed::default();
            embed.title("Você está procurando um cachorinho? Me adote");
            embed.description(format!("Me adote no Discord, `{}`! Woof Woof", user.tag()));
            embed.thumbnail(user.avatar_url().unwrap());
            embed.color(colors::TURQUOISE);

            msg.channel_id
                .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                .await?;
        } else {
            let api = get_woof_api();
            let image = api.get_random().await?;

            let mut embed = CreateEmbed::default();
            embed.title("Woof");
            embed.description(format!("[Link direto]({})", image.message));
            embed.image(image.message);
            embed.color(colors::TURQUOISE);

            msg.channel_id
                .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                .await?;
        }
    }

    Ok(())
}

#[command("loli")]
#[only_in("guilds")]
#[help_available(false)]
#[description("Manda uma imagem para que você seja preso")]
async fn loli(ctx: &Context, msg: &Message) -> CommandResult {
    let channel = msg.channel(ctx).await;

    if let Some(Channel::Guild(channel)) = channel {
        if !channel.nsfw {
            return Ok(());
        }
    } else {
        return Ok(());
    }

    let guild = msg.guild(ctx).await.unwrap();
    let db_guild = get_db_guild(guild).await?;

    if db_guild.guild_type == DbGuildType::Normal as u32 {
        return Ok(());
    }

    let api = get_danbooru_api();
    let image = api.get_loli().await?;

    let mut embed = CreateEmbed::default();
    embed.image(image.large_file_url);
    embed.color(colors::TURQUOISE);

    msg.channel_id
        .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
        .await?;

    Ok(())
}
