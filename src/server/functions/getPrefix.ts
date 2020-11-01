import { sendPayload, waitPayload } from '@utils/payload'

export async function getPrefix (messageId: string, guildId: string): Promise<string> {
    sendPayload('getPrefix', messageId, guildId)
    return await waitPayload(messageId)
}
