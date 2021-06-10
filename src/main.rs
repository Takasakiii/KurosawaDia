use config::get_token;
use database::crate_database;
use serenity::Client;

#[macro_use]
extern crate lazy_static;

mod commands;
mod events;
pub mod utils;
pub mod apis;
pub mod config;
pub mod database;

#[tokio::main]
async fn main() {
    dotenv::dotenv().ok();

    crate_database()
        .await
        .expect("Falha em iniciar a db");

    let mut kurosawa = Client::builder(get_token())
        .event_handler(events::Handler)
        .framework(commands::crete_framework())
        .await
        .expect("Fudeo");

    if let Err(err) = kurosawa.start().await {
        println!("Teve um erro {:?}", err);
    }
}
