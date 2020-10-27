export interface ICommandInvoke {
    ClassDefinition: any,
    name: string
    alias: string[]
}

export interface ICommandsInvoke {
    [keyof: string]: ICommandInvoke
}
