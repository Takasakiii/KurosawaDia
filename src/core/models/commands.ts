import { Context } from './context'

type types = 'Number' | 'String' | 'DiscordUser'

export interface IUsage {
    name: string
    type: types
    description: string
    optional: boolean
    default?: string
}

export interface ICommandInfo {
    description: string
    module: string
    usages?: IUsage[]
}

export interface ICommand {
    validAuthorAndChannel(ctx: Context): Promise<boolean>
    validPermission(ctx: Context): Promise<boolean>
    execCommand(ctx: Context): Promise<void>
    visible(): boolean
}

export interface ICommands {
    [keyof: string]: ICommand
}

/*
@CommandInfo({
    description: 'Sem descrição informada',
    module: 'Default'
})
*/
export abstract class Command implements ICommand {
    async validAuthorAndChannel (ctx: Context): Promise<boolean> {
        if (ctx.author.bot || !ctx.message.guild) {
            return false
        }

        return true
    }

    async validPermission (ctx: Context): Promise<boolean> {
        return true
    }

    abstract execCommand(ctx: Context): Promise<void>

    visible (): boolean {
        return true
    }
}
