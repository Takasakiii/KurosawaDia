export function Alias (...alias: string[]) {
    return (target: any) => {
        Reflect.defineMetadata('command:alias', alias, target)
    }
}

export function Name (name: string) {
    return (target: any) => {
        Reflect.defineMetadata('command:name', name, target)
    }
}
