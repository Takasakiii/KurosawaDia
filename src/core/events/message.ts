import { Client, Message } from 'discord.js'
import { ICommands } from '../models/commands'

export function executeCommand(message: Message, commands: ICommands, client: Client): void {
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
        client: client
    })
}
