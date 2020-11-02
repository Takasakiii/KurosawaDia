import { BaseError } from '@bot/errors/baseError'
import { IContext } from '@bot/models/context'

export async function errorHandler (ctx: IContext, error: BaseError) {
    await error.sendEmbed(ctx)
}
