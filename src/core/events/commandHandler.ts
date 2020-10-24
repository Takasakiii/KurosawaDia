import { IBot } from '@bot/models/bot'
import { ICommands } from '@bot/models/commands'
import { IContext } from '@bot/models/context'
import { registerIdol } from '@database/functions/registerIdol'
import { Message } from 'discord.js'

export async function commandHandler (message: Message, commands: ICommands, bot: IBot): Promise<void> {
    if (!message.content.startsWith('~')) return

    if (message.guild) {
        registerIdol(message.guild.id, message.author.id)
    }

    const args = message.content.slice(1).trim().split(/ +/)

    const commandName = args.shift()?.toLowerCase()

    if (!commandName) return

    const command = commands[commandName.toLowerCase()]

    if (!command) return

    const context: IContext = {
        message: message,
        args: args,
        client: bot.client,
        bot: bot,
        author: message.author
    }

    try {
        if (!await command.validAuthorAndChannel(context)) return

        if (!await command.validPermission(context)) return

        await command.execCommand(context)
    } catch (error) {
        if (error instanceof Error) {
            console.log(error)
        }
    }
}
