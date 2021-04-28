mod config;
mod util;
mod moderation;

use serenity::framework::StandardFramework;

pub fn crete_framework() -> StandardFramework {
    StandardFramework::new()
        .configure(|x| x.prefix("k."))
        .group(&util::UTIL_GROUP)
        .group(&moderation::MODERATION_GROUP)
}