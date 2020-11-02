import { GuildConfig } from '@server/models/guildConfig'
import { Client, DMChannel, Guild, GuildMember, Message, NewsChannel, TextChannel, User } from 'discord.js'
import { IBot } from './bot'

export interface IContext {
    message: Message
    args: string[]
    clientBot: Client
    memberClient: GuildMember | undefined
    bot: IBot
    author: User
    memberAuthor: GuildMember | null
    channel: TextChannel | DMChannel | NewsChannel,
    guild: Guild | null,
    guildConfig: GuildConfig
}
