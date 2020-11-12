import { CommandInfo, CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { Context } from '@bot/models/context'
import userResolver from '@bot/utils/userResolver'
import { MessageEmbed } from 'discord.js'
import embedConfig from '@configs/embedConfig.json'

@CommandName('avatar')
@CommandInfo({
    description: 'avatar',
    module: 'util',
    usages: [
        {
            name: 'user',
            type: 'DiscordUser',
            description: 'user',
            optional: true
        }
    ]
})
export default class Avatar extends Command {
    async execCommand (ctx: Context): Promise<void> {
        if (ctx.args.length > 0) {
            const id = userResolver(ctx.args[0])
            if (id) {
                const user = ctx.clientBot.users.cache.get(id)

                if (!user) {
                    return
                }

                const embed = new MessageEmbed({
                    image: {
                        url: user.displayAvatarURL({
                            dynamic: true,
                            size: 1024
                        })
                    },
                    color: embedConfig.colors.purple
                })
                await ctx.channel.send(embed)
            }
        } else {
            const embed = new MessageEmbed({
                image: {
                    url: ctx.author.displayAvatarURL({
                        dynamic: true,
                        size: 1024
                    })
                },
                color: embedConfig.colors.purple
            })
            await ctx.channel.send(embed)
        }
    }
}
