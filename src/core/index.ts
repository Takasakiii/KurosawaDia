import { Client } from 'discord.js'
import { ICommands, Command } from './models/commands'
import glob from 'glob'
import { executeCommand } from './events/message'

class KurosawaDia {
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

    registerCommands (): void {
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
                const command = require(file).default

                if (command instanceof Command) {
                    this._commands[command.name] = command
                    for (const alia of command.alias) {
                        this._commands[alia] = command
                    }
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

        this.client.on('message', message => {
            executeCommand(message, this._commands, this.client)
        })

        this.client.login(this._token)
    }
}

const kurosawaDia = new KurosawaDia()
kurosawaDia.registerCommands()

export { kurosawaDia }
