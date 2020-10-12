import { Connection, createConnection } from 'typeorm'

class DabataseBot {
    private connection!: Connection

    public async start() {
        this.connection = await createConnection()
        console.log('Database start')
    }
}

const dabataseBot = new DabataseBot()

export { dabataseBot }
