import { IContext } from '@bot/models/context'
import { PermissionString } from 'discord.js'
import { __ } from 'i18n'
import embedConfig from '@configs/embedConfig.json'

type target = 'client' | 'author'

export function getMessageError (ctx: IContext, permissions: PermissionString[], type: target): string {
    let message = ''

    for (let i = 0; i < permissions.length; i++) {
        const permission = permissions[i]

        if (type === 'client' && ctx.memberClient?.hasPermission(permission)) {
            message += ctx.client.emojis.cache.get(embedConfig.emojis.enable)?.toString()
        } else if (type === 'author' && ctx.memberAuthor?.hasPermission(permission)) {
            message += ctx.client.emojis.cache.get(embedConfig.emojis.enable)?.toString()
        } else {
            message += ctx.client.emojis.cache.get(embedConfig.emojis.disable)?.toString()
        }

        message += __({
            phrase: 'error.permission.' + permission,
            locale: 'en-us'
        })
        message += '\n'
    }

    return message
}
