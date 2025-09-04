import { TodoClient } from "./generated-ts-client"

const isProduction = import.meta.env.PROD

const prod = ''
const dev = 'http://localhost:5036'

export const finalUrl = isProduction ? prod : dev
export const todoClient = new TodoClient(finalUrl)