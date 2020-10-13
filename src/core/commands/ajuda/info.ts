import { Command } from '../../models/commands'
import { IContext } from '../../models/context'

class CommandHelp extends Command {
    name: string
    alias: string[]

    constructor() {
        super()
        this.name = 'help'
        this.alias = ['ajuda']
    }

    execCommand(context: IContext): void {
        context.message.channel.send('teste')
    }  
}

const commandHelp = new CommandHelp()

export default commandHelp

