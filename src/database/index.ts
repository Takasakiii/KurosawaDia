import { Connection, createConnection } from 'typeorm'

export class DatabaseBot {
    private connection!: Connection

    async start () {
        this.connection = await createConnection()
        console.log('Database start')
    }
}
