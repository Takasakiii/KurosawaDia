import { IContext } from './context'

export interface IUsage {
    description: string
    optional: boolean
    default: string
}

export interface ICommandInfo {
    description: string
    module: string
    usage: IUsage[]
}

export interface ICommand {
    name: string
    alias: string[]
    info: ICommandInfo

    validAuthorAndChannel(ctx: IContext): Promise<boolean>
    validPermission(ctx: IContext): Promise<boolean>
    execCommand(ctx: IContext): void
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
            usage: []
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

    abstract execCommand(ctx: IContext): void

    visible (): boolean {
        return true
    }
}
