use isahc::{AsyncReadResponseExt, HttpClient};
use serenity::framework::standard::CommandError;

use crate::config::get_danbooru_token;

use super::danbooru_image::DanbooruImage;

const BASE_URL: &str = "https://danbooru.donmai.us";

pub struct DanbooruClient {
    client: HttpClient
}

impl DanbooruClient {
    pub fn default() -> Self {
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

    pub async fn get_hentai_random(&self) -> Result<DanbooruImage, CommandError> {
        let result = self.client
            .get_async(format!("{}/posts/random.json?tags=rating%3Ae%20-loli%20-shota%20-toddlercon", 
                BASE_URL
            ))
            .await;

        match result {
            Ok(mut response) => Ok(response.json().await?),
            Err(_) => Err("Falha ao pegar a imagem de hentai".into())
        }
    }

    pub async fn get_hentai_tags(&self, tags: &[&str]) -> Result<DanbooruImage, CommandError> {
        let mut search_tags = vec!["rating:e"];
        search_tags.extend_from_slice(tags);

        let result = self.client
            .get_async(format!("{}/posts/random.json?{}",
                BASE_URL,
                serde_urlencoded::to_string(&[("tags", search_tags.join(" "))])?
            ))
            .await;

        match result {
            Ok(mut response) => Ok(response.json().await?),
            Err(_) => Err("Falha ao pegar a imagem de hentai".into())
        }
    }

    pub async fn get_tags(&self, tags: &[&str]) -> Result<DanbooruImage, CommandError> {
        let result = self.client
            .get_async(format!("{}/posts/random.json?{}",
                BASE_URL,
                serde_urlencoded::to_string(&[("tags", tags.join(" "))])?
            ))
            .await;

        match result {
            Ok(mut response) => Ok(response.json().await?),
            Err(_) => Err("Falha ao pegar a imagem de hentai".into())
        }
    }
}
