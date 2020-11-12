import { CommandInfo, CommandName } from '@bot/helpers/command'
import { Command, ICommandInfo } from '@bot/models/commands'
import { Context } from '@bot/models/context'
import { MessageEmbed } from 'discord.js'
import { __, __n } from 'i18n'
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

            const info = Reflect.getMetadata('command:info', command.ClassDefinition) as ICommandInfo

            embed.description = __({
                phrase: info.description,
                locale: ctx.guildConfig.lang
            })

            if (info.usages) {
                let usages = ''

                for (let i = 0; i < info.usages.length; i++) {
                    const usage = info.usages[i]
                    usages += '``'
                    usages += '[' + usage.name + ']: ' + usage.type
                    if (usage.optional) {
                        usages += __({
                            phrase: 'command.help.embeds.single.vars.optional',
                            locale: ctx.guildConfig.lang
                        })
                    }
                    usages += '`` - '
                    usages += __({
                        phrase: usage.description,
                        locale: ctx.guildConfig.lang
                    })
                    usages += '\n'
                }

                embed.addField(
                    __n({
                        plural: 'command.help.embeds.single.usages',
                        singular: 'command.help.embeds.single.usages',
                        count: info.usages.length,
                        locale: ctx.guildConfig.lang
                    }),
                    usages
                )
            }

            await ctx.channel.send(embed)
        } else {

        }
    }
}
