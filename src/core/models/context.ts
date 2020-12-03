import userResolver from '@bot/utils/userResolver'
import { GuildConfig } from '@server/models/guildConfig'
import { Client, DMChannel, Guild, GuildMember, Message, NewsChannel, TextChannel, User } from 'discord.js'
import { IBot } from './bot'

class ContextArgs {
    public message!: Message
    public args!: string[]
    public clientBot!: Client
    public client!: User | null
    public memberClient!: GuildMember | undefined
    public bot!: IBot
    public author!: User
    public memberAuthor!: GuildMember | null
    public channel!: TextChannel | DMChannel | NewsChannel
    public guild!: Guild | null
    public guildConfig!: GuildConfig
}

export class Context extends ContextArgs {
    constructor (context: ContextArgs) {
        super()
        this.message = context.message
        this.args = context.args
        this.clientBot = context.clientBot
        this.client = context.client
        this.memberClient = context.memberClient
        this.bot = context.bot
        this.author = context.author
        this.memberAuthor = context.memberAuthor
        this.channel = context.channel
        this.guild = context.guild
        this.guildConfig = context.guildConfig
    }

    public getMember (resolver: string) {
        const id = userResolver(resolver)
        if (!id) {
            return
        }

        const member = this.guild?.members.cache.get(id)
        return member
    }

    public getUser (resolver: string) {
        const id = userResolver(resolver)
        if (!id) {
            return
        }

        const user = this.clientBot.users.cache.get(id)
        return user
    }
}
