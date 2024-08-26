import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, map } from "rxjs";
import { BaseQueriesResponse } from "../Models/Common/BaseQueriesResponse.entity";
import { Account, GeneralInfo, } from "../Models/Account/Account.entity";
import { UpdatePersionalInfor } from "../Models/Account/UpdateAccount.entity";
import { BaseCommandResponse } from "../Models/Common/BaseCommandResponse.entity";
import { Register } from "../Models/Account/Register.entity";
import { apiUrl } from "../Environments/env";
import { UserCredentials } from "../Models/Account/user-credentials";
import { ResetPassword } from "../Models/Account/ResetPassword";
@Injectable({
    providedIn: 'root'
})
export class UserService {
    constructor(private httpClient: HttpClient) {
    }
    register(user: Register): Observable<BaseCommandResponse<string>> {
        const formData: FormData = new FormData();
        formData.append('Name', user.name);
        formData.append('Address', user.address);
        formData.append('PhoneNumber', user.phoneNumber);
        formData.append('Gender', user.gender.toString());
        formData.append('Email', user.email);
        formData.append('UserName', user.userName);
        formData.append('Password', user.password);
        formData.append('ConfirmPassword', user.confirmPassword);
        formData.append('Birthday', user.birthday.toString());
        // formData.append('Image', user.image);
        return this.httpClient.post<BaseCommandResponse<string>>(`${apiUrl}/api/Account/register`, formData)
    }
    ResetPassword(resetPassword:ResetPassword): Observable<BaseCommandResponse<string>> {
        return this.httpClient.post<BaseCommandResponse<string>>(`${apiUrl}/api/Account/ResetPassword`, resetPassword);
    }
    GenerateTokenConfirmEmail(email: string): Observable<boolean> {
        return this.httpClient.get<boolean>(`${apiUrl}/api/Account/generateConfirmTokenEmail/${email}`);
    }
    GenerateTokenResetPassword(email: string) {
        return this.httpClient.post(`${apiUrl}/api/Account/GenerateTokenConfirmEmail/${email}`, {}, { responseType: 'text' });
    }
    login(userName: string, password: string) {
        return this.httpClient.post<any>(`${apiUrl}/api/Account/Login`, { userName, password })
    }
    getUser(): UserCredentials {
        const userString = localStorage.getItem('user');
        if (userString) {
            return JSON.parse(userString) as UserCredentials;
        } else {
            // Trả về một đối tượng mặc định nếu không có dữ liệu trong localStorage
            return {
                token: '',
                id: '',
                name: ''
            };
        }
    }
    getPagedData(pageIndex: number, pageSize: number, keyword: string): Observable<BaseQueriesResponse<Account>> {
        let params = new HttpParams()
            .set('PageIndex', pageIndex.toString())
            .set('PageSize', pageSize.toString());
        if (keyword) {
            params = params.set('Keyword', keyword);
        }
        return this.httpClient.get<BaseQueriesResponse<Account>>(`${apiUrl}/api/Account/GetAll`, { params});
    }
    getGeneralInfor(userId: string): Observable<GeneralInfo> {
        return this.httpClient.get<GeneralInfo>(`${apiUrl}/api/Account/GetUserInfor/${userId}`)
    }
    updateGeneralInfor(request: GeneralInfo): Observable<BaseCommandResponse<GeneralInfo>> {
        return this.httpClient.post<BaseCommandResponse<GeneralInfo>>(`${apiUrl}/api/Account/UpdateGeneralInfor`, request)
    }
    updatePersionalInfor(request: UpdatePersionalInfor): Observable<BaseCommandResponse<UpdatePersionalInfor>> {
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
        return this.httpClient.post<BaseCommandResponse<UpdatePersionalInfor>>(`${apiUrl}/api/Account/UpdatePersionalInfor`, formData)
    }
    getPersionalInfor(userId: string): Observable<Account> {
        return this.httpClient.get<Account>(`${apiUrl}/api/Account/GetById/${userId}`)
    }
    deleteAccountById(id: number): Observable<any> {
        return this.httpClient.delete<any>(`${apiUrl}/api/Account/delete/${id}`)
    }
    getImagesOfUser(userId: string, pageIndex: number, pageSize: number, keyword: string): Observable<BaseQueriesResponse<string>> {
        let params = new HttpParams()
            .set('UserId', userId)
            .set('PageIndex', pageIndex.toString())
            .set('PageSize', pageSize.toString());
        if (keyword) {
            params = params.set('Keyword', keyword);
        }
        return this.httpClient.get<BaseQueriesResponse<string>>(`${apiUrl}/api/Account/GetImageOfUser`,{params})
    }
}