import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})

/**
 * Get para el Api
 */
export class GetService {

    private baseURL = 'https://serviceapistraviatec.azurewebsites.net/api/';//cambiar

    constructor(private http: HttpClient) {}

    /**
     * get para obtener todos los deportistas
     * @param UsuarioID //usario del deportista
     * @returns //devuelve un array de deportistas
     */
    getAthlete(UsuarioID:string):Observable<any>{
        return this.http.get<any>(this.baseURL + "Usuario/" + UsuarioID);
    }

    /**
     * Obtiene todos los retos a los que el deportista está suscrito
     * @param UsuarioID
     * @returns 
     */
    getDeportistEnDesafio(UsuarioID:string):Observable<any>{ //cambiar
        let URL = this.baseURL + 'DeportistEnDesafio/Usuario/' + UsuarioID;
        return this.http.get<any[]>(URL);
    }

    /**
     * Gets infomacion del reto segun el ID
     * @param RetosID 
     * @returns 
     */
    getReto(RetosID:string):Observable<any>{
        let URL = this.baseURL + 'Retos/' + RetosID;
        return this.http.get<any>(URL);
    }

    /**
     * Gets toda las carreras en las que el deportista ha participado
     * @param UsuarioID
     * @returns 
     */
    getDeportistaEnCarrera(UsuarioID :string):Observable<any>{
        let URL = this.baseURL + 'DeportistaEnCarrera/Deportista/' + UsuarioID ;
        return this.http.get<any[]>(URL);
    }

    /**
     * Gets la informacion de la carrera segun el ID
     * @param CarreraID 
     * @returns 
     */
    getCarrera(CarreraID:string):Observable<any>{
        let URL = this.baseURL + 'Carrera/' + CarreraID;
        return this.http.get<any>(URL);
    }

    /**
     * Gets actividades
     * @param ActivitidadID 
     * @returns  
     */
    getActivitidad(ActivitidadID:string):Observable<any>{
        let URL = this.baseURL + 'Activity/' + ActivitidadID;
        return this.http.get<any>(URL);
    }

    getCarreras():Observable<any>{
        let URL = this.baseURL + '/carreras';
        return this.http.get<any[]>(URL);
    }

    getRetos():Observable<any>{
        let URL = this.baseURL + '/retos';
        return this.http.get<any[]>(URL);
    }
}
