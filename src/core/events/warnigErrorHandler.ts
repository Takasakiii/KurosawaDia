import { Context } from '@bot/models/context'
import { MessageEmbed } from 'discord.js'

export async function warningErrorHandler (ctx: Context, error: Error) {
    await ctx.channel.send(new MessageEmbed({
        title: error.name,
        description: error.message
    }))
}
