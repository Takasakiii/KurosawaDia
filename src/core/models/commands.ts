import { IContext } from './context'

export interface ICommands {
    [keyof: string]: ICommand
}

export interface ICommand {
    name: string
    alias: string[]
    info: ICommandInfo

    execCommand(context: IContext): void
}

export interface ICommandInfo {
    description: string
    visible: boolean
    module: string
    usage: string[]
}

export abstract class Command implements ICommand {
    abstract name: string;
    abstract alias: string[];
    info: ICommandInfo;

    constructor() {
        this.info = {
            description: 'Sem descrição informada',
            visible: true,
            module: 'Default',
            usage: []
        }
    }

    abstract execCommand(context: IContext): void
}
