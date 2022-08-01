use std::{str::FromStr, time::Duration};

use serenity::{
    builder::CreateEmbed,
    client::Context,
    framework::standard::{
        macros::{command, group},
        Args, CommandResult,
    },
    model::channel::{Message, ReactionType},
};

use crate::{
    database::functions::guild::set_prefix,
    utils::constants::{colors, emojis},
};

#[group]
#[commands(prefix)]
#[description(
    "Configurações ⚙️- Em configurações, você define preferências de como vou agir em seu servidor"
)]
pub struct Config;

#[command("prefix")]
#[aliases("setprefix")]
#[only_in("guilds")]
#[required_permissions("MANAGE_GUILD")]
#[max_args(1)]
#[min_args(1)]
#[description("Modifica o meu prefixo no servidor\n\n(Observação: você precisa da permissão de administrador ou da permissão de gerenciar servidor para poder usar esse comando)")]
#[usage("prefix <novo prefixo>")]
#[example("prefix +")]
async fn prefix(ctx: &Context, msg: &Message, mut args: Args) -> CommandResult {
    let new_prefix = args.single::<String>()?;

    if new_prefix.len() > 15 {
        return Err("Prefixo deve ser menor que 15".into());
    }

    let mut embed_confirmation = CreateEmbed::default();
    embed_confirmation.title(format!(
        "**{}**, você quer mudar o prefixo?",
        msg.author.name
    ));
    embed_confirmation.description("Se não, apenas ignore essa mensagem");
    embed_confirmation.color(colors::YELLOW);

    let msg_confirmation = msg
        .channel_id
        .send_message(ctx, |x| {
            x.set_embed(embed_confirmation).reference_message(msg)
        })
        .await?;

    msg_confirmation
        .react(ctx, ReactionType::from_str("check:770694069638135843")?)
        .await?;

    let reaction_result = msg_confirmation
        .await_reaction(ctx)
        .timeout(Duration::from_secs(10))
        .author_id(msg.author.id)
        .await;

    if let Some(reaction_action) = reaction_result {
        if reaction_action.is_added() {
            let reaction = reaction_action.as_inner_ref();

            if reaction.emoji.as_data() == emojis::CHECK {
                let guild = msg.guild_id.ok_or("Falha em pegar o guild id")?;
                let guild = guild
                    .to_guild_cached(ctx)
                    .ok_or("Falha em pegar a guild do cache")?;

                set_prefix(guild, &new_prefix).await?;

                let mut embed = CreateEmbed::default();
                embed.title(format!(
                    "{}, meu prefixo foi alterado com sucesso para `{}`!",
                    msg.author.name, new_prefix
                ));
                embed.color(colors::GREEN);

                msg_confirmation
                    .channel_id
                    .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                    .await?;
            }
        }
    }

    Ok(())
}
