use serenity::framework::standard::CommandError;

pub mod custom_reaction;
pub mod guild;
pub mod status;
pub mod users;

type DbResult<T> = Result<T, CommandError>;
