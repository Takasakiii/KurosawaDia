use isahc::{prelude::*, HttpClient};
use serenity::framework::standard::CommandError;

use crate::config::KurosawaConfig;

use super::weeb_image::WeebImage;

const BASE_URL: &str = "https://api.weeb.sh";

pub struct WeebClient {
    client: HttpClient,
}

impl WeebClient {
    pub fn default() -> Self {
        let client = HttpClient::builder()
            .default_header(
                "Authorization",
                format!("Wolke {}", KurosawaConfig::get_weeb_api_token()),
            )
            .build()
            .expect("Falha ao gerar o client weeb");

        Self { client }
    }

    pub async fn get_random(&self, image_type: &str) -> Result<WeebImage, CommandError> {
        let result = self
            .client
            .get_async(format!("{}/images/random?type={}", BASE_URL, image_type))
            .await;

        match result {
            Ok(mut response) => Ok(response.json().await?),
            Err(_) => Err("Falha ao pegar a imagem de weeb".into()),
        }
    }
}
