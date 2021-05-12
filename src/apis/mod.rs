use self::weeb::client::WeebClient;

pub mod weeb;

static mut WEEB_API: Option<WeebClient> = None;

pub fn get_weeb_api() -> &'static WeebClient {
    unsafe {
        match &WEEB_API {
            Some(weeb_api) => weeb_api,
            None => {
                WEEB_API = Some(WeebClient::new());
                WEEB_API.as_ref().unwrap()
            } 
        }
    }
}