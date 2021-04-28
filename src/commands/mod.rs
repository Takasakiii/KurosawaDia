mod config;
mod util;

use serenity::framework::StandardFramework;

pub fn crete_framework() -> StandardFramework {
    StandardFramework::new()
        .configure(|x| x.prefix("k."))
        .group(&util::UTIL_GROUP)
}