import { sendPayload, waitPayload } from '@utils/payload'

interface SetPrefix {
    newLang: string
    guildId: string
}

export async function setLang (messageId: string, data: SetPrefix): Promise<string> {
    const promise = waitPayload<string>(messageId)
    sendPayload('setLang', messageId, data)
    return await promise
}
