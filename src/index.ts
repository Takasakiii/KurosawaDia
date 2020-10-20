import { config } from 'dotenv'
import { env } from 'process'
import { dabataseBot } from './database'
import { kurosawaDia } from './core'

config()

init().catch(error => {
    console.error(error)
    process.exit()
})

async function init () {
    await dabataseBot.start()

    kurosawaDia.token = env.bot_token as string
    kurosawaDia.start()
}
