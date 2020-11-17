import BotPermissionError from '@bot/errors/botPermissionError'
import ClientPermissionError from '@bot/errors/clientPermissionError'
import { CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { Context } from '@bot/models/context'
import { setLang } from '@server/functions/setLang'

@CommandName('setlang')
@CommandInfo({
    description: 'setlang',
    module: 'configs'
})
export default class SetLang extends Command {
    async validPermission (ctx: Context): Promise<boolean> {
        if (!ctx.memberClient?.permissionsIn(ctx.channel).has(['ADD_REACTIONS', 'USE_EXTERNAL_EMOJIS', 'MANAGE_MESSAGES'])) {
            throw new BotPermissionError(['ADD_REACTIONS', 'USE_EXTERNAL_EMOJIS', 'MANAGE_MESSAGES'], ctx.channel)
        }

        if (!ctx.memberAuthor?.permissionsIn(ctx.channel).has(['MANAGE_GUILD', 'ADD_REACTIONS'])) {
            throw new ClientPermissionError(['MANAGE_GUILD', 'ADD_REACTIONS'], ctx.channel)
        }

        return true
    }

    async execCommand (ctx: Context): Promise<void> {
        if (ctx.args.length > 0) {
            if (ctx.guildConfig.lang !== ctx.args[0]) {
                if (ctx.args[0] === 'pt-br' || ctx.args[0] === 'en-us') {
                    await setLang(ctx.message.id, {
                        guildId: ctx.guild?.id as string,
                        newLang: ctx.args[0]
                    })
                    ctx.channel.send('nova lingua ' + ctx.args[0])
                } else {
                    ctx.channel.send('lingua n existe')
                }
            }
        } else {
            ctx.channel.send('sem lang')
        }
    }
}
