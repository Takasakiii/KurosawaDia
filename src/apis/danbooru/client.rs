use isahc::{AsyncReadResponseExt, HttpClient};
use serenity::framework::standard::CommandError;

use crate::config::get_danbooru_token;

use super::danbooru_image::DanbooruImage;

const BASE_URL: &str = "https://danbooru.donmai.us";

pub struct DanbooruClient {
    client: HttpClient
}

impl DanbooruClient {
    pub fn new() -> Self {
        let client = HttpClient::builder()
            .default_header("Authorization", format!("Basic {}",
                base64::encode(get_danbooru_token())
            ))
            .build()
            .expect("Falha ao gerar o client danbooru");

        Self {
            client
        }
    }

    pub async fn get_loli(&self) -> Result<DanbooruImage, CommandError> {
        let result = self.client
            .get_async(format!("{}/posts/random.json?tags=loli", 
                BASE_URL
            ))
            .await;

        match result {
            Ok(mut response) => Ok(response.json().await?),
            Err(_) => Err("Falha ao pegar a imagem de loli".into())
        }
    }
}
