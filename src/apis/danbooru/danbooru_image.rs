use serde::Deserialize;

#[derive(Deserialize)]
pub struct DanbooruImage {
    pub large_file_url: String,
    pub file_url: String,
    pub tag_string: String,
}
