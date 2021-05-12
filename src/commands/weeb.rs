use rand::{Rng, thread_rng};
use serenity::{builder::CreateEmbed, client::Context, framework::standard::{Args, CommandResult, macros::{command, group}}, model::channel::Message};
use unidecode::unidecode_char;

use crate::{apis::get_weeb_api, utils::constants::colors};

#[group]
#[commands(owoify, hug)]
pub struct Weeb;

#[command("hug")]
async fn hug(ctx: &Context, msg: &Message, _args: Args) -> CommandResult {
    let api = get_weeb_api();
    let image = api.get_random("hug").await?;

    let mut embed = CreateEmbed::default();
    embed.image(image.url);

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    ).await?;
    
    Ok(())
}

#[command("owoify")]
#[aliases("furroify", "furrofy", "furrar")]
#[min_args(1)]
async fn owoify(ctx: &Context, msg: &Message, args: Args) -> CommandResult {
    let text = args.remains().unwrap();
    let chars = text.chars().collect::<Vec<char>>();
    
    if text.len() > 800 {
        return Err("Texto maior que 800".into());
    }

    let mut result = "".to_string();

    let faces = [" OwO ", " owo ", " oωo ", " òωó ", " °ω° ", " UwU ", " >w< ", " ^w^ "];
    
    let mut indexer = 0;
    
    loop {
        if chars.len() <= indexer {
            break;
        }

        let ch = chars[indexer];

        if ch == 'r' || ch == 'l' {
            result.push('w');
        } else if text.len() - indexer != 1 && (ch == 'n' || ch == 'N') {
            let next = chars[indexer + 1];
            let next_norm = unidecode_char(next)
                .chars()
                .next()
                .unwrap();
            let next_lower = next_norm
                .to_lowercase()
                .next()
                .unwrap();

            if next_lower == 'a' || next_lower == 'e' || next_lower == 'i' || next_lower == 'o' || next_lower == 'u' {
                if next_norm == next_lower {
                    result.push_str(format!("{}y", ch).as_str());
                } else {
                    result.push_str(format!("{}Y", ch).as_str());
                }
            } else {
                result.push(ch);
            }
        } else if ch == 'R' || ch == 'L' {
            result.push('W');
        } else if ch == '!' {
            if indexer != 0 && chars[indexer - 1] == '@' {
                result.push(ch);
            } else if !(text.len() - indexer != 1 && chars[indexer + 1] == '!') {
                let mut rng = thread_rng();
                result.push_str(faces[rng.gen_range(0..faces.len())]);
            }
        } else if text.len() - indexer > 2 {
            if ch == 'o' && chars[indexer + 1] == 'v' && chars[indexer + 2] == 'e' {
                result.push_str("uv");
                indexer += 2;
            } else if ch == 'O' && chars[indexer + 1] == 'V' && chars[indexer + 2] == 'E'  {
                result.push_str("UV");
                indexer += 2;
            } else {
                result.push(ch);
            }
        } else {
            result.push(ch);
        }

        indexer += 1;
    }

    let mut embed = CreateEmbed::default();
    embed.description(result);
    embed.color(colors::PINK);

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    ).await?;

    Ok(())
}