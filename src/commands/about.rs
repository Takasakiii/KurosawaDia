use serenity::{builder::{CreateEmbed, CreateEmbedFooter}, client::Context, framework::standard::{CommandResult, macros::{command, group}}, model::channel::Message};

use crate::utils::constants::colors;

#[group]
#[commands(sobre)]
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
