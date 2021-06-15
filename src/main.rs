use std::error::Error;

use config::get_token;
use database::crate_database;
use serenity::Client;

#[macro_use]
extern crate lazy_static;

pub mod apis;
mod commands;
pub mod config;
pub mod database;
mod events;
pub mod utils;

type KurosawaError = Box<dyn Error + Send + Sync + 'static>;
type KurosawaResult<T> = Result<T, KurosawaError>;

#[tokio::main]
async fn main() {
    dotenv::dotenv().ok();

    crate_database().await.expect("Falha em iniciar a db");

    let mut kurosawa = Client::builder(get_token())
        .event_handler(events::Handler)
        .framework(commands::crete_framework())
        .await
        .expect("Fudeo");

    if let Err(err) = kurosawa.start().await {
        println!("Teve um erro {:?}", err);
    }
}
