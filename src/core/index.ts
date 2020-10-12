import { Client } from 'discord.js'

class KurosawaDia {
    public client: Client
    private _token: string

    constructor() {
        this.client = new Client()
        this._token = ''
    }

    public set token(value: string) {
        if (!value) {
            console.log('token não foi definido')
            process.exit()
        }

        this._token = value
    }

    public start() : void {
        this.client.on('ready', () => {
            console.log('Bot iniciado')
        })

        this.client.login(this._token)
    }
}

const kurosawaDia = new KurosawaDia()

export { kurosawaDia }
