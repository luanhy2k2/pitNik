export interface Account {
    id:string,
    name:string,
    address:string,
    phoneNumber:string,
    image:string,
    email:string,
    userName:string,
    birthday:Date,
    gender:Gender
}
export enum Gender{
    Male,
    Female
}
export interface GeneralInfo{
    id:number,
    userId:string,
    hobbies:string,
    education:string,
    aboutMe:string,
    workAndExperience:string
}