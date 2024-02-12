import { Status } from "./Status"

export interface Task
{
    id : number | undefined
    name : string
    description : string 
    statusId : number | undefined
    status : Status | undefined
}