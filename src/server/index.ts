/* eslint-disable no-undef */
import * as io from 'socket.io-client'

class ServerSocket {
    private _socket!: SocketIOClient.Socket
    private _url = ''

    start () {
        this._socket = io.connect(this._url)
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
