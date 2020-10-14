import express from 'express'

class ServerBot {
    private _app: express.Express
    private _port: number

    constructor () {
        this._app = express()
        this._port = 0
    }

    get app (): express.Express {
        return this._app
    }

    set port (value: number) {
        this._port = value
    }

    start () {
        this._app.listen(this._port, () => {
            console.log('Server bot listen in port: ' + this._port)
        })
    }
}

const serverBot = new ServerBot()

serverBot.app.get('/', (req, res) => {
    return res.status(200).send('hello world')
})

export { serverBot }
