/* eslint-disable no-undef */
import io from 'socket.io-client'

class ServerSocket {
    private _socket!: SocketIOClient.Socket
    private _url = ''
    public ping: number = 0

    start () {
        this._socket = io(this._url)

        this._socket.on('connect', () => {
            console.log('Connect to te server')
        })

        this._socket.on('reconnect', (attemptNumber: number) => {
            console.log('Reconnect: ' + attemptNumber)
        })

        this._socket.on('pong', (latency: number) => {
            this.ping = latency
        })

        this._socket.connect()
    }

    public get socket (): SocketIOClient.Socket {
        return this._socket
    }

    public set url (value: string) {
        this._url = value
    }
}

const serverSocket = new ServerSocket()

export default serverSocket
