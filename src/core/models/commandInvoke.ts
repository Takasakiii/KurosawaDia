export interface ICommandInvoke {
    module: string
    ClassDefinition: any,
    name: string
    alias: string[]
}

export interface ICommandsInvoke {
    [keyof: string]: ICommandInvoke
}
