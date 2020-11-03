import BotPermissionError from '@bot/errors/botPermissionError'
import ClientPermissionError from '@bot/errors/clientPermissionError'
import { CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'

@CommandName('setlang')
@CommandInfo({
    description: 'command.setprefix.description',
    module: 'module.configs'
})
export default class SetLang extends Command {
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
        throw new Error('Method not implemented.')
    }
}
