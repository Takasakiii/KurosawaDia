use mysql::{params, prelude::Queryable};
use rand::{Rng, thread_rng};
use serenity::{framework::standard::{CommandError, CommandResult}, model::guild::Guild};

use crate::database::{get_database_connection, models::custom_reaction::{DbCustomReaction, DbCustomReactionType}};

pub async fn get_custom_reaction(guild: Guild, question: &str) -> Result<Option<DbCustomReaction>, CommandError> {
    let mut conn = get_database_connection().await?;

    let mut results: Vec<DbCustomReaction> = conn.exec_map(r"
        SELECT * FROM custom_reactions 
        WHERE 
            guild_id = :guild_id and
            :question LIKE concat('%', lower(question), '%')
    ", params! {
        "guild_id" => guild.id.to_string(),
        "question" => &question
    }, |(id, question, reply, cr_type, guild_id)| {
        let cr_type: u32 = cr_type;
        DbCustomReaction {
            id,
            cr_type: DbCustomReactionType::from(cr_type),
            guild_id,
            question,
            reply
        }
    })?;

    if results.is_empty() {
        return Ok(None);
    }

    loop {
        let index = thread_rng().gen_range(0..results.len());

        let custom_reaction = results.remove(index);
        
        if custom_reaction.cr_type == DbCustomReactionType::Normal {
            if custom_reaction.question == question {
                return Ok(Some(custom_reaction));
            }
        } else {
            return Ok(Some(custom_reaction));
        }

        if results.is_empty() {
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

pub async fn remove_custom_reaction(guild: Guild, id: u32) -> Result<bool, CommandError> {
    let mut conn = get_database_connection().await?;

    conn.exec_drop(r"
        DELETE FROM custom_reactions
        WHERE
            guild_id = :guild_id and
            id = :id
    ", params! {
        "guild_id" => guild.id.to_string(),
        "id" => id.to_string()
    })?;

    if conn.affected_rows() == 1 {
        Ok(true)
    } else {
        Ok(false)
    }
}

pub async fn list_custom_reaction(guild: &Guild, find: &str, page: u8) -> Result<Vec<DbCustomReaction>, CommandError> {
    let mut conn = get_database_connection().await?;
    let skip = page * 10;

    let results: Vec<DbCustomReaction> = if find.is_empty() {
        conn.exec_map(r"
            SELECT * FROM custom_reactions
            WHERE
                guild_id = :guild_id
            LIMIT :skip, 10
        ", params! {
            "guild_id" => guild.id.0,
            "skip" => skip
        }, |(id, question, reply, cr_type, guild_id)| {
            let cr_type: u32 = cr_type;
    
            DbCustomReaction {
                id,
                question,
                reply,
                cr_type: DbCustomReactionType::from(cr_type), 
                guild_id
            }
        })?
    } else {
        conn.exec_map(r"
            SELECT * FROM custom_reactions
            WHERE
                guild_id = :guild_id and
                reply LIKE concat('%', :find, '%')
            LIMIT :skip, 10
        ", params! {
            "guild_id" => guild.id.0,
            "find" => find,
            "skip" => skip
        }, |(id, question, reply, cr_type, guild_id)| {
            let cr_type: u32 = cr_type;
    
            DbCustomReaction {
                id,
                question,
                reply,
                cr_type: DbCustomReactionType::from(cr_type), 
                guild_id
            }
        })?
    };

    Ok(results)
}
