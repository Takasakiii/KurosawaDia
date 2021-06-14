use serenity::{builder::{CreateEmbed, CreateEmbedFooter}, client::Context, framework::standard::{CommandResult, macros::{command, group}}, model::channel::Message};

use crate::utils::constants::{colors, infos};

#[group]
#[commands(sobre, info)]
#[description("Ajuda ❓- Este módulo tem comandos para te ajudar na ultilização do bot")]
pub struct About;

#[command("sobre")]
async fn sobre(ctx: &Context, msg: &Message) -> CommandResult {
    let mut embed = CreateEmbed::default();
    embed.title("Será um enorme prazer te ajudar :yum:");
    embed.image("https://i.imgur.com/PC5QDiX.png");
    embed.color(colors::PURPLE);
    embed.description(r"Eu me chamo Kurosawa Dia, sou presidente do conselho de classe, idol e também ajudo as pessoas com algumas coisinhas no Discord :wink:
    Se você usar `help` no chat, vai aparecer tudo que eu posso fazer atualmente (isso não é demais :grin:)
    Sério, estou muito ansiosa para passar um tempo com você e também te ajudar XD
    Se você tem ideias de mais coisas que eu possa fazer, por favor mande uma sugestão com o `sugestao`
    Se você quiser saber mais sobre mim, me convidar para seu servidor, ou até entrar em meu servidor de suporte, use o comando `info`

    E como a Mari fala, Let's Go!!");

    let mut footer = CreateEmbedFooter::default();
    footer.icon_url("");
    footer.text("Kurosawa Dia é um projeto feito com amor e carinho pelos seus desenvolvedores!");

    embed.set_footer(footer);

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    ).await?;

    Ok(())
}

#[command("info")]
async fn info(ctx: &Context, msg: &Message) -> CommandResult {
    let mut embed = CreateEmbed::default();
    embed.title("**Dia's book:**");
    embed.thumbnail("https://i.imgur.com/L8PxTrT.jpg");
    embed.description("Espero que não faça nada estranho com minhas informações! (Tô zoando kkkkkk :stuck_out_tongue_closed_eyes:)");
    embed.image("https://i.imgur.com/qGb6xtG.jpg");
    embed.color(colors::PURPLE);

    let users = ctx.cache.user_count().await;
    let guilds = ctx.cache.guild_count().await;

    embed.field(
        "Sobre mim:",
        r"__Nome__: Kurosawa Dia (Dia-chan)
        __Aniversário__: 1° de Janeiro (Quero presentes!)
        __Ocupação__: Estudante e traficante/idol nas horas vagas",
        false);

    embed.field(
        "As pessoas que fazem tudo isso ser possível:",
        r"Jena#0439
        Yummi#4986
        LuckShiba#6614
        Vulcan#4805

        E é claro você que acredita em meu potencial :orange_heart:",
        false);

    embed.field(
        "Links úteis:",
        format!(
            "[Me adicione em seu servidor]({})\n[Entre no meu servidor para dar suporte ao projeto]({})",
            infos::CONVITE_DIA,
            infos::CONVITE_SERVER),
        false);

    // __Ping__: {ctx.Client.Ping}\n

    embed.field(
        "Informações chatas:",
        format!(
            "__Servidores__: {}\n__Usuarios__: {}\n__Versão__: {} ({})",
            guilds,
            users,
            infos::VERSION_NUMBER,
            infos::VERSION_NAME),
        false);

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    ).await?;

    Ok(())
}
