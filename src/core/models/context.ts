import { Client, Message } from 'discord.js'

export interface IContext {
    message: Message
    client: Client
}
