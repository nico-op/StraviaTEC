import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
//import { AthleteModel } from "src/app/Models/athlete-model";
import { Observable } from "rxjs";
import { Usuario } from "../../Models/Usuario";
import { Actividad } from "../../Models/Actividad";
import { UsuariosPorCarrera } from "../../Models/UsuarioPorCarrera";
import { UsuarioPorReto } from "../../Models/UsuarioPorReto";


@Injectable({
    providedIn: 'root'
})

/**
 * Post para el Api
 */
export class PostService {
    private baseURL = 'https://localhost:5001/api/';//cambiar

    constructor(private http: HttpClient) {}

    /**
     * Post el deportista
     * @param usuario
     * @returns 
     */
    loginUsuario(usuario: Usuario): Observable<any>{
        return this.http.post<any>(this.baseURL + "Usuario/login", usuario);
    }

    /**
     * Post nuevo deportista 
     * @param usuario
     * @returns 
     */
    registrarUsuario(usuario: Usuario): Observable<any>{
        return this.http.post<any>(this.baseURL + "Usuario", usuario);
    }

  
   

    /**
     * Crear actividad 
     * @param Activitidad 
     * @returns
     */
    createActivity(activitad: Actividad): Observable<any>{
        return this.http.post<any>(this.baseURL + "Activitidad", activitad);
    }

}
