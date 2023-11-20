import { Component } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-registro-reto',
  templateUrl: './registro-reto.component.html',
  styleUrls: ['./registro-reto.component.css']
})
export class RegistroRetoComponent {
  constructor(private apiService: ApiService) {}

  enviarDatos(): void {
    const nombreReto = (document.getElementById('nombre') as HTMLInputElement).value;
    const tipoActividad = (document.getElementById('tipo-actividad') as HTMLInputElement).value;
    const periodo = +(document.getElementById('periodo') as HTMLInputElement).value;
    const privacidad = (document.getElementById('privacidad') as HTMLSelectElement).value;
    const fondo = (document.getElementById('fondo') as HTMLInputElement).value;
    const altitud = (document.getElementById('altitud') as HTMLInputElement).value;
    const patrocinador = (document.getElementById('patrocinador') as HTMLInputElement).value;


    const dataReto = {
      privacidad,
      periodo,
      tipoActividad,
      altitud,
      fondo,
      nombreReto
    };

    const dataPatrocinador = {
      nombreReto,
      nombreComercial: patrocinador
    };

    const urlReto = 'https://serviceapistraviatec.azurewebsites.net/api/Reto';
    const urlPatrocinador = 'https://serviceapistraviatec.azurewebsites.net/api/PatrocinadoresPorReto';

    this.apiService.postData(urlReto, dataReto).subscribe(
      (response) => {
        console.log('Respuesta de la API Reto:', response);
      },
      (error) => {
        console.error('Error en la API Reto:', error);
      }
    );

    this.apiService.postData(urlPatrocinador, dataPatrocinador).subscribe(
      (response) => {
        console.log('Respuesta de la API Patrocinador del Reto:', response);
      },
      (error) => {
        console.error('Error en la API Patrocinador del Reto:', error);
      }
    );
  }
}
