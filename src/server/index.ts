import express from 'express'

class ServerBot {
    private _app: express.Express
    private _port: number

    constructor() {
        this._app = express()
        this._port = 0
    }

    public get app(): express.Express {
        return this._app
    }

    public set port(value: number) {
        this._port = value
    }

    public start() {
        this._app.listen(this._port, () => {
            console.log('Server bot listen in port: ' + this._port)
        })
    }
}

const serverBot = new ServerBot()

export { serverBot }
