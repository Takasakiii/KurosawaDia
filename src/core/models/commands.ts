import { IContext } from './context'

export interface IUsage {
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
    validAuthorAndChannel(ctx: IContext): Promise<boolean>
    validPermission(ctx: IContext): Promise<boolean>
    execCommand(ctx: IContext): Promise<void>
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
    async validAuthorAndChannel (ctx: IContext): Promise<boolean> {
        if (ctx.author.bot || !ctx.message.guild) {
            return false
        }

        return true
    }

    async validPermission (ctx: IContext): Promise<boolean> {
        return true
    }

    abstract execCommand(ctx: IContext): Promise<void>

    visible (): boolean {
        return true
    }
}
