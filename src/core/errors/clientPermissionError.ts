import { IContext } from '@bot/models/context'
import { MessageEmbed, PermissionString } from 'discord.js'
import { BaseError } from './baseError'
import embedConfig from '@configs/embedConfig.json'
import { __n } from 'i18n'
import { getMessageError } from '@bot/functions/genMessageError'

export default class ClientPermissionError extends BaseError {
    private permissions: PermissionString[]

    constructor (permissions: PermissionString[]) {
        super(permissions.join(' ').toString())
        this.permissions = permissions
    }

    async sendEmbed (ctx: IContext): Promise<void> {
        const message = getMessageError(ctx, this.permissions, 'author')

        const title = __n({
            plural: 'error.message.client',
            singular: 'error.message.client',
            locale: ctx.guildConfig.lang
        }, this.permissions.length)

        const embed = new MessageEmbed({
            title: title,
            description: message,
            color: embedConfig.colors.orange,
            thumbnail: {
                url: ctx.memberAuthor?.user.displayAvatarURL()
            }
        })

        await ctx.channel.send(embed)
    }
}
