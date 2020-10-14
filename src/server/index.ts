import express, { Router } from 'express'
import { glob } from 'glob'

class ServerBot {
    private _app: express.Express
    private _port: number

    constructor () {
        this._app = express()
        this._port = 0
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
        glob('./src/server/middlewares/**/*.ts', {
            absolute: true
        }, (error, files) => {
            if (error) {
                console.error(error)
            }

            console.log('Loading middlewares')
            let i = 0
            for (const file of files) {
                const middleware = require(file).default
                this._app.use(middleware)
                i++
            }
            console.log(i + ' middlewares load')
        })
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

                if (route instanceof Router) {
                    this._app.use('/', route as Router)
                    i++
                }
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
