import { Context } from '@bot/models/context'
import { Channel, PermissionString } from 'discord.js'

export abstract class BaseError extends Error {
    protected permissions: PermissionString[]
    protected channel: Channel

    constructor (permissions: PermissionString[], channel: Channel) {
        super(permissions.join(' ').toString())
        this.permissions = permissions
        this.channel = channel
    }

    abstract sendEmbed(ctx: Context): Promise<void>
}
