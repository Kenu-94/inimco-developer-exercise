import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PersonRequest, PersonResponse } from '../models/person.model';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  // Pas deze URL aan als jouw backend op een andere poort draait
  private readonly apiUrl = 'http://localhost:5088/api/Person';

  constructor(private http: HttpClient) {}

  analyze(request: PersonRequest): Observable<PersonResponse> {
    return this.http.post<PersonResponse>(this.apiUrl, request);
  }
}