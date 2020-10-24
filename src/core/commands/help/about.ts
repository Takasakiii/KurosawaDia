import { Command } from '@bot/models/commands'
import { IContext } from '@bot/models/context'

export default class About extends Command {
    name: string
    alias: string[]

    constructor () {
        super()
        this.name = 'about'
        this.alias = [
            'sobre'
        ]
        this.info.module = 'Help'
    }

    async execCommand (ctx: IContext): Promise<void> {
        ctx.message.channel.send('teste')
    }
}
