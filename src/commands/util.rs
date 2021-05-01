use rand::Rng;
use serenity::{builder::CreateEmbed, client::Context, framework::standard::{Args, CommandResult, macros::{command, group}}, model::{channel::Message}, utils::parse_emoji};
use unic_emoji_char::is_emoji;

use crate::utils::{constants::colors, user::get_user_from_args};

#[group]
#[commands(emoji, avatar, server_image, whatsify)]
pub struct Util;

#[command("emoji")]
#[aliases("emogi", "emote")]
async fn emoji(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let emoji_mention = args.single::<String>().unwrap();

    let emoji = parse_emoji(&emoji_mention);

    match emoji {
        Some(emoji) => {
            let link = emoji.url();
            let mut embed = CreateEmbed::default();
            embed.title(emoji.name);
            embed.description(format!("[Link direto]({})", &link));
            embed.image(&link);
            embed.color(colors::GREEN);

            msg.channel_id.send_message(ctx, |x| x
                .set_embed(embed)
                .reference_message(msg)
            ).await?;
        },
        None => {
            if emoji_mention.len() <= 8 {
                let mut chars = emoji_mention.chars();
                let mut emoji = Vec::new();
                emoji.push(chars.next().unwrap());

                if is_emoji(emoji[0]) {
                    let emoji_modify = chars.next();
                    if emoji_modify.is_some() && is_emoji(emoji_modify.unwrap()) {
                        emoji.push(emoji_modify.unwrap());
                    }

                    let utf32 = if emoji.len() == 2 {
                        format!("{:x}-{:x}", emoji[0] as u32, emoji[1] as u32)
                    } else {
                        format!("{:x}", emoji[0] as u32)
                    };

                    let link = format!("https://twemoji.maxcdn.com/2/72x72/{}.png", utf32);

                    let mut embed = CreateEmbed::default();
                    embed.title(emoji_mention);
                    embed.description(format!("[Link direto]({})", &link));
                    embed.color(colors::GREEN);
                    embed.image(&link);

                    msg.channel_id.send_message(ctx, |x| x
                        .set_embed(embed)
                        .reference_message(msg)
                    ).await?;
                }
            }
        }
    }

    Ok(())
}

#[command("avatar")]
#[aliases("uimg")]
async fn avatar(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let user = get_user_from_args(ctx, &mut args).await;
    let user = match user {
        Some(user) => user,
        None => msg.author.clone()
    };

    let avatar = match user.avatar_url() {
        Some(url) => url,
        None => user.default_avatar_url()
    };

    let mut embed = CreateEmbed::default();
    embed.color(colors::GREEN);
    embed.image(&avatar);

    let kurosawa = ctx.cache.current_user().await;

    if user.id == kurosawa.id {
        let titles = [
            "Ownt, que amor, você realmente quer me ver 😍", 
            "Assim você me deixa sem jeito 😊"
        ];

        let mut rng = rand::thread_rng();

        embed.title(titles[rng.gen_range(0..titles.len())]);
    } else {
        let titles = [
            "Nossa, que avatar bonito! Agora sei porque você queria vê-lo 🤣", 
            "Vocês são realmente criativos para avatares 😂",
            "Com um avatar assim seria um desperdício não se tornar uma idol 😃",
            "Talvez se você colocasse um filtro ficaria melhor... 🤐"
        ];

        let mut rng = rand::thread_rng();

        embed.title(titles[rng.gen_range(0..titles.len())]);
    }

    embed.description(format!("{}\n[Link direto]({})", user.tag(), &avatar));

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    ).await?;

    Ok(())
}

#[command("serverimage")]
#[aliases("simg")]
#[only_in("guilds")]
async fn server_image(ctx: &Context, msg: &Message) -> CommandResult {
    let guild = msg.guild(ctx).await.unwrap();

    let avatar = match guild.icon_url() {
        Some(url) => url,
        None => return Err("Sem imagem do servidor".into())
    };

    let avatar = format!("{}?size=2048", avatar);

    let mut embed = CreateEmbed::default();
    embed.title(guild.name);
    embed.description(format!("[Link direto]({})", avatar));
    embed.image(avatar);

    msg.channel_id.send_message(ctx, |x| x 
        .set_embed(embed)
        .reference_message(msg)
    ).await?;

    Ok(())
}

#[command("whatsify")]
#[aliases("copipasta", "zapironga")]
async fn whatsify(ctx: &Context, msg: &Message, args: Args) -> CommandResult {
    let mut embed = CreateEmbed::default();

    let texto = args.remains();

    if texto.is_none() {
        return Err("Nenhuma mensagem enviada".into());
    }

    embed.description(format!("```{}```", texto.unwrap()));
    embed.color(colors::GREEN);

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    ).await?;

    Ok(())
}