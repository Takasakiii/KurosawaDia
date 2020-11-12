import { ICommandInfo } from '@bot/models/commands'
import { __ } from 'i18n'
import { env } from 'process'

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
        const infoPath = `command.${info.description}.info`
        info.description = infoPath + '.description'
        info.module = `module.${info.module}`
        if (env.IS_DEV) {
            __(info.description)
            __(info.module)
            if (info.usages) {
                for (let i = 0; i < info.usages.length; i++) {
                    const usage = info.usages[i]
                    usage.description = infoPath + '.usages.' + usage.description
                    __(usage.description)
                }
            }
        }
        Reflect.defineMetadata('command:info', info, target)
    }
}
