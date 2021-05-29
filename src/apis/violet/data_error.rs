use serde::Serialize;
use serde_repr::Serialize_repr;
use serenity::framework::standard::CommandError;

#[derive(Serialize)]
pub struct VioletError {
    severity: Severity,
    title: String,
    message: String,
    stacktrace: String
}

impl VioletError {
    pub fn error(why: CommandError, cmd_name: &str) -> VioletError {
        Self {
            severity: Severity::Error,
            title: format!("Command error in {}", cmd_name),
            stacktrace: format!("{:?}", why),
            message: why.to_string()
        }
    }
}

#[derive(Serialize_repr)]
#[repr(u8)]
pub enum Severity {
    NoDefined = 0,
    Severe = 1,
    Error = 2,
    Warning = 3,
    Info = 4,
    Verbose = 5
}

impl From<u8> for Severity {
    fn from(el: u8) -> Self {
        match el {
            1 => Severity::Severe,
            2 => Severity::Error,
            3 => Severity::Warning,
            4 => Severity::Info,
            5 => Severity::Verbose,
            _ => Severity::NoDefined
        }
    }
}

impl From<Severity> for u8 {
    fn from(val: Severity) -> Self {
        match val {
            Severity::NoDefined => 0,
            Severity::Severe => 1,
            Severity::Error => 2,
            Severity::Warning => 3,
            Severity::Info => 4,
            Severity::Verbose => 5
        }
    }
}
