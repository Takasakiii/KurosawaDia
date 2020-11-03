import BotPermissionError from '@bot/errors/botPermissionError'
import ClientPermissionError from '@bot/errors/clientPermissionError'
import { CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'
import { Message, MessageEmbed, MessageReaction, User } from 'discord.js'
import { __ } from 'i18n'
import embedConfig from '@configs/embedConfig.json'
import { setPrefix } from '@server/functions/setPrefix'

@CommandName('setprefix')
@CommandInfo({
    description: 'command.setprefix.description',
    module: 'module.configs'
})
export default class SetPrefix extends Command {
    async validPermission (ctx: IContext): Promise<boolean> {
        if (!ctx.memberClient?.permissionsIn(ctx.channel).has(['ADD_REACTIONS', 'USE_EXTERNAL_EMOJIS'])) {
            throw new BotPermissionError(['ADD_REACTIONS', 'USE_EXTERNAL_EMOJIS'], ctx.channel)
        }

        if (!ctx.memberAuthor?.permissionsIn(ctx.channel).has(['MANAGE_GUILD', 'ADD_REACTIONS'])) {
            throw new ClientPermissionError(['MANAGE_GUILD', 'ADD_REACTIONS'], ctx.channel)
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
                time: 10000,
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

            const reaction = (await embedMessage.awaitReactions((reaction: MessageReaction, user: User) => {
                return reaction.emoji.id === embedConfig.emojis.check && user.id === ctx.author.id
            }, {
                max: 1,
                time: 10000,
                errors: [
                    'time'
                ]
            })).first()

            if (!reaction) {
                return
            }

            await setPrefix(ctx.message.id, {
                guildId: ctx.guild?.id as string,
                newPrefix: message?.content as string
            })
        } catch (error) {
            await embedMessage.delete()
        }
    }
}
