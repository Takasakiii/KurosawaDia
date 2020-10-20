import { Client, Message, User } from 'discord.js'
import { IBot } from './bot'

export interface IContext {
    message: Message
    client: Client
    bot: IBot
    author: User
}
