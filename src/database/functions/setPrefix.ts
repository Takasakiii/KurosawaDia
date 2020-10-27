import { Guild } from '../models/guild'

export async function setPrefix (newPrefix: string, guildDiscordId: string): Promise<Guild> {
    const guild = await Guild.findOne({
        where: {
            discordId: guildDiscordId
        }
    }) as Guild

    guild.prefix = newPrefix

    await Guild.save(guild)

    return guild
}
