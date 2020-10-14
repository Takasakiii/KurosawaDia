import { IContext } from './context'

export interface ICommandInfo {
    description: string
    module: string
    usage: string[]
}

export interface ICommand {
    name: string
    alias: string[]
    info: ICommandInfo

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

    abstract execCommand(context: IContext): void
    visible (): boolean {
        return true
    }
}
