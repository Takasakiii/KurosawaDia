import { IContext } from '@bot/models/context'
import { MessageEmbed } from 'discord.js'

export async function warningErrorHandler (ctx: IContext, error: Error) {
    await ctx.channel.send(new MessageEmbed({
        title: error.name,
        description: error.message
    }))
}
