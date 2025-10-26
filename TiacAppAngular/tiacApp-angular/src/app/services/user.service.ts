import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5106/Person';

  constructor(private http: HttpClient) { }

  // Save user data
  createUser(userData: any): Observable<any> {
    return this.http.post(this.apiUrl, userData);
  }
}
