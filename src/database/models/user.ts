import { BaseEntity, Column, Entity, Index, OneToMany, PrimaryGeneratedColumn } from 'typeorm'
import { Idol } from './idol'

@Entity('users')
export class User extends BaseEntity {
    @PrimaryGeneratedColumn('increment', {
        type: 'bigint',
        unsigned: true
    })
    id!: number

    @Column('varchar', {
        length: 64,
        nullable: false
    })
    @Index({ unique: true })
    discordId!: string

    @OneToMany(() => Idol, x => x.guild)
    idols!: Idol[]
}
