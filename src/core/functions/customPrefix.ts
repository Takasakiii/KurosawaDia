import { getPrefix } from '@/database/functions/getPrefix'
import { Message } from 'discord.js'

export async function customPrefix (message: Message): Promise<number> {
    if (!message.guild) {
        return message.content.startsWith('~') ? 1 : -1
    } else {
        const idol = await getPrefix(message.guild.id, message.author.id)

        if (message.content.startsWith(idol.guild.prefix)) {
            return idol.guild.prefix.length
        } else {
            return -1
        }
    }
}
