import BotPermissionError from '@bot/errors/botPermissionError'
import ClientPermissionError from '@bot/errors/clientPermissionError'
import { CommandAlias, CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'

@CommandName('prefix')
@CommandAlias('setprefix')
@CommandInfo({
    description: 'command.prefix.description',
    module: 'module.configs'
})
export default class Prefix extends Command {
    async validPermission (ctx: IContext): Promise<boolean> {
        if (!ctx.memberClient?.hasPermission('MANAGE_GUILD')) {
            throw new BotPermissionError(['MANAGE_GUILD', 'ADD_REACTIONS', 'ATTACH_FILES', 'CONNECT'])
        }

        if (!ctx.memberAuthor?.hasPermission('MANAGE_GUILD')) {
            throw new ClientPermissionError(['MANAGE_GUILD', 'ADD_REACTIONS', 'ATTACH_FILES', 'CONNECT'])
        }

        return true
    }

    async execCommand (ctx: IContext): Promise<void> {
        ctx.channel.send('aaaaaaa')
    }
}
