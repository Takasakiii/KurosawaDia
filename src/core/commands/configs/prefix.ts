import { CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'
import { setPrefix } from '@server/functions/setPrefix'

@CommandName('prefix')
export default class Prefix extends Command {
    async execCommand (ctx: IContext): Promise<void> {
        ctx.channel.send(await setPrefix(ctx.message.id, {
            newPrefix: ctx.args[0],
            guildDiscordId: ctx.guild?.id as string
        }))
    }
}
