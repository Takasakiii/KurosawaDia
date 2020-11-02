import { IContext } from '@bot/models/context'

export abstract class BaseError extends Error {
    abstract sendEmbed(ctx: IContext): Promise<void>
}
