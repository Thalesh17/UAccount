import { Injectable } from '@angular/core';
import { User } from '../_models/User';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseURL = 'http://localhost:5000/api/User';

  constructor(private http: HttpClient) {
  }

  obterUsuario(id: number): Observable<User> {
    return this.http.get<User>(`${this.baseURL}/${id}`);
  }

}
