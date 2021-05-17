use std::env::var;

pub fn get_weeb_api_token() -> String {
    var("KUROSAWA_WEEB_API_TOKEN")
        .unwrap_or("sem token".to_string())
}

pub fn get_token() -> String {
    var("KUROSAWA_TOKEN")
        .expect("Falha ao pegar o token do bot")
}

pub fn get_database_connection_string() -> String {
    var("KUROSAWA_DATABASE_CONNECTION_STRING")
        .expect("Falha ao pegar a connection string da db")
}

pub fn get_database_name() -> String {
    var("KUROSAWA_DATABASE_NAME")
        .expect("Falha ao pegar o nome da db")
}

pub fn get_default_prefix() -> String {
    "k.".into()
}
