use serenity::{builder::CreateEmbed, client::Context, framework::standard::{Args, CommandResult, macros::{command, group}}, model::{channel::Message, guild::Guild}};

use crate::{database::{functions::custom_reaction::add_custom_reaction, models::custom_reaction::DbCustomReactionType}, utils::constants::colors};

#[group]
#[commands(acr, aecr)]
pub struct CustomReaction;

#[command("adicionarrc")]
#[aliases("acr", "adicionarcr", "arc")]
#[only_in("guilds")]
#[min_args(3)]
async fn acr(ctx: &Context, msg: &Message, args: Args) -> CommandResult {
    let args = args.remains().unwrap().split('|').collect::<Vec<&str>>();

    if args.len() != 2 {
        return Ok(());
    }

    let guild = msg.guild(ctx).await.unwrap();

    create_custom_reaction(
        guild, 
        args[0].trim_end().to_string(), 
        args[1].trim_start().to_string(), 
        DbCustomReactionType::Normal
    ).await?;

    let mut embed = CreateEmbed::default();
    embed.title("Custom Reaction adicionada com sucesso 😃");
    embed.color(colors::ORANGE);

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    ).await?;

    Ok(())
}

#[command("adicionarrcespecial")]
#[aliases("aecr", "arce", "adicionarcrespecial")]
#[only_in("guilds")]
#[min_args(3)]
async fn aecr(ctx: &Context, msg: &Message, args: Args) -> CommandResult {
    let args = args.remains().unwrap().split('|').collect::<Vec<&str>>();

    if args.len() != 2 {
        return Ok(());
    }

    let guild = msg.guild(ctx).await.unwrap();

    create_custom_reaction(
        guild, 
        args[0].trim_end().to_string(), 
        args[1].trim_start().to_string(), 
        DbCustomReactionType::Especial
    ).await?;

    

    let mut embed = CreateEmbed::default();
    embed.title("Custom Reaction especial adicionada com sucesso 😃");
    embed.color(colors::ORANGE);

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    ).await?;

    Ok(())
}

async fn create_custom_reaction(guild: Guild, question: String, reply: String, cr_type: DbCustomReactionType) -> CommandResult {
    add_custom_reaction(guild, question, reply, cr_type).await?;

    Ok(())
}
