import { eliminateMember } from '@bot/functions/eliminateMember'
import { CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'

@CommandName('kick')
@CommandInfo({
    description: 'Expulsa um usuário.\n\n(Observação: você precisa da permissão de expulsar membros para poder usar esse comando.)',
    module: 'moderation',
    usages: [
        {
            description: 'Usuário que deseja expulsar.',
            optional: false
        },
        {
            description: 'Motivo da punição.',
            optional: true
        }
    ]
})
export default class Ban extends Command {
    async validPermission (ctx: IContext): Promise<boolean> {
        if (!ctx.memberAuthor?.hasPermission('KICK_MEMBERS')) {
            return false
        }

        if (!ctx.memberClient?.hasPermission('KICK_MEMBERS')) {
            return false
        }

        return true
    }

    async execCommand (ctx: IContext): Promise<void> {
        if (!ctx.message.mentions.members) {
            return
        }

        await eliminateMember(ctx, 'kick', ctx.args.slice(1).join(' ').toString())
    }
}
