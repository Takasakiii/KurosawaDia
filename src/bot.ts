import { Client } from 'discord.js'

export default class KurosawaDia {

    private client: Client
    private token: string

    constructor(token: string) {

        this.client = new Client()
        this.token = token

    }

    start() : void {

        this.client.login(this.token)

    }

}