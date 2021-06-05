pub struct DbCustomReaction {
    pub id: u32,
    pub question: String,
    pub reply: String,
    pub cr_type: u32,
    pub guild_id: u64
}

pub enum DbCustomReactionType {
    Normal = 0,
    Especial = 1
}
