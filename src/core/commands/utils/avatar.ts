import { CommandName } from '@bot/helpers/command'
import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'
import userResolver from '@bot/utils/userResolver'
import { MessageEmbed } from 'discord.js'
import embedConfig from '@configs/embedConfig.json'

@CommandName('avatar')
export default class Avatar extends Command {
    async execCommand (ctx: IContext): Promise<void> {
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
                ctx.channel.send(embed)
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
            ctx.channel.send(embed)
        }
    }
}
