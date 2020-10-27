import { Client } from 'discord.js'
import { ICommandsInvoke } from './commandInvoke'

export interface IBot {
    client: Client
    commands: ICommandsInvoke
    uniqueCommands: ICommandsInvoke
}
