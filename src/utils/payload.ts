import serverSocket from '@server'

export interface IBasePayload<T> {
    messageId: string
    data: T
}

export function sendPayload<T> (event: string, messageId: string, data: T) {
    serverSocket.socket.emit(event, {
        messageId: messageId,
        data: data
    } as IBasePayload<T>)
}

export async function waitPayload<T> (messageId: string): Promise<T> {
    const payloadResult = await new Promise<T>(resolve => {
        serverSocket.socket.on(messageId, (result: T | PromiseLike<T>) => {
            resolve(result)
        })
    })
    serverSocket.socket.off(messageId)
    return payloadResult
}
