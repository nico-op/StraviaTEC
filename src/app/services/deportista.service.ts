// deportista.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DeportistaService {
  private apiUrl = 'url_del_backend'; // Reemplazar

  constructor(private http: HttpClient) {}

  getActividades(): Observable<any> {
    return this.http.get(`${this.apiUrl}/actividades`);
  }
  
}
