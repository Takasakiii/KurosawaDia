use std::env::var;

pub fn get_weeb_api_token() -> String {
    var("KUROSAWA_WEEB_API_TOKEN")
        .unwrap_or("sem token".to_string())
}

pub fn get_token() -> String {
    var("KUROSAWA_TOKEN")
        .expect("Falha ao pegar o token do bot")
}