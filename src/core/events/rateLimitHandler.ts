import { RateLimitData } from 'discord.js'

export async function rateLimitHandler (info: RateLimitData) {
    const time = info.timeDifference ? info.timeDifference : info.timeout ? info.timeout : 'Unknown timeout'
    console.log('Rate limit hit ' + time)
}
