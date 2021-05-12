mod commands;
mod events;
pub mod utils;
pub mod apis;
pub mod config;

use config::get_token;
use serenity::Client;

#[tokio::main]
async fn main() {
    dotenv::dotenv().ok();

    let mut kurosawa = Client::builder(get_token())
        .event_handler(events::Handler)
        .framework(commands::crete_framework())
        .await
        .expect("Fudeo");

    if let Err(err) = kurosawa.start().await {
        println!("Teve um erro {:?}", err);
    }
}
