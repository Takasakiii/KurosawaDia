import { Client } from 'discord.js'

export default class KurosawaDia {
    private client: Client
    private token: string

    constructor(token: string) {
        if (token === undefined) {
            console.log('token não foi definido')
            process.exit()
        }

        this.client = new Client()
        this.token = token
    }

    start() : void {
        this.client.on('ready', () => {
            console.log('bot iniciado')
        })

        this.client.login(this.token)
    }
}
