import { delay } from '@bot/functions/delay'
import { CommandAlias, CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'
import { DMChannel } from 'discord.js'

@CommandName('clearchat')
@CommandAlias('limparchat', 'prune', 'clear')
@CommandInfo({
    description: 'Limpa as mensagens.\n\n(Observação: você precisa da permissão de gerenciar mensagens para poder usar esse comando.)',
    module: 'moderation',
    usages: [
        {
            description: 'Quantidade de mensagens para apagar.',
            optional: true,
            default: '10'
        }
    ]
})
export default class Clearchat extends Command {
    async validPermission (ctx: IContext): Promise<boolean> {
        if (!ctx.memberAuthor?.hasPermission('MANAGE_MESSAGES')) {
            return false
        }

        if (!ctx.memberClient?.hasPermission('MANAGE_MESSAGES')) {
            return false
        }

        return true
    }

    async execCommand (ctx: IContext): Promise<void> {
        let remaining = Number(ctx.args[0] ?? 10)

        if (Number.isNaN(remaining)) {
            await ctx.channel.send('Isso não é um numero')
            return
        }

        if (ctx.channel instanceof DMChannel) {
            return
        }

        do {
            const amout = remaining > 99 ? 99 : remaining

            remaining -= amout

            const messages = await ctx.channel.messages.fetch({
                limit: amout
            })

            if (messages.size === 0) {
                break
            }

            await ctx.channel.bulkDelete(messages)
            await delay(1000)
        } while (remaining > 0)
    }
}
