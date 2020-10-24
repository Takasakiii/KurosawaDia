import { Guild } from '@database/models/guild'
import { Idol } from '@database/models/idol'
import { User } from '@database/models/user'

export async function registerIdol (guildDiscordId: string, userDiscordId: string): Promise<Idol> {
    let guild = await Guild.findOne({
        where: {
            discordId: guildDiscordId
        }
    })

    if (!guild) {
        guild = Guild.create({
            discordId: guildDiscordId
        })

        await Guild.save(guild)
    }

    let user = await User.findOne({
        where: {
            discordId: userDiscordId
        }
    })

    if (!user) {
        user = User.create({
            discordId: userDiscordId
        })

        await User.save(user)
    }

    let idol = await Idol.findOne({
        where: {
            guildId: guild.id,
            userId: user.id
        }
    })

    if (!idol) {
        idol = Idol.create({
            guildId: guild.id,
            userId: user.id
        })

        Idol.save(idol)
    }

    return idol
}
