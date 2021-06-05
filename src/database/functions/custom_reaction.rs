use mysql::{params, prelude::Queryable};
use rand::{Rng, thread_rng};
use serenity::{framework::standard::{CommandError, CommandResult}, model::guild::Guild};

use crate::database::{get_database_connection, models::custom_reaction::{DbCustomReaction, DbCustomReactionType}};

pub async fn get_custom_reaction(guild: Guild, question: &str) -> Result<Option<DbCustomReaction>, CommandError> {
    let mut conn = get_database_connection().await?;

    let mut results: Vec<DbCustomReaction> = conn.exec_map(r"
        SELECT * FROM custom_reactions cr
        WHERE 
            cr.guild_id = :guild_id and
            :question LIKE concat('%', cr.question, '%')
    ", params! {
        "guild_id" => guild.id.to_string(),
        "question" => &question
    }, |(id, question, reply, cr_type, guild_id)| {
        DbCustomReaction {
            id,
            cr_type,
            guild_id,
            question,
            reply
        }
    })?;

    if results.len() == 0 {
        return Ok(None);
    }

    loop {
        let index = thread_rng().gen_range(0..results.len());

        let custom_reaction = results.remove(index);
        
        if custom_reaction.cr_type == DbCustomReactionType::Normal as u32 {
            if custom_reaction.question == question {
                return Ok(Some(custom_reaction));
            }
        } else {
            return Ok(Some(custom_reaction));
        }

        if results.len() == 0 {
            break;
        }
    }

    Ok(None)
}

pub async fn add_custom_reaction(guild: Guild, question: String, reply: String, cr_type: DbCustomReactionType) -> CommandResult {
    let mut conn = get_database_connection().await?;

    conn.exec_drop(r"
        INSERT INTO custom_reactions (
            question,
            reply,
            cr_type,
            guild_id
        )
        VALUES (
            :question,
            :reply,
            :cr_type,
            :guild_id
        )
    ", params! {
        "question" => question,
        "reply" => reply,
        "cr_type" => u32::from(cr_type).to_string(),
        "guild_id" => guild.id.to_string()
    })?;

    Ok(())
}
