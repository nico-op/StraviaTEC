import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  // Función genérica para realizar una solicitud POST a la API
  postData(url: string, data: any): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post(url, data, { headers: headers });
  }

// Función genérica para realizar una solicitud GET a la API
  getData(url: string): Observable<any> {
    return this.http.get(url);
  }

  // Función para obtener un recurso por su ID
  getById(url: string, id: number): Observable<any> {
    return this.http.get(`${url}/${id}`);
  }

  // Función para realizar una solicitud PUT a la API
  putData(url: string, id: number, data: any): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.put(`${url}/${id}`, data, { headers: headers });
  }

  // Función para realizar una solicitud DELETE a la API
  deleteData(url: string, id: number): Observable<any> {
    return this.http.delete(`${url}/${id}`);
  }

}
