import { Message } from 'discord.js'
import { IBot } from '../models/bot'
import { ICommands } from '../models/commands'
import { IContext } from '../models/context'

export function commandHandler (message: Message, commands: ICommands, bot: IBot): void {
    if (!message.content.startsWith('~')) {
        return
    }

    const args = message.content.slice(1).trim().split(/ +/)

    const commandName = args.shift()?.toLowerCase()

    if (!commandName) {
        return
    }

    const command = commands[commandName.toLowerCase()]

    if (!command) {
        return
    }

    const context: IContext = {
        message: message,
        args: args,
        client: bot.client,
        bot: bot,
        author: message.author
    }

    if (!command.validAuthorAndChannel(context)) return

    if (!command.validPermission(context)) return

    command.execCommand(context)
}
