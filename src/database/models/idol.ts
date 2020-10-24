import { BaseEntity, Column, Entity, ManyToOne, PrimaryGeneratedColumn } from 'typeorm'
import { Guild } from './guild'
import { User } from './user'

@Entity('idols')
export class Idol extends BaseEntity {
    @PrimaryGeneratedColumn('increment', {
        type: 'bigint',
        unsigned: true
    })
    id!: number

    @Column('bigint', {
        nullable: false,
        unsigned: true
    })
    guildId!: number

    @Column('bigint', {
        nullable: false,
        unsigned: true
    })
    userId!: number

    @ManyToOne(() => Guild, x => x.idols)
    guild!: Guild

    @ManyToOne(() => User, x => x.idols)
    user!: User
}
