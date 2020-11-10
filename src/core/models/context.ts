import userResolver from '@bot/utils/userResolver'
import { GuildConfig } from '@server/models/guildConfig'
import { Client, DMChannel, Guild, GuildMember, Message, NewsChannel, TextChannel, User } from 'discord.js'
import { IBot } from './bot'

export class Context {
    message!: Message
    args!: string[]
    clientBot!: Client
    client!: User | null
    memberClient!: GuildMember | undefined
    bot!: IBot
    author!: User
    memberAuthor!: GuildMember | null
    channel!: TextChannel | DMChannel | NewsChannel
    guild!: Guild | null
    guildConfig!: GuildConfig

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
