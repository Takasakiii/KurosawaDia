import { Client } from 'discord.js'
import { ICommands, Command } from './models/commands'
import glob from 'glob'
import { commandHandler } from './events/commandHandler'
import { IBot } from './models/bot'
import { exit } from 'process'

export class KurosawaDia implements IBot {
    client: Client
    private _token: string
    private _commands: ICommands

    constructor () {
        this.client = new Client()
        this._token = ''
        this._commands = {}
    }

    set token (value: string) {
        if (!value) {
            console.log('token não foi definido')
            process.exit()
        }

        this._token = value
    }

    registerCommand (command: Command): void {
        if (!this._commands[command.name]) {
            this._commands[command.name] = command
            for (const alias of command.alias) {
                if (!this._commands[alias]) {
                    this._commands[alias] = command
                } else {
                    console.log('Comando ' + alias + ' ja existe')
                    exit()
                }
            }
        } else {
            console.log('Comando ' + command.name + ' ja existe')
            exit()
        }
    }

    registerCommands (): void {
        this._commands = {}

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
                const Class = require(file).default
                const command = new Class()

                if (command instanceof Command) {
                    this.registerCommand(command)
                    i++
                }
            }
            console.log(i + ' commands load')
        })
    }

    start (): void {
        this.client.on('ready', () => {
            console.log('Bot iniciado')
        })

        this.client.on('message', async message => {
            await commandHandler(message, this._commands, this)
        })

        this.client.login(this._token)
    }
}
