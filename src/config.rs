use std::env::var;

use serenity::model::id::UserId;

pub fn get_weeb_api_token() -> String {
    var("KUROSAWA_WEEB_API_TOKEN").unwrap_or_else(|_| "sem token".to_string())
}

pub fn get_token() -> String {
    var("KUROSAWA_TOKEN").expect("Falha ao pegar o token do bot")
}

pub fn get_database_connection_string() -> String {
    var("KUROSAWA_DATABASE_CONNECTION_STRING").expect("Falha ao pegar a connection string da db")
}

pub fn get_database_name() -> String {
    var("KUROSAWA_DATABASE_NAME").expect("Falha ao pegar o nome da db")
}

pub fn get_default_prefix() -> String {
    "k.".into()
}

pub fn get_id_mention() -> Option<UserId> {
    match var("KUROSAWA_BOT_ID") {
        Ok(id) => Some(UserId(id.parse::<u64>().expect("Falha ao converter a id"))),
        Err(_) => None,
    }
}

pub fn get_danbooru_token() -> String {
    var("KUROSAWA_DANBOORU_TOKEN").expect("Falha ao pegar o token do danbooru")
}

pub fn get_violet_token() -> String {
    var("KUROSAWA_VIOLET_TOKEN").expect("Falha ao pegar o token da violet")
}
