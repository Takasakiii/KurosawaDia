use serde::Deserialize;
use serde_json::from_str;
use serenity::{builder::CreateEmbed, client::Context, model::channel::Message};

#[derive(Deserialize)]
pub struct EmbedAuthor {
    pub name: Option<String>,
    pub icon_url: Option<String>,
}

#[derive(Deserialize)]
pub struct EmbedFooter {
    pub text: String,
    pub icon_url: Option<String>,
}

#[derive(Deserialize)]
pub struct EmbedField {
    pub name: String,
    pub value: String,
    pub inline: Option<bool>,
}

#[derive(Deserialize)]
pub struct Embed {
    #[serde(rename = "plainText")]
    pub plain_text: Option<String>,
    pub title: Option<String>,
    pub description: Option<String>,
    pub author: Option<EmbedAuthor>,
    pub color: Option<u32>,
    pub footer: Option<EmbedFooter>,
    pub thumbnail: Option<String>,
    pub image: Option<String>,
    pub fields: Option<Vec<EmbedField>>,
}

pub enum IsEmbed {
    Embed(Embed, String),
    Message(String),
}

impl Embed {
    pub async fn from_str(ctx: &Context, msg: &Message, json_raw: &str) -> IsEmbed {
        let json = json_raw.chars().collect::<Vec<char>>();
        let mut result = "".to_string();
        let mut indexer = 0;

        'main_loop: loop {
            if json.len() == indexer {
                break;
            }

            if json[indexer] == '%' {
                let mut var_size = 0;
                let mut var_name = "".to_string();

                loop {
                    var_size += 1;

                    if json.len() == indexer + var_size {
                        result.push_str(&json_raw[indexer..json.len()]);
                        break 'main_loop;
                    }

                    if json[indexer + var_size] == '%' {
                        let var_result = Embed::switch_args(ctx, msg, &var_name).await;

                        if var_result != var_name {
                            result.push_str(&var_result);
                        } else {
                            result.push_str(&format!("%{}%", var_result));
                        }

                        indexer += var_size;
                        break;
                    } else {
                        var_name.push(json[indexer + var_size]);
                    }
                }
            } else {
                result.push(json[indexer])
            }

            indexer += 1;
        }

        if let Ok(embed) = from_str::<Embed>(&result) {
            IsEmbed::Embed(embed, result)
        } else {
            IsEmbed::Message(result)
        }
    }

    async fn switch_args(ctx: &Context, msg: &Message, arg: &str) -> String {
        match arg {
            "user" => msg.author.tag(),
            "username" => msg.author.name.to_string(),
            "usermention" => format!("<@{}>", msg.author.id),
            "id" => msg.author.id.to_string(),
            "avatar" => {
                if let Some(avatar) = msg.author.avatar_url() {
                    avatar
                } else {
                    msg.author.default_avatar_url()
                }
            }
            "membros" => {
                if let Some(guild) = msg.guild(ctx).await {
                    guild.member_count.to_string()
                } else {
                    0.to_string()
                }
            }
            "idservidor" => {
                if let Some(guild) = msg.guild(ctx).await {
                    guild.id.to_string()
                } else {
                    0.to_string()
                }
            }
            "server" => {
                if let Some(guild) = msg.guild(ctx).await {
                    guild.name
                } else {
                    "".to_string()
                }
            }
            _ => arg.to_string(),
        }
    }
}

impl From<Embed> for CreateEmbed {
    fn from(el: Embed) -> Self {
        let mut embed = CreateEmbed::default();

        if let Some(title) = el.title {
            embed.title(title);
        }

        if let Some(description) = el.description {
            embed.description(description);
        }

        if let Some(author) = el.author {
            embed.author(|a| {
                if let Some(name) = author.name {
                    a.name(name);
                }

                if let Some(icon_url) = author.icon_url {
                    a.icon_url(icon_url);
                }
                a
            });
        }

        if let Some(color) = el.color {
            embed.colour(color);
        }

        if let Some(footer) = el.footer {
            embed.footer(|f| {
                f.text(footer.text);
                if let Some(icon_url) = footer.icon_url {
                    f.icon_url(icon_url);
                }
                f
            });
        }

        if let Some(thumbnail) = el.thumbnail {
            embed.thumbnail(thumbnail);
        }

        if let Some(image) = el.image {
            embed.image(image);
        }

        if let Some(fields) = el.fields {
            for field in fields {
                embed.field(field.name, field.value, field.inline.unwrap_or(false));
            }
        }

        embed
    }
}
