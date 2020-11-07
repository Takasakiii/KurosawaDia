export default function userResolver (user: string) {
    const matches = user.match(/^<@!?(\d{18})>$|^(\d{18})$/)

    if (!matches) {
        return
    }

    return matches[1] ?? matches[2]
}
