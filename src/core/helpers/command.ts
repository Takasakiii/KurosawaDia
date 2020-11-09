import { ICommandInfo } from '@bot/models/commands'

export function CommandAlias (...alias: string[]) {
    return (target: any) => {
        Reflect.defineMetadata('command:alias', alias, target)
    }
}

export function CommandName (name: string) {
    return (target: any) => {
        Reflect.defineMetadata('command:name', name, target)
    }
}

export function CommandInfo (info: ICommandInfo) {
    return (target: any) => {
        info.description = `command.${info.description}.description`
        info.module = `module.${info.module}`
        Reflect.defineMetadata('command:info', info, target)
    }
}
