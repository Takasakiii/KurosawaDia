use rand::{Rng, thread_rng};
use serenity::{builder::CreateEmbed, client::Context, framework::standard::{Args, CommandResult, macros::{command, group}}, model::channel::Message};
use unidecode::unidecode_char;

#[group]
#[commands(owoify)]
pub struct Weeb;

#[command("owoify")]
#[aliases("furroify", "furrofy", "furrar")]
#[min_args(1)]
async fn owoify(ctx: &Context, msg: &Message, args: Args) -> CommandResult {
    let text = args.remains().unwrap();
    let mut chars = text.chars();
    
    if text.len() > 800 {
        return Err("Texto maior que 800".into());
    }

    let mut result = "".to_string();

    let faces = ["OwO", "owo", "oωo", "òωó", "°ω°", "UwU", ">w<", "^w^"];
    
    let mut rng = thread_rng();

    let mut indexer = 0;

    loop {
        match chars.next() {
            Some(ch) => {
                if ch == 'r' || ch == 'l' {
                    result.push('w');
                } else if text.len() - indexer != 1 && (ch == 'n' || ch == 'N') {
                    let next = chars.nth(indexer + 1).unwrap();
                    let next = unidecode_char(next).chars().next().unwrap().to_lowercase();

                    
                } else if ch == 'R' || ch == 'L' {
                    result.push('w');
                } else if ch == '!' {
                    if indexer != 0 && chars.nth(indexer - 1).unwrap() == '@' {
                        result.push(ch);
                    } else if !(text.len() - indexer != 1 && chars.nth(indexer + 1).unwrap() == '!') {
                        result.push_str(faces[rng.gen_range(0..faces.len())]);
                    }
                } else if text.len() - indexer > 2 {
                    if ch == 'o' && chars.nth(indexer + 1).unwrap() == 'v' && chars.nth(indexer + 2).unwrap() == 'e' {
                        result.push_str("uv");
                        indexer += 1;
                    } else if ch == 'O' && chars.nth(indexer + 1).unwrap() == 'V' && chars.nth(indexer + 2).unwrap() == 'E'  {
                        result.push_str("UV");
                        indexer += 1;
                    } else {
                        result.push(ch);
                    }
                } else {
                    result.push(ch);
                }
            },
            None => {
                break;
            }
        }

        indexer += 1;
    }

    let mut embed = CreateEmbed::default();
    embed.description(result);

    msg.channel_id.send_message(ctx, |x| x
        .set_embed(embed)
        .reference_message(msg)
    );

    Ok(())
}