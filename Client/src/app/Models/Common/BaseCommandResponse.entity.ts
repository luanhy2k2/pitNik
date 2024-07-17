export interface BaseCommandResponse<T> {
    id:string,
    success:boolean,
    message:string,
    errors:string[],
    object:T
}