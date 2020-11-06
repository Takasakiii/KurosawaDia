/* eslint-disable no-undef */
import io from 'socket.io-client'

class ServerSocket {
    private _socket!: SocketIOClient.Socket
    private _url = ''

    start () {
        this._socket = io(this._url)
        this._socket.on('connect', () => {
            console.log('Connect to te server')
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
