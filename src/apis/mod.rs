use self::{nekoslife::client::NekosLifeClient, weeb::client::WeebClient, woof::client::WoofClient};

pub mod weeb;
pub mod nekoslife;
pub mod woof;

static mut WEEB_API: Option<WeebClient> = None;
static mut NEKOSLIFE_API: Option<NekosLifeClient> = None;
static mut WOOF_API: Option<WoofClient> = None;

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

pub fn get_woof_api() -> &'static WoofClient {
    unsafe {
        match &WOOF_API {
            Some(woof_api) => woof_api,
            None => {
                WOOF_API = Some(WoofClient::new());
                WOOF_API.as_ref().unwrap()
            }
        }
    }
}
