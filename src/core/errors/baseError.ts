import { IContext } from '@bot/models/context'

export abstract class BotBaseError extends Error {
    abstract sendEmbed(ctx: IContext): Promise<void>
}
