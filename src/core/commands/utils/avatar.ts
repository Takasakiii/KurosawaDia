import { CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'

@CommandName('avatar')
export default class Avatar extends Command {
    async execCommand (ctx: IContext): Promise<void> {
        console.log(ctx.args[0])
        const user = ctx.clientBot.users.resolveID(ctx.args[0])
        console.log(user)
    }
}
