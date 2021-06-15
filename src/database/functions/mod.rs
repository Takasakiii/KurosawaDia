use serenity::framework::standard::CommandError;

pub mod custom_reaction;
pub mod guild;
pub mod status;

type DbResult<T> = Result<T, CommandError>;
