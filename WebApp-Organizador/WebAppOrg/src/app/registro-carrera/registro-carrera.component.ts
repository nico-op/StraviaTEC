import { Component } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-registro-carrera',
  templateUrl: './registro-carrera.component.html',
  styleUrls: ['./registro-carrera.component.css']
})
export class RegistroCarreraComponent {
  constructor(private apiService: ApiService) {}

  enviarDatos(): void {
    const nombreCarrera = (document.getElementById('nombre') as HTMLInputElement).value;
    const recorrido = (document.getElementById('ruta') as HTMLInputElement).value;
    const fechaCarrera = (document.getElementById('fechaCarrera') as HTMLInputElement).value;
    const modalidad = (document.getElementById('modalidad') as HTMLInputElement).value;
    const categoria = (document.getElementById('categoria') as HTMLInputElement).value;
    const nombreBanco = (document.getElementById('nombreBanco') as HTMLInputElement).value;
    const cuentaBanco = ((document.getElementById('cuentaBanco') as HTMLInputElement).value);
    const patrocinador = (document.getElementById('patrocinador') as HTMLInputElement).value;
    const costo = +(document.getElementById('costo') as HTMLInputElement).value;

    const dataCarrera = {
      nombreCarrera,
      recorrido,
      fechaCarrera,
      modalidad,
      categoria,
      costo
    };

    const dataCuentaBancaria = {
      nombreCarrera,
      nombreBanco,
      numeroCuenta: cuentaBanco
    };

    const dataPatrocinador = {
      nombreCarrera,
      nombreComercial: patrocinador
    };

    const urlCarrera = 'https://serviceapistraviatec.azurewebsites.net/api/Carrera';
    const urlCuentaBancaria = 'https://serviceapistraviatec.azurewebsites.net/api/GestionarCuentaBancaria';
    const urlPatrocinador = 'https://serviceapistraviatec.azurewebsites.net/api/PatrocinadoresPorCarrera';

    // Aquí realiza las llamadas a las API con los datos divididos
    // Utiliza el método POST de tu ApiService y ajusta las URL según tu API
    this.apiService.postData(urlCarrera, dataCarrera).subscribe(
      (response) => {
        console.log('Respuesta de la API Carrera:', response);
      },
      (error) => {
        console.error('Error en la API Carrera:', error);
      }
    );

    this.apiService.postData(urlCuentaBancaria, dataCuentaBancaria).subscribe(
      (response) => {
        console.log('Respuesta de la API Cuenta Bancaria:', response);
      },
      (error) => {
        console.error('Error en la API Cuenta Bancaria:', error);
      }
    );

    this.apiService.postData(urlPatrocinador, dataPatrocinador).subscribe(
      (response) => {
        console.log('Respuesta de la API Patrocinador:', response);
      },
      (error) => {
        console.error('Error en la API Patrocinador:', error);
      }
    );
  }
}

