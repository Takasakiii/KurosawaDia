use crate::apis::nekoslife::client::NekosLifeClient;
use self::weeb::client::WeebClient;

pub mod weeb;
pub mod nekoslife;

static mut WEEB_API: Option<WeebClient> = None;
static mut NEKOSLIFE_API: Option<NekosLifeClient> = None;

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

pub fn get_nekoslife_api() -> &'static NekosLifeClient {
    unsafe {
        match &NEKOSLIFE_API {
            Some(nekoslife_api) => nekoslife_api,
            None => {
                NEKOSLIFE_API = Some(NekosLifeClient::new());
                NEKOSLIFE_API.as_ref().unwrap()
            }
        }
    }
}
