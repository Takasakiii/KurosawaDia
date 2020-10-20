import { Client, Message, User } from 'discord.js'
import { IBot } from './bot'

export interface IContext {
    message: Message
    args: string[]
    client: Client
    bot: IBot
    author: User
}
