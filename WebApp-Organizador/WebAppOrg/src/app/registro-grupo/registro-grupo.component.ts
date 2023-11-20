import { Component } from '@angular/core';
import { ApiService } from '../api.service';


@Component({
  selector: 'app-registro-grupo',
  templateUrl: './registro-grupo.component.html',
  styleUrls: ['./registro-grupo.component.css']
})
export class RegistroGrupoComponent {
  constructor(private apiService: ApiService) {}

  enviarDato(): void {
    const nombreGrupo = (document.getElementById('nombre') as HTMLInputElement).value;
    const descripcion = (document.getElementById('descripcion') as HTMLInputElement).value;
    const administrador = (document.getElementById('admin') as HTMLInputElement).value;
    const creacion = (document.getElementById('fecha') as HTMLInputElement).value;
    const grupoId = (document.getElementById('id') as HTMLInputElement).value;

    const dataGrupo = {
      nombreGrupo,
      descripcion,
      administrador,
      creacion,
      grupoId
    };

    const urlGrupo = 'https://serviceapistraviatec.azurewebsites.net/api/Grupo';

    console.log(dataGrupo);

    this.apiService.postData(urlGrupo, dataGrupo).subscribe(
      (response) => {
        console.log('Respuesta de la API Grupo:', response);

      },
      (error) => {
        console.error('Error en la API Grupo:', error);
      }
    );
  }
}
