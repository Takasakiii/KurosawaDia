import { BaseError } from '@bot/errors/baseError'
import { customPrefix } from '@bot/functions/customPrefix'
import { IBot } from '@bot/models/bot'
import { ICommandsInvoke } from '@bot/models/commandInvoke'
import { Command } from '@bot/models/commands'
import { Context } from '@bot/models/context'
import { registerIdol } from '@server/functions/registerIdol'
import { GuildConfig } from '@server/models/guildConfig'
import { Message } from 'discord.js'
import { errorHandler } from './errorHandler'
import { warningErrorHandler } from './warnigErrorHandler'

export async function commandHandler (message: Message, commands: ICommandsInvoke, bot: IBot): Promise<void> {
    let guildConfig = new GuildConfig()

    if (message.guild && !message.author.bot) {
        guildConfig = await registerIdol(message.id, {
            guildId: message.guild.id,
            userId: message.author.id
        })
    }

    const length = await customPrefix(message, guildConfig.prefix)

    if (length === -1) {
        return
    }

    const args = message.content.slice(length).trim().split(/ +/)

    const commandName = args.shift()?.toLowerCase()

    if (!commandName) {
        return
    }

    const invoke = commands[commandName.toLowerCase()]

    if (!invoke) {
        return
    }

    const command = new invoke.ClassDefinition() as Command

    if (!command) {
        return
    }

    const context = {
        message: message,
        args: args,
        clientBot: bot.client,
        client: bot.client.user,
        memberClient: message.guild?.members.cache.get(bot.client.user?.id as string),
        bot: bot,
        author: message.author,
        memberAuthor: message.member,
        channel: message.channel,
        guild: message.guild,
        guildConfig: guildConfig
    } as Context

    try {
        if (!await command.validAuthorAndChannel(context)) {
            return
        }

        if (!await command.validPermission(context)) {
            return
        }

        await command.execCommand(context)
    } catch (error) {
        if (error instanceof BaseError) {
            await errorHandler(context, error)
        } else {
            await warningErrorHandler(context, error)
        }
    }
}
