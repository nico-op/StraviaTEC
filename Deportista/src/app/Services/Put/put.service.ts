import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UsuariosPorCarrera } from "../../Models/UsuarioPorCarrera";
import { Usuario } from "../../Models/Usuario";


@Injectable({
    providedIn: 'root'
})

/**
 * put para el Api
 */
export class PutService {
    
    private baseURL = 'https://serviceapistraviatec.azurewebsites.net/api/';//cambiar

    constructor(private http: HttpClient) {}
    
    /**
     * Put usuario para cambiar la informacion
     * @param usuario 
     * @returns
     */
    updateAthlete(usuario: Usuario):Observable<any>{
        let URL = this.baseURL + "Usuario";
        return this.http.put<any>(URL, usuario);
   }
}
