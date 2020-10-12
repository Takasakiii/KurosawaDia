import express from 'express'

class ServerBot {
    private app: express.Express
    private _port: number

    constructor() {
        this.app = express()
        this._port = 0
    }

    public set port(value: number) {
        this._port = value
    }

    public start() {
        this.app.listen(this._port, () => {
            console.log('Server bot listen in port: ' + this._port)
        })
    }
}

const serverBot = new ServerBot()

export { serverBot }
