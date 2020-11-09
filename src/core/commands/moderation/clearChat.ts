import BotPermissionError from '@bot/errors/botPermissionError'
import ClientPermissionError from '@bot/errors/clientPermissionError'
import { CommandAlias, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'
import { delay } from '@utils/delay'
import { DMChannel } from 'discord.js'

@CommandName('clearchat')
@CommandAlias('clear', 'prune')
export default class ClearChat extends Command {
    async validPermission (ctx: IContext): Promise<boolean> {
        if (!ctx.memberClient?.permissionsIn(ctx.channel).has(['MANAGE_MESSAGES'])) {
            throw new BotPermissionError(['MANAGE_MESSAGES'], ctx.channel)
        }

        if (!ctx.memberAuthor?.permissionsIn(ctx.channel).has(['MANAGE_MESSAGES'])) {
            throw new ClientPermissionError(['MANAGE_MESSAGES'], ctx.channel)
        }

        return true
    }

    async execCommand (ctx: IContext): Promise<void> {
        let remaining = Number(ctx.args[0] ?? 10)

        if (!Number.isNaN(remaining)) {
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
