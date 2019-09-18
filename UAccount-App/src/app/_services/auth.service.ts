import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseURL = 'http://localhost:5000/api/user/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  
  constructor(private http: HttpClient, public router : Router) { }

  login(model: any) {
    return this.http.post(`${this.baseURL}login`, model).pipe(
        map((response: any) => {
          const user = response;
          if(user) {
            localStorage.setItem('token', user.token);
            sessionStorage.setItem('user_id', user.userName.id);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            sessionStorage.setItem('user_name', this.decodedToken.unique_name);
          }else{

          }
        })
    )
  }

  register(model: any) {
      return this.http.post(`${this.baseURL}register`, model);
  }

  loggedIn() {
      const token = localStorage.getItem('token');
      return !this.jwtHelper.isTokenExpired(token);
  }
}
