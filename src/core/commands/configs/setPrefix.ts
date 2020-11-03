import BotPermissionError from '@bot/errors/botPermissionError'
import ClientPermissionError from '@bot/errors/clientPermissionError'
import { CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'
import { Message, MessageEmbed } from 'discord.js'
import { __ } from 'i18n'
import embedConfig from '@configs/embedConfig.json'

@CommandName('setprefix')
@CommandInfo({
    description: 'command.setprefix.description',
    module: 'module.configs'
})
export default class SetPrefix extends Command {
    async validPermission (ctx: IContext): Promise<boolean> {
        if (!ctx.memberClient?.hasPermission('MANAGE_GUILD')) {
            throw new BotPermissionError(['MANAGE_GUILD', 'ADD_REACTIONS'])
        }

        if (!ctx.memberAuthor?.hasPermission('MANAGE_GUILD')) {
            throw new ClientPermissionError(['MANAGE_GUILD', 'ADD_REACTIONS'])
        }

        return true
    }

    async execCommand (ctx: IContext): Promise<void> {
        let embed = new MessageEmbed({
            color: embedConfig.colors.green
        })

        embed.title = __({
            phrase: 'command.setprefix.embeds.enter.title',
            locale: ctx.guildConfig.lang
        })

        embed.description = __({
            phrase: 'command.setprefix.embeds.enter.description',
            locale: ctx.guildConfig.lang
        })

        let embedMessage = await ctx.channel.send(embed)

        try {
            const message = (await ctx.channel.awaitMessages((message: Message) => {
                return message.author === ctx.author && message.content.length > 0
            }, {
                max: 1,
                time: 5000,
                errors: [
                    'time'
                ]
            })).first()

            embed = new MessageEmbed({
                color: embedConfig.colors.green
            })

            embed.title = __({
                phrase: 'command.setprefix.embeds.confirm.title',
                locale: ctx.guildConfig.lang
            })

            embed.addField(
                __({
                    phrase: 'command.setprefix.embeds.confirm.current',
                    locale: ctx.guildConfig.lang
                }),
                ctx.guildConfig.prefix,
                true
            )

            embed.addField(
                __({
                    phrase: 'command.setprefix.embeds.confirm.new',
                    locale: ctx.guildConfig.lang
                }),
                message?.content,
                true
            )

            await embedMessage.delete()
            embedMessage = await ctx.channel.send(embed)
            await embedMessage.react(embedConfig.emojis.check)
            await embedMessage.react(embedConfig.emojis.uncheck)
        } catch (error) {
            await embedMessage.delete()
        }
    }
}
