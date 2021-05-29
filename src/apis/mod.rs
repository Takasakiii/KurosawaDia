use self::{danbooru::client::DanbooruClient, nekoslife::client::NekosLifeClient, violet::client::VioletCLient, weeb::client::WeebClient, woof::client::WoofClient};

pub mod weeb;
pub mod nekoslife;
pub mod woof;
pub mod danbooru;
pub mod violet;

static mut WEEB_API: Option<WeebClient> = None;
static mut NEKOSLIFE_API: Option<NekosLifeClient> = None;
static mut WOOF_API: Option<WoofClient> = None;
static mut DANBOORU_API: Option<DanbooruClient> = None;
static mut VIOLET_API: Option<VioletCLient> = None;

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

pub fn get_danbooru_api() -> &'static DanbooruClient {
    unsafe {
        match &DANBOORU_API {
            Some(danbooru_api) => danbooru_api,
            None => {
                DANBOORU_API = Some(DanbooruClient::new());
                DANBOORU_API.as_ref().unwrap()
            }
        }
    }
}

pub fn get_violet_api() -> &'static VioletCLient {
    unsafe {
        match &VIOLET_API {
            Some(violet_api) => violet_api,
            None => {
                VIOLET_API = Some(VioletCLient::new());
                VIOLET_API.as_ref().unwrap()
            }
        }
    }
}
