use self::{danbooru::client::DanbooruClient, nekoslife::client::NekosLifeClient, violet::client::VioletCLient, weeb::client::WeebClient, woof::client::WoofClient};

pub mod weeb;
pub mod nekoslife;
pub mod woof;
pub mod danbooru;
pub mod violet;

lazy_static! {
    static ref WEEB_API: WeebClient = WeebClient::default();
    static ref NEKOSLIFE_API: NekosLifeClient = NekosLifeClient::default();
    static ref WOOF_API: WoofClient = WoofClient::default();
    static ref DANBOORU_API: DanbooruClient = DanbooruClient::default();
    static ref VIOLET_API: VioletCLient = VioletCLient::default();
}

pub fn get_weeb_api() -> &'static WeebClient {
    &WEEB_API
}

pub fn get_nekoslife_api() -> &'static NekosLifeClient {
    &NEKOSLIFE_API
}

pub fn get_woof_api() -> &'static WoofClient {
    &WOOF_API
}

pub fn get_danbooru_api() -> &'static DanbooruClient {
    &DANBOORU_API
}

pub fn get_violet_api() -> &'static VioletCLient {
    &VIOLET_API
}
