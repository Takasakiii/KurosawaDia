import { sendPayload, waitPayload } from '@utils/payload'

interface RegisterIdol {
    guildId: string
    userId: string
}

export async function registerIdol (messageId: string, data: RegisterIdol): Promise<boolean> {
    sendPayload('registerIdol', messageId, data)
    return await waitPayload(messageId)
}
