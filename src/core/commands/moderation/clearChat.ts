import BotPermissionError from '@bot/errors/botPermissionError'
import ClientPermissionError from '@bot/errors/clientPermissionError'
import { CommandAlias, CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { Context } from '@bot/models/context'
import { delay } from '@utils/delay'
import { DiscordAPIError, DMChannel, MessageEmbed } from 'discord.js'
import { __, __n } from 'i18n'
import embedConfig from '@configs/embedConfig.json'

@CommandName('clearchat')
@CommandAlias('clear', 'prune')
@CommandInfo({
    description: 'clearchat',
    module: 'moderation'
})
export default class ClearChat extends Command {
    async validPermission (ctx: Context): Promise<boolean> {
        if (!ctx.memberClient?.permissionsIn(ctx.channel).has(['MANAGE_MESSAGES'])) {
            throw new BotPermissionError(['MANAGE_MESSAGES'], ctx.channel)
        }

        if (!ctx.memberAuthor?.permissionsIn(ctx.channel).has(['MANAGE_MESSAGES'])) {
            throw new ClientPermissionError(['MANAGE_MESSAGES'], ctx.channel)
        }

        return true
    }

    async execCommand (ctx: Context): Promise<void> {
        let remaining = Number(ctx.args[0] ?? 10)

        if (Number.isNaN(remaining)) {
            return
        }

        const amount = remaining
        let pinneds = 0

        if (ctx.channel instanceof DMChannel) {
            return
        }

        let messageId = ctx.message.id

        do {
            const amout = remaining > 99 ? 99 : remaining

            let messages = await ctx.channel.messages.fetch({
                limit: amout,
                before: messageId
            })

            if (messages.size === 0) {
                break
            }

            const messageAmout = messages.size

            messageId = messages.last()?.id as string
            messages = messages.filter(msg => !msg.pinned)

            remaining -= messages.size
            pinneds += messageAmout - messages.size

            try {
                await ctx.channel.bulkDelete(messages)
            } catch (error) {
                if (error instanceof DiscordAPIError) {
                    break
                }
            }
            await delay(2500)
        } while (remaining > 0)

        const embed = new MessageEmbed({
            thumbnail: {
                url: ctx.client?.displayAvatarURL()
            },
            color: embedConfig.colors.purple
        })
        embed.title = __({
            phrase: 'command.clearchat.embeds.info.title',
            locale: ctx.guildConfig.lang
        })

        let description = ''

        description += amount - remaining
        description += __n({
            plural: 'command.clearchat.embeds.info.deleted',
            singular: 'command.clearchat.embeds.info.deleted',
            count: amount - remaining,
            locale: ctx.guildConfig.lang
        })
        description += amount + '\n'

        if (pinneds) {
            description += pinneds
            description += __n({
                plural: 'command.clearchat.embeds.info.pinned',
                singular: 'command.clearchat.embeds.info.pinned',
                count: pinneds,
                locale: ctx.guildConfig.lang
            })
        }

        if (remaining > 0) {
            description += remaining
            description += __n({
                plural: 'command.clearchat.embeds.info.remaining',
                singular: 'command.clearchat.embeds.info.remaining',
                count: remaining,
                locale: ctx.guildConfig.lang
            })
        }

        embed.description = description

        await ctx.channel.send(embed)
    }
}
