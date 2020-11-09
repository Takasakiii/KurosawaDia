import { BaseError } from '@bot/errors/baseError'
import { Context } from '@bot/models/context'

export async function errorHandler (ctx: Context, error: BaseError) {
    await error.sendEmbed(ctx)
}
