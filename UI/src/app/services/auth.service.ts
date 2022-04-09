import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  baseServerUrl="https://localhost:44386/api/Account/"

  registerUser(user: Array<String>){
    return this.http.post(this.baseServerUrl+"Register", 
    {
      Email: user[0],
      Password: user[1],
      ConfirmPassword: user[2]
    },
    {
      responseType: 'text',
    }
    );
  }

  loginUser(loginInfo: Array<String>){
    return this.http.post(this.baseServerUrl+"Login", 
    {
      Email: loginInfo[0],
      Password: loginInfo[1]
    },
    {
      responseType: 'text',
    }
    );
  }
}
