import { Client } from 'discord.js'
import { Command } from './models/commands'
import glob from 'glob'
import { commandHandler } from './events/commandHandler'
import { IBot } from './models/bot'
import { exit } from 'process'
import { ICommandInvoke, ICommandsInvoke } from './models/commandInvoke'

export class KurosawaDia implements IBot {
    readonly client: Client
    private _token: string
    commands: ICommandsInvoke
    uniqueCommands: ICommandsInvoke

    constructor () {
        this.client = new Client()
        this._token = ''
        this.commands = {}
        this.uniqueCommands = {}
    }

    set token (value: string) {
        if (!value) {
            console.log('token não foi definido')
            process.exit()
        }

        this._token = value
    }

    registerCommand (Class: any): void {
        const commandInvoke: ICommandInvoke = {
            ClassDefinition: Class,
            name: Reflect.getMetadata('command:name', Class),
            alias: Reflect.getMetadata('command:alias', Class)
        }

        if (this.commands[commandInvoke.name]) {
            console.log('Comando ' + commandInvoke.name + ' ja existe')
            exit()
        }

        this.commands[commandInvoke.name] = commandInvoke
        this.uniqueCommands[commandInvoke.name] = commandInvoke
        for (const alias of commandInvoke.alias) {
            if (!this.commands[alias]) {
                this.commands[alias] = commandInvoke
            } else {
                console.log('Comando ' + alias + ' ja existe')
                exit()
            }
        }
    }

    registerCommands (): void {
        this.commands = {}

        glob('./src/core/commands/**/*.ts', {
            absolute: true
        }, (error, files) => {
            if (error) {
                console.error(error)
                return
            }

            console.log('Loading commands')
            let i = 0
            for (const file of files) {
                const ClassDefinition = require(file).default

                if (!ClassDefinition) {
                    console.log('Commando não esta usando export default')
                    exit()
                }

                const command = new ClassDefinition()

                if (command instanceof Command) {
                    this.registerCommand(ClassDefinition)
                    i++
                }
            }
            console.log(i + ' commands load')
            console.table(this.uniqueCommands)
        })
    }

    start (): void {
        this.client.on('ready', () => {
            console.log('Bot iniciado')
        })

        this.client.on('message', async message => {
            await commandHandler(message, this.commands, this)
        })

        this.client.login(this._token)
    }
}
