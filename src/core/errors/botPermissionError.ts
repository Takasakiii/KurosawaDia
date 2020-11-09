import { Context } from '@bot/models/context'
import { GuildMember, MessageEmbed } from 'discord.js'
import { BaseError } from './baseError'
import embedConfig from '@configs/embedConfig.json'
import { __n } from 'i18n'
import { getMessageError } from '@bot/functions/genMessageError'

export default class BotPermissionError extends BaseError {
    async sendEmbed (ctx: Context): Promise<void> {
        const message = getMessageError(
            ctx,
            ctx.memberClient as GuildMember,
            ctx.channel,
            this.permissions
        )

        const title = __n({
            plural: 'error.message.bot',
            singular: 'error.message.bot',
            locale: ctx.guildConfig.lang
        }, this.permissions.length)

        const embed = new MessageEmbed({
            title: title,
            description: message,
            color: embedConfig.colors.orange,
            thumbnail: {
                url: ctx.client?.displayAvatarURL({ dynamic: true })
            }
        })

        await ctx.channel.send(embed)
    }
}
