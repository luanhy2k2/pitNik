import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, map } from "rxjs";
import { BaseQueriesResponse } from "../Models/Common/BaseQueriesResponse.entity";
import { Post } from "../Models/Post/Post.entity";
import { Account, GeneralInfo,  } from "../Models/Account/Account.entity";
import { UpdateGenerallInfor, UpdatePersionalInfor } from "../Models/Account/UpdateAccount.entity";
import { BaseCommandResponse } from "../Models/Common/BaseCommandResponse.entity";
const host = "https://localhost:7261"
@Injectable({
    providedIn: 'root'
})
export class UserService {
    private currentUserSubject: BehaviorSubject<any>;
    public currentUser: Observable<any>;
    constructor(private httpClient: HttpClient) { 
        const user = JSON.parse(localStorage.getItem('user') ?? '{}');
        this.currentUserSubject = new BehaviorSubject<any>(user);
        this.currentUser = this.currentUserSubject.asObservable();
    }
    
    signUp(user: any): Observable<any> {
        return this.httpClient.post(`${host}/api/account/signup`, user,{responseType: 'text' })
    }
    ResetPassword(email:string, code:string,password: string): Observable<any> {
        const request = {
            email: email,
            code: code,
            newPassword: password
        }
        return this.httpClient.post(`${host}/api/account/resetPassword`, request,{responseType: 'text' });
    }
    GenerateTokenConfirmEmail(email:string){
        return this.httpClient.post(`${host}/api/account/generateConfirmTokenEmail/${email}`, {},{responseType: 'text' });
    }
    GenerateTokenResetPassword(email:string){
        return  this.httpClient.post(`${host}/api/account/generateTokenResetpassword/${email}`, {},{responseType: 'text' });
    }
    
    // login(userName: string, password: string): Observable<any> {
    //     return this.httpClient.post<any>(`${host}/api/Account/Login`, { userName, password })
    // }
    login(userName: string, password: string) {
        return this.httpClient.post<any>(`${host}/api/Account/Login`, { userName, password })
          .pipe(map(user => {
            // Lưu trữ chi tiết người dùng và token trong local storage để giữ người dùng đăng nhập giữa các lần refresh
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSubject.next(user);
            return user;
          }));
      }
  
    getUser(): any {
        return this.currentUserSubject.value;
    }
    getPagedData(pageIndex:number,pageSize:number,keyword:string): Observable<BaseQueriesResponse<Account>> {
        let params = new HttpParams()
          .set('PageIndex', pageIndex.toString())
          .set('PageSize', pageSize.toString());
        if (keyword) {
          params = params.set('Keyword', keyword);
        }
        return this.httpClient.get<BaseQueriesResponse<Account>>(`${host}/api/Account/GetAll`, { params,headers: this.addHeaderToken()});
      }
    getGeneralInfor(userId:string): Observable<GeneralInfo> {
        return this.httpClient.get<GeneralInfo>(`${host}/api/Account/GetUserInfor/${userId}`)
    }
    updateGeneralInfor(request: GeneralInfo): Observable<BaseCommandResponse> {
        return this.httpClient.post<BaseCommandResponse>(`${host}/api/Account/UpdateGeneralInfor`, request, {headers:this.addHeaderToken()})
    }
    updatePersionalInfor(request: UpdatePersionalInfor): Observable<BaseCommandResponse> {
        const formData: FormData = new FormData();
        formData.append('Id', request.id.toString());
        formData.append('Name', request.name);
        formData.append('Address', request.address);
        formData.append('PhoneNumber', request.phoneNumber);
        formData.append('Gender', request.gender.toString());
        formData.append('Email', request.email);
        formData.append('UserName', request.userName);
        formData.append('Birthday', request.birthDay.toString());
        formData.append('Image', request.image, request.image.name);
        return this.httpClient.post<BaseCommandResponse>(`${host}/api/Account/UpdatePersionalInfor`, formData, {headers:this.addHeaderToken()})
    }
    getPersionalInfor(userId:string): Observable<Account> {
        return this.httpClient.get<Account>(`${host}/api/Account/GetById/${userId}`)
    }
    deleteAccountById(id: number): Observable<any> {
        return this.httpClient.delete<any>(`${host}/api/Account/delete/${id}`)
    }
    addHeaderToken() {
        const user = this.getUser();
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${this.getUser().token}`
        });
        return headers;
    }
}