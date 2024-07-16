import { Gender } from "./Account.entity";

export interface Register{
    name:string,
    address:string,
    phoneNumber:string,
    email:string,
    userName:string,
    password:string,
    gender:Gender,
    birthday:Date,
    image:string,
    confirmPassword:string
}
