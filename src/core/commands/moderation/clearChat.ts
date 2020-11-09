import { CommandAlias, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'

@CommandName('clearchat')
@CommandAlias('clear', 'prune')
export default class ClearChat extends Command {
    execCommand (ctx: IContext): Promise<void> {
        throw new Error('Method not implemented.')
    }
}
