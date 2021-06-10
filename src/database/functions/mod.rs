use serenity::framework::standard::CommandError;

pub mod guild;
pub mod status;
pub mod custom_reaction;

type DbResult<T> = Result<T, CommandError>;
