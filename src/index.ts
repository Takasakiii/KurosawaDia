import dotenv from 'dotenv'
import { env } from 'process'
import { dabataseBot } from './database'
import { serverBot } from './server'
import { kurosawaDia } from './core'

dotenv.config()

init().catch(error => {
    console.error(error)
    process.exit()
})

async function init () {
    await dabataseBot.start()

    serverBot.port = env.express_port as unknown as number
    serverBot.start()

    kurosawaDia.token = env.bot_token as string
    kurosawaDia.start()
}
