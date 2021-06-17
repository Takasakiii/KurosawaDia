use serenity::{
    builder::{CreateEmbed, CreateEmbedFooter},
    client::Context,
    model::{
        channel::{Channel, Message},
        Permissions,
    },
};

use crate::utils::constants::{colors, emojis};

pub async fn error_permission(ctx: &Context, msg: &Message, permissions: Permissions) {
    if let Some(guild) = &msg.guild(ctx).await {
        if let (Ok(member), Some(Channel::Guild(channel))) =
            (&msg.member(ctx).await, &msg.channel(ctx).await)
        {
            if let Ok(user_perm) = guild.user_permissions_in(channel, member) {
                let has_perm = user_perm & permissions;
                let has_not_perm = !has_perm & permissions;

                let match_names = |perms: Vec<&str>| {
                    perms
                        .iter()
                        .map(|perm| match *perm {
                            "Add Reactions" => "Adicionar reações",
                            "Administrator" => "Administrador",
                            "Attach Files" => "Anexar arquivos",
                            "Ban Members" => "Banir membros",
                            "Change Nickname" => "Alterar apelido",
                            "Connect" => "Conectar",
                            "Create Invite" => "Criar convite",
                            "Deafen Members" => "Ensurdecer membros",
                            "Embed Links" => "Inserir links",
                            "Use External Emojis" => "Usar emojis externos",
                            "Kick Members" => "Expulsar membros",
                            "Manage Channels" => "Gerenciar canais",
                            "Manage Emojis" => "Gerenciar emojis",
                            "Manage Guilds" => "Gerenciar servidor",
                            "Manage Messages" => "Gerenciar mensagens",
                            "Manage Nicknames" => "Gerenciar apelidos",
                            "Manage Roles" => "Gerenciar cargos",
                            "Manage Webhooks" => "Gerenciar webhooks",
                            "Mention Everyone" => "Mencionar @everyone",
                            "Move Members" => "Mover membros",
                            "Mute Members" => "Silenciar membros",
                            "Priority Speaker" => "Voz prioritária",
                            "Read Message History" => "Ver histórico de mensagens",
                            "Request To Speak" => "Pedir para falar",
                            "Read Messages" => "Ver canais",
                            "Send Messages" => "Enviar mensagens",
                            "Send TTS Messages" => "Enviar mensagens em-texto para-voz",
                            "Speak" => "Falar",
                            "Stream" => "Video",
                            "Use Slash Commands" => "Usar comandos /",
                            "Use Voice Activity" => "Usar detecção de voz",
                            "View Audit Log" => "Ver o registro de auditoria",
                            _ => "Permissão desconhecida",
                        })
                        .collect::<Vec<&str>>()
                };

                let has_perm = match_names(has_perm.get_permission_names());
                let has_not_perm = match_names(has_not_perm.get_permission_names());

                let has_perm = has_perm.iter().fold("".to_string(), |result, item| {
                    format!("{} <:{}> {}\n", result, emojis::ENABLED, item)
                });
                let has_not_perm = has_not_perm.iter().fold("".to_string(), |result, item| {
                    format!("{} <:{}> {}\n", result, emojis::DISABLED, item)
                });

                let perms = format!("{}{}", has_perm, has_not_perm);

                let mut embed = CreateEmbed::default();
                embed.title("Por favor verifique as suas permissões");
                embed.description(perms);
                embed.color(colors::YELLOW);

                let mut footer = CreateEmbedFooter::default();
                footer.text("Todas devem estar ativas");
                embed.set_footer(footer);

                msg.channel_id
                    .send_message(ctx, |x| x.set_embed(embed).reference_message(msg))
                    .await
                    .ok();
            }
        }
    }
}
