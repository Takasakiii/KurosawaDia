use isahc::{AsyncReadResponseExt, HttpClient};
use serenity::framework::standard::CommandError;

use super::nekoslife_image::NekosLifeImage;

const BASE_URL: &str = "https://nekos.life/api/v2/img";

pub struct NekosLifeClient {
    client: HttpClient
}

impl NekosLifeClient {
    pub fn new() -> Self {
        let client = HttpClient::builder()
            .build()
            .expect("Falha em criar o client nekos life");

        Self {
            client
        }

    }

    pub async fn get_cat(&self) -> Result<NekosLifeImage, CommandError> {
        let result = self.client
            .get_async(format!("{}/meow", BASE_URL))
            .await;

        match result {
            Ok(mut response) => Ok(response.json().await?),
            Err(_) => Err("Falha ao pegar a imagem".into())
        }
    }
}
