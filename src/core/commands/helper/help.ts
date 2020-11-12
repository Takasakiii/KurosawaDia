import { CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { Context } from '@bot/models/context'
import { MessageEmbed } from 'discord.js'
import { __ } from 'i18n'
import embedConfig from '@configs/embedConfig.json'

@CommandName('help')
@CommandInfo({
    description: 'help',
    module: 'helper'
})
export default class Help extends Command {
    async execCommand (ctx: Context): Promise<void> {
        if (ctx.args.length > 0) {
            const command = ctx.bot.commands[ctx.args[0]]

            if (!command) {
                await ctx.channel.send('comando n escontrado')
                return
            }

            const embed = new MessageEmbed({
                color: embedConfig.colors.purple,
                thumbnail: {
                    url: ctx.client?.displayAvatarURL()
                }
            })
            embed.title = __({
                phrase: 'command.help.embeds.single.title',
                locale: ctx.guildConfig.lang
            }, {
                name: command.name
            })

            embed.description = __({
                phrase: Reflect.getMetadata('command:info', command.ClassDefinition).description,
                locale: ctx.guildConfig.lang
            })

            await ctx.channel.send(embed)
        } else {

        }
    }
}
