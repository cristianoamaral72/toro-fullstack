import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Account } from 'app/Model/Account';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

   constructor(private http: HttpClient) {}
   
 private apiUrl = environment.apiUrl;

  getAccount() {
   return this.http.get<Account>(`${this.apiUrl}/account`);
  }

}
