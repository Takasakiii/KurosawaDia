use isahc::{AsyncReadResponseExt, HttpClient};
use serenity::framework::standard::CommandError;

use super::woof_image::WoofImage;

const BASE_URL: &str = "https://dog.ceo/api/breeds/image/random";

pub struct WoofClient {
    client: HttpClient
}

impl WoofClient {
    pub fn default() -> Self {
        let client = HttpClient::builder()
            .build()
            .expect("Falha ao gerar o client woof");

        Self {
            client
        }
    }

    pub async fn get_random(&self) -> Result<WoofImage, CommandError> {
        let result = self.client
            .get_async(BASE_URL)
            .await;

        match result {
            Ok(mut response) => {
                Ok(response.json().await?)
            },
            Err(_) => Err("Falha ao pegar a imagem de woof".into())
        }
    }
}
