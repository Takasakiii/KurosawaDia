import { Command } from '@/core/models/commands'
import { IContext } from '@/core/models/context'
import { MessageEmbed, MessageReaction, User } from 'discord.js'
import embedConfig from '@configs/embedConfig.json'
import { setPrefix } from '@/database/functions/setPrefix'

export default class Prefix extends Command {
    name: string
    alias: string[]

    constructor () {
        super()
        this.name = 'prefix'
        this.alias = [
            'setprefix'
        ]
        this.info = {
            description: 'Modifica o meu prefixo no servidor.\n\n(Observação: você precisa da permissão de administrador ou da permissão de gerenciar servidor para poder usar esse comando.)',
            module: 'Setting',
            usages: [
                {
                    description: 'O meu novo prefixo no servidor.',
                    optional: false
                }
            ]
        }
    }

    async validPermission (ctx: IContext): Promise<boolean> {
        if (ctx.memberAuthor?.hasPermission('MANAGE_GUILD')) {
            return true
        } else {
            return false
        }
    }

    async execCommand (ctx: IContext): Promise<void> {
        if (ctx.args.length > 0 && ctx.args[0].length < 14 && ctx.args[0].length > 0) {
            const embed = new MessageEmbed({
                title: `**${ctx.memberAuthor?.displayName}**, você quer mudar o prefixo?`,
                color: embedConfig.colors.yellow
            })

            const message = await ctx.channel.send(embed)

            await message.react(embedConfig.emojis.yes)
            await message.react(embedConfig.emojis.no)

            const collector = message.createReactionCollector((reaction: MessageReaction, user: User) => {
                if (user.id !== ctx.author.id) {
                    return false
                }

                if (reaction.emoji.name !== embedConfig.emojis.yes && reaction.emoji.name !== embedConfig.emojis.no) {
                    return false
                }

                return true
            }, {
                max: 1,
                time: 5000
            })

            collector.on('collect', async (reaction) => {
                if (reaction.emoji.name === embedConfig.emojis.yes) {
                    await setPrefix(ctx.args[0], ctx.guild?.id as string)

                    embed.setTitle(`${ctx.memberAuthor?.displayName}, meu prefixo foi alterado com sucesso para \`${ctx.args[0]}\``)
                    embed.setColor(embedConfig.colors.green)

                    ctx.channel.send(embed)
                } else {
                    embed.setTitle('Operação cancelada')
                    embed.setColor(embedConfig.colors.green)

                    ctx.channel.send(embed)
                }
            })

            collector.on('end', () => {
                message.delete()
            })
        } else {
            ctx.channel.send('O prefixo deve conter no máximo 13 carateres e no minimo 1')
        }
    }
}
