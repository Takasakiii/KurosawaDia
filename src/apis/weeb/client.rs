use isahc::HttpClient;

pub struct WeebClient{
    client: HttpClient
}

impl WeebClient {
    pub fn new() -> Self {
        let client = HttpClient::builder()
            .default_header("Authorization", "")
            .build()
            .expect("Falha ao gerar o client weeb");

        Self {
            client
        }
    }
}