import express, { json } from 'express'
import { glob } from 'glob'

class ServerBot {
    private _app: express.Express
    private _port: number

    constructor () {
        this._app = express()
        this._port = 5000
        this.addMiddlewares()
        this.addRoutes()
    }

    get app (): express.Express {
        return this._app
    }

    set port (value: number) {
        this._port = value
    }

    private addMiddlewares () {
        this._app.use(json())
    }

    private addRoutes () {
        glob('./src/server/routes/**/*.ts', {
            absolute: true
        }, (error, files) => {
            if (error) {
                console.error(error)
            }

            console.log('Loading routes')
            let i = 0
            for (const file of files) {
                const route = require(file).default

                this._app.use('/', route)
                i++
            }
            console.log(i + ' routes load')
        })
    }

    start () {
        this._app.listen(this._port, () => {
            console.log('Server bot listen in port: ' + this._port)
        })
    }
}

const serverBot = new ServerBot()

export { serverBot }
