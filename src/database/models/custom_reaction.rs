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

impl From<u32> for DbCustomReactionType {
    fn from(el: u32) -> Self {
        match el {
            1 => DbCustomReactionType::Especial,
            _ => DbCustomReactionType::Normal
        }
    }
}

impl From<DbCustomReactionType> for u32 {
    fn from(el: DbCustomReactionType) -> Self {
        match el {
            DbCustomReactionType::Normal => 0,
            DbCustomReactionType::Especial => 1,
        }
    }
}
