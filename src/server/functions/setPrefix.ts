import { sendPayload, waitPayload } from '@utils/payload'

interface SetPrefix {
    newPrefix: string
    guildId: string
}

export async function setPrefix (messageId: string, data: SetPrefix): Promise<string> {
    const promise = waitPayload<string>(messageId)
    sendPayload('setPrefix', messageId, data)
    return await promise
}
