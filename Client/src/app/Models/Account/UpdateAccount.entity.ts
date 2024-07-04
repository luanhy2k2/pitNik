import { Gender } from "./Account.entity"

export interface UpdateGenerallInfor{
    id:number,
    userId:string,
    hobbies:string,
    education:string,
    aboutMe:string,
    workAndExperience:string
}
export interface UpdatePersionalInfor {
    id:string,
    name:string,
    address:string,
    phoneNumber:string,
    image:File,
    email:string,
    userName:string,
    birthDay:Date,
    gender:Gender,
    
}