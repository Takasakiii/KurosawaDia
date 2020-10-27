import { CommandAlias, CommandInfo, CommandName } from '@bot/helpers/command'
import { Command, ICommandInfo } from '@bot/models/commands'
import { IContext } from '@bot/models/context'
import { MessageEmbed } from 'discord.js'
import embedConfig from '@configs/embedConfig.json'

interface IModules {
    [keyof: string]: string
}

@CommandName('help')
@CommandAlias('ajuda', 'comandos')
@CommandInfo({
    description: 'Com esse comando eu posso te fornecer informações, como se comunicar comigo e as tarefas que realizo.',
    module: 'Help',
    usages: [
        {
            description: 'Comando que você precisa de ajuda',
            optional: true
        }
    ]
})
export default class Help extends Command {
    async execCommand (ctx: IContext): Promise<void> {
        if (!ctx.args[0]) {
            const embed = new MessageEmbed({
                title: 'Comandos atacaaaaar 😁',
                description: 'Para mais informações sobre um módulo ou comando, digite `help {Comando}` que eu lhe informarei mais sobre ele 😄',
                image: {
                    url: 'https://i.imgur.com/mQVFSrP.gif'
                },
                color: embedConfig.colors.purple
            })

            const modules: IModules = {}

            for (const key in ctx.bot.uniqueCommands) {
                const element = ctx.bot.uniqueCommands[key]

                const commandInfo = Reflect.getMetadata('command:info', element.ClassDefinition) as ICommandInfo

                if (!modules[commandInfo.module]) {
                    modules[commandInfo.module] = ''
                }

                modules[commandInfo.module] = modules[commandInfo.module].concat(`\`${key}\` `)
            }

            for (const key in modules) {
                const element = modules[key]

                embed.addField(key, element)
            }

            ctx.channel.send(embed)
        }
    }
}
