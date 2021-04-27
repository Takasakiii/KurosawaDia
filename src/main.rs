use serenity::Client;

mod commands;

#[tokio::main]
async fn main() {
    dotenv::dotenv().ok();

    let mut kurosawa = Client::builder(std::env::var("KUROSAWA_TOKEN").expect("Colocar o token"))
        .framework(commands::crete_framework())
        .await
        .expect("Fudeo");

    if let Err(err) = kurosawa.start().await {
        println!("Teve um erro {:?}", err);
    }
}
