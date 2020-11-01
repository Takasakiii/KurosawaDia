import { getPrefix } from '@server/functions/getPrefix'
import { Message } from 'discord.js'

export async function customPrefix (message: Message): Promise<number> {
    if (!message.guild) {
        return message.content.startsWith('~') ? 1 : -1
    } else {
        const prefix = await getPrefix(message.id, message.guild.id)

        if (message.content.startsWith(prefix)) {
            return prefix.length
        } else {
            return -1
        }
    }
}
