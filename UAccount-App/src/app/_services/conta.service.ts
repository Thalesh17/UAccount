import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Conta } from '../_models/Conta';

@Injectable({
    providedIn: 'root'
})
export class ContaService {
    baseURL = 'http://localhost:5000/api/conta';

    constructor(private http: HttpClient) {
    }

      obterContas(id: number): Observable<Conta[]> {
        return this.http.get<Conta[]>(`${this.baseURL}/${id}`);
      }

      postConta(conta: Conta) {
        console.log(conta);
        return this.http.post<Conta>(`${this.baseURL}/`, conta);
      }
    
      putConta(conta: Conta) {
        return this.http.put<Conta>(`${this.baseURL}/${conta.id}`, conta);
      }
    
      deleteConta(id: number){
        return this.http.delete(`${this.baseURL}/${id}`);
      }

}
