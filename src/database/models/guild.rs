pub struct DbGuildType {
    id: i32
}

pub struct DbGuild {
    discord_id: u64,
    name: String,
    prefix: String,
    guild_type: DbGuildType
}
