pub struct DbGuild {
    pub discord_id: u64,
    pub name: String,
    pub prefix: String,
    pub guild_type: u32
}

pub enum DbGuildType {
    Normal = 0,
    Loli = 1,
    Owner = 2
}
