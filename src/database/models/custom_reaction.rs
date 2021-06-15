pub struct DbCustomReaction {
    pub id: u32,
    pub question: String,
    pub reply: String,
    pub cr_type: DbCustomReactionType,
    pub guild_id: u64,
}

impl DbCustomReaction {
    pub fn format(&self) -> String {
        if self.cr_type == DbCustomReactionType::Especial {
            format!("{}. *{}*", self.id, self.question)
        } else {
            format!("{}. {}", self.id, self.question)
        }
    }
}

#[derive(PartialEq)]
pub enum DbCustomReactionType {
    Normal = 0,
    Especial = 1,
}

impl From<u32> for DbCustomReactionType {
    fn from(el: u32) -> Self {
        match el {
            1 => DbCustomReactionType::Especial,
            _ => DbCustomReactionType::Normal,
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
