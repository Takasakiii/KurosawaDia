import { CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'

@CommandName('ban')
@CommandInfo({
    description: 'ban',
    module: 'moderation'
})
export default class Ban extends Command {
    async execCommand (ctx: IContext): Promise<void> {
        throw new Error('Method not implemented.')
    }
}
