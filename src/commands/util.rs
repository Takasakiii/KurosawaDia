use rand::Rng;
use serenity::{builder::CreateEmbed, client::Context, framework::standard::{CommandResult, macros::{command, group}}, model::channel::Message};

use crate::utils::user::get_user_from_id_or_mention;

#[group]
#[commands(avatar)]
pub struct Util;

#[command]
pub async fn avatar(ctx: &Context, msg: &Message) -> CommandResult {
    let user = get_user_from_id_or_mention(msg, ctx).await;
    let user = match user {
        Some(user) => user,
        None => msg.author.clone()
    };

    let avatar = match user.avatar_url() {
        Some(avatar) => avatar,
        None => user.default_avatar_url()
    };

    let mut embed = CreateEmbed::default();
    embed.color(0x5fad47);
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