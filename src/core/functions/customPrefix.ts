import { Message } from 'discord.js'

export async function customPrefix (message: Message, prefix: string): Promise<number> {
    if (message.content.startsWith(prefix)) {
        return prefix.length
    } else {
        return -1
    }
}
