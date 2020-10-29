import { BotBaseError } from '@bot/errors/baseError'
import { IContext } from '@bot/models/context'

export async function errorHandler (ctx: IContext, error: BotBaseError) {
    await error.sendEmbed(ctx)
}
