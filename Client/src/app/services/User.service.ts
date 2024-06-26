import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseQueriesResponse } from "../Models/Common/BaseQueriesResponse.entity";
import { Post } from "../Models/Post/Post.entity";
import { Account } from "../Models/Account/Account.entity";
const host = "https://localhost:7261"
@Injectable({
    providedIn: 'root'
})
export class UserService {
    constructor(private httpClient: HttpClient) { }
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
    login(userName: string, password: string): Observable<any> {
        return this.httpClient.post<any>(`${host}/api/Account/Login`, { userName, password })
    }
    
    getUser() {
        var userString = localStorage.getItem('user');
        return userString ? JSON.parse(userString) : null;
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
    getUserById(id: any): Observable<any> {
        return this.httpClient.get<any>(`${host}/api/Account/getById/${id}`)
    }
    deleteAccountById(id: number): Observable<any> {
        return this.httpClient.delete<any>(`${host}/api/Account/delete/${id}`)
    }
    
    updateAccount(account: any): Observable<any> {
        return this.httpClient.post<any>(`${host}/api/Account/updateUser`, account, {headers:this.addHeaderToken()})
    }
    addHeaderToken() {
        const user = this.getUser();
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${user.token}`
        });
        return headers;
    }
}