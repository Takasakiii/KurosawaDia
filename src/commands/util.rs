use rand::Rng;
use serenity::{builder::CreateEmbed, client::Context, framework::standard::{CommandResult, macros::{command, group}}, model::channel::Message};

use crate::utils::{constants::colors, user::get_user_from_id_or_mention};

#[group]
#[commands(avatar, server_image, whatsify)]
pub struct Util;

#[command]
async fn avatar(ctx: &Context, msg: &Message) -> CommandResult {
    let user = get_user_from_id_or_mention(msg, ctx).await;
    let user = match user {
        Some(user) => user,
        None => msg.author.clone()
    };

    let avatar = match user.avatar_url() {
        Some(url) => url,
        None => user.default_avatar_url()
    };

    let avatar = format!("{}?size=2048", avatar);

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

#[command]
#[aliases("copipasta", "zapironga")]
async fn whatsify(ctx: &Context, msg: &Message) -> CommandResult {
    let mut embed = CreateEmbed::default();

    let texto = msg.content.split(" ").skip(1).collect::<Vec<&str>>().join(" ");

    embed.description(format!("```{}```", texto));
    embed.color(colors::GREEN);

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    ).await?;

    Ok(())
}