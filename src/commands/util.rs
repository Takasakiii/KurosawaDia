use rand::{thread_rng, Rng};
use serenity::{
    builder::CreateEmbed,
    client::Context,
    framework::standard::{
        macros::{command, group},
        Args, CommandResult,
    },
    model::channel::{Channel, Message},
    utils::parse_emoji,
};
use unic_emoji_char::is_emoji;

use crate::utils::{channel::get_channel_from_id, constants::colors, user::get_user_from_args};

#[group]
#[commands(emoji, avatar, server_image, whatsify, suggestion, bug)]
#[description("Utilidade 🛠️- Esse módulo possui coisas úteis para o seu dia a dia")]
pub struct Util;

#[command("emoji")]
#[aliases("emogi", "emote")]
#[max_args(1)]
#[min_args(1)]
#[description("Aumenta o tamanho de um emoji e também permite você pegar a url do mesmo")]
#[usage("emoji <emoji>")]
#[example("emoji 👍")]
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

            msg.channel_id
                .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                .await?;
        }
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

                    msg.channel_id
                        .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                        .await?;
                }
            }
        }
    }

    Ok(())
}

#[command("avatar")]
#[aliases("uimg")]
#[max_args(1)]
#[min_args(1)]
#[description("Mostra o avatar de um usuário")]
#[usage("uimg <usuario>")]
#[example("uimg @Vulcan")]
#[example("uimg 203713369927057408")]
async fn avatar(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let user = get_user_from_args(ctx, &mut args).await.ok();
    let user = match user {
        Some(user) => user,
        None => msg.author.clone(),
    };

    let avatar = match user.avatar_url() {
        Some(url) => url,
        None => user.default_avatar_url(),
    };

    let mut embed = CreateEmbed::default();
    embed.color(colors::GREEN);
    embed.image(&avatar);

    let kurosawa = ctx.cache.current_user().await;

    if user.id == kurosawa.id {
        let titles = [
            "Ownt, que amor, você realmente quer me ver 😍",
            "Assim você me deixa sem jeito 😊",
        ];

        let mut rng = thread_rng();

        embed.title(titles[rng.gen_range(0..titles.len())]);
    } else {
        let titles = [
            "Nossa, que avatar bonito! Agora sei porque você queria vê-lo 🤣",
            "Vocês são realmente criativos para avatares 😂",
            "Com um avatar assim seria um desperdício não se tornar uma idol 😃",
            "Talvez se você colocasse um filtro ficaria melhor... 🤐",
        ];

        let mut rng = thread_rng();

        embed.title(titles[rng.gen_range(0..titles.len())]);
    }

    embed.description(format!("{}\n[Link direto]({})", user.tag(), &avatar));

    msg.channel_id
        .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
        .await?;

    Ok(())
}

#[command("serverimage")]
#[aliases("simg")]
#[only_in("guilds")]
#[max_args(0)]
#[description("Mostra o ícone do servidor")]
async fn server_image(ctx: &Context, msg: &Message) -> CommandResult {
    let guild = msg.guild(ctx).await.unwrap();

    let avatar = guild
        .icon_url()
        .unwrap_or_else(|| "https://cdn.discordapp.com/embed/avatars/1.png".to_string());

    let avatar = format!("{}?size=2048", avatar);

    let mut embed = CreateEmbed::default();
    embed.title(guild.name);
    embed.description(format!("[Link direto]({})", avatar));
    embed.image(avatar);

    msg.channel_id
        .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
        .await?;

    Ok(())
}

#[command("whatsify")]
#[aliases("copipasta", "zapironga")]
#[min_args(1)]
#[description("Converte um texto com emoji do Discord para um texto com emojis universais")]
#[usage("copipasta <texto>")]
#[example("copipasta Olá, meu nome é Kurosawa Dia!")]
async fn whatsify(ctx: &Context, msg: &Message, args: Args) -> CommandResult {
    let mut embed = CreateEmbed::default();

    let texto = args.remains();

    if texto.is_none() {
        return Err("Nenhuma mensagem enviada".into());
    }

    embed.description(format!("```{}```", texto.unwrap()));
    embed.color(colors::GREEN);

    msg.channel_id
        .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
        .await?;

    Ok(())
}

#[command("sugestao")]
#[min_args(1)]
#[description("Nos envie uma sugestão")]
#[usage("sugestao <sugestão>")]
#[example("sugestao ser o melhor bot de todos")]
async fn suggestion(ctx: &Context, msg: &Message, args: Args) -> CommandResult {
    let text = args.remains().unwrap();

    send_suggestion(ctx, msg, text, "Sugestão").await?;

    Ok(())
}

#[command("bug")]
#[min_args(1)]
#[description("Nos reporte um bug")]
#[usage("bug <bug>")]
#[example("sugestao o comando cat não esta funcionando")]
async fn bug(ctx: &Context, msg: &Message, args: Args) -> CommandResult {
    let text = args.remains().unwrap();

    send_suggestion(ctx, msg, text, "Bug").await?;

    Ok(())
}

async fn send_suggestion(
    ctx: &Context,
    msg: &Message,
    text: &str,
    suggestion_type: &str,
) -> CommandResult {
    let mut embed = CreateEmbed::default();
    embed.title(format!("Nova sugestão de {}", msg.author.tag()));
    embed.color(colors::LILAC);
    embed.field(suggestion_type, text, false);
    if let Some(guild) = msg.guild(ctx).await {
        embed.field("Servidor", guild.name, false);
    }
    embed.field(
        "Mais informações",
        format!("Channel: {}\nUser: {}", msg.channel_id.0, msg.author.id),
        false,
    );

    let channel = get_channel_from_id(ctx, 769583932597338112).await?;

    if let Channel::Guild(channel) = channel {
        channel.send_message(ctx, |x| x.set_embed(embed)).await?;
    }

    let mut embed = CreateEmbed::default();
    embed.description(format!(
        "{}, obrigada! Usarei isso para melhorar ❤",
        msg.author.tag()
    ));
    embed.color(colors::GREEN);

    msg.channel_id
        .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
        .await?;

    Ok(())
}
