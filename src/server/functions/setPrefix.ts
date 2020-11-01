import { sendPayload, waitPayload } from '@utils/payload'

interface SetPrefix {
    newPrefix: string
    guildDiscordId: string
}

export async function setPrefix (messageId: string, data: SetPrefix): Promise<string> {
    sendPayload('setPrefix', messageId, data)
    return await waitPayload(messageId)
}
