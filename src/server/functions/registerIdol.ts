import { GuildConfig } from '@server/models/guildConfig'
import { sendPayload, waitPayload } from '@utils/payload'

interface RegisterIdol {
    guildId: string
    userId: string
}

export async function registerIdol (messageId: string, data: RegisterIdol): Promise<GuildConfig> {
    const promise = waitPayload<GuildConfig>(messageId)
    sendPayload('registerIdol', messageId, data)
    return await promise
}
