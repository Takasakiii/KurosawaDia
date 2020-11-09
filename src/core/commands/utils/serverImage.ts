import { CommandAlias, CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'
import { MessageEmbed } from 'discord.js'
import embedConfig from '@configs/embedConfig.json'

@CommandName('serverimage')
@CommandAlias('simg', 'simage', 'serveravatar', 'savatar')
@CommandInfo({
    description: 'serverimage',
    module: 'util'
})
export default class ServerImage extends Command {
    async execCommand (ctx: IContext): Promise<void> {
        const embed = new MessageEmbed({
            image: {
                url: ctx.guild?.iconURL({
                    dynamic: true,
                    size: 1024
                }) as string
            },
            color: embedConfig.colors.purple
        })

        await ctx.channel.send(embed)
    }
}
