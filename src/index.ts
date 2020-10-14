import dotenv from 'dotenv'
import { env } from 'process'
import { kurosawaDia } from './core'
import { dabataseBot } from './database'
import { serverBot } from './server'

dotenv.config()

init().catch(error => {
    console.log(error)
    process.exit()
})

async function init () {
    await dabataseBot.start()

    serverBot.port = env.express_port as unknown as number
    serverBot.start()

    kurosawaDia.token = env.bot_token as string
    kurosawaDia.start()
}
