import { Channel, GuildMember, PermissionString } from 'discord.js'
import { __ } from 'i18n'
import embedConfig from '@configs/embedConfig.json'
import { Context } from '@bot/models/context'

export function getMessageError (ctx: Context, member: GuildMember, channel: Channel, permissions: PermissionString[]): string {
    let message = ''

    for (let i = 0; i < permissions.length; i++) {
        const permission = permissions[i]

        if (member?.permissionsIn(channel).has(permission)) {
            message += ctx.clientBot.emojis.cache.get(embedConfig.emojis.enable)?.toString()
        } else {
            message += ctx.clientBot.emojis.cache.get(embedConfig.emojis.disable)?.toString()
        }
        message += ' '
        message += __({
            phrase: 'error.permission.' + permission,
            locale: 'en-us'
        })
        message += '\n'
    }

    return message
}
