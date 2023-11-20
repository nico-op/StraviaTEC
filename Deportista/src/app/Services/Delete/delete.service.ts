import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})

/**
 * Borrar para el Api
 */
export class DeleteService {

  private baseURL = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  /**
   * Borrar al deportista
   * @param DeportistaID 
   * @returns 
   */
  deleteAthlete(DeportistaID:string):Observable<any>{
    return this.http.delete<any>(this.baseURL + "Deportista/" + DeportistaID);
  }
}
