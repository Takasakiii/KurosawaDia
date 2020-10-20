import { Message } from 'discord.js'
import { IBot } from '../models/bot'
import { ICommands } from '../models/commands'

export function executeCommand (message: Message, commands: ICommands, bot: IBot): void {
    if (!message.content.startsWith('~') || message.author.bot) {
        return
    }

    const args = message.content.slice(1).trim().split(/ +/)

    const commandName = args.shift()?.toLowerCase() as string

    if (!commandName) {
        return
    }

    const command = commands[commandName]

    if (!command) {
        return
    }

    command.execCommand({
        message: message,
        client: bot.client,
        bot: bot,
        author: message.author
    })
}
