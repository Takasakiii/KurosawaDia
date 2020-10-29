import { IContext } from '@bot/models/context'
import { MessageEmbed, PermissionString } from 'discord.js'
import { BotBaseError } from './baseError'
import embedConfig from '@configs/embedConfig.json'
import { __ } from 'i18n'

export default class BotPermissionError extends BotBaseError {
    private permissions: PermissionString[]

    constructor (permissions: PermissionString[]) {
        super(permissions.join(' ').toString())
        this.permissions = permissions
    }

    async sendEmbed (ctx: IContext): Promise<void> {
        let message = ''

        for (let i = 0; i < this.permissions.length; i++) {
            const permission = this.permissions[i]
            message += __('error.permission.' + permission) + (i < this.permissions.length - 1 ? ', ' : ' ')
        }

        const embed = new MessageEmbed({
            title: 'O bot precisa de permissão para: ' + message,
            color: embedConfig.colors.orange
        })

        await ctx.channel.send(embed)
    }
}
