use serde::Deserialize;

#[derive(Deserialize)]
pub struct DanbooruImage {
    pub large_file_url: String,
}
