use serenity::Client;

mod commands;
mod events;

#[tokio::main]
async fn main() {
    dotenv::dotenv().ok();

    let mut kurosawa = Client::builder(std::env::var("KUROSAWA_TOKEN").expect("Colocar o token"))
        .event_handler(events::Handler)
        .framework(commands::crete_framework())
        .await
        .expect("Fudeo");

    if let Err(err) = kurosawa.start().await {
        println!("Teve um erro {:?}", err);
    }
}
