import BotPermissionError from '@bot/errors/botPermissionError'
import ClientPermissionError from '@bot/errors/clientPermissionError'
import { CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { Context } from '@bot/models/context'

@CommandName('setlang')
@CommandInfo({
    description: 'setlang',
    module: 'configs'
})
export default class SetLang extends Command {
    async validPermission (ctx: Context): Promise<boolean> {
        if (!ctx.memberClient?.permissionsIn(ctx.channel).has('MANAGE_GUILD')) {
            throw new BotPermissionError(['MANAGE_GUILD', 'ADD_REACTIONS'], ctx.channel)
        }

        if (!ctx.memberAuthor?.permissionsIn(ctx.channel).has('MANAGE_GUILD')) {
            throw new ClientPermissionError(['MANAGE_GUILD', 'ADD_REACTIONS'], ctx.channel)
        }

        return true
    }

    async execCommand (ctx: Context): Promise<void> {
        throw new Error('Method not implemented.')
    }
}
