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

    validAuthorAndChannel(context: IContext): boolean
    validPermission(context: IContext): boolean
    execCommand(context: IContext): void
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

    validAuthorAndChannel (context: IContext): boolean {
        if (context.author.bot || !context.message.guild) {
            return false
        }

        return true
    }

    validPermission (context: IContext): boolean {
        return true
    }

    abstract execCommand(context: IContext): void

    visible (): boolean {
        return true
    }
}
