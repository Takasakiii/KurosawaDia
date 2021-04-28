use serenity::{client::Context, model::{channel::Message, prelude::User}};

pub async fn get_user_from_id_or_mention(msg: &Message, ctx: &Context) -> Option<User> {
    match msg.mentions.first() {
        Some(user) => Some(user.clone()),
        None => {
            let mut msg_splited = msg.content.split(" ").into_iter();
            let _ = msg_splited.next();
            let id = msg_splited.next();

            match id {
                Some(id) => {
                    let formated_id = id.parse::<u64>();

                    match formated_id {
                        Ok(id) => {
                            let user = ctx.http.get_user(id)
                                .await;
                            
                            match user {
                                Ok(user) => Some(user),
                                Err(_) => None
                            }
                        },
                        Err(_) => None
                    }
                },
                None => None
            }
        }
    }
}