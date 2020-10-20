import { Message } from 'discord.js'
import { IBot } from '../models/bot'
import { ICommands } from '../models/commands'
import { IContext } from '../models/context'

export function commandHandler (message: Message, commands: ICommands, bot: IBot): void {
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

    const context = {
        message: message,
        client: bot.client,
        bot: bot,
        author: message.author
    } as IContext

    if (!command.validPermission(context)) return

    command.execCommand(context)
}
