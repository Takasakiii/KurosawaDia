import { IContext } from './context'

export interface IUsage {
    description: string
    optional: boolean
    default: string
}

export interface ICommandInfo {
    description: string
    module: string
    usages: IUsage[]
}

export interface ICommand {
    name: string
    alias: string[]
    info: ICommandInfo

    validAuthorAndChannel(ctx: IContext): Promise<boolean>
    validPermission(ctx: IContext): Promise<boolean>
    execCommand(ctx: IContext): Promise<void>
    visible(): boolean
}

export interface ICommands {
    [keyof: string]: ICommand
}

export abstract class Command implements ICommand {
    abstract name: string;
    abstract alias: string[];
    info: ICommandInfo;

    constructor () {
        this.info = {
            description: 'Sem descrição informada',
            module: 'Default',
            usages: []
        }
    }

    async validAuthorAndChannel (ctx: IContext): Promise<boolean> {
        if (ctx.author.bot || !ctx.message.guild) {
            return false
        }

        return true
    }

    async validPermission (): Promise<boolean> {
        return true
    }

    abstract execCommand(ctx: IContext): Promise<void>

    visible (): boolean {
        return true
    }
}
