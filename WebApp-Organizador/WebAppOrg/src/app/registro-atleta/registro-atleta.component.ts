import { Component } from '@angular/core';
import { ApiService } from '../api.service';


@Component({
  selector: 'app-registro-atleta',
  templateUrl: './registro-atleta.component.html',
  styleUrls: ['./registro-atleta.component.css']
})
export class RegistroAtletaComponent {
  imagen: any;
  rutaFoto: string = '';
  constructor(private apiService: ApiService) {}


    mostrarFoto(event: any) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.imagen = e.target.result;
        this.rutaFoto = e.target.result; // Guardar la ruta como string
      };
      reader.readAsDataURL(event.target.files[0]);
    }

  enviarDatos(): void {
    const nombre = (document.getElementById('nombre') as HTMLInputElement).value;
    const apellido1 = (document.getElementById('primer-apellido') as HTMLInputElement).value;
    const apellido2 = (document.getElementById('segundo-apellido') as HTMLInputElement).value;
    const fechaNacimiento = (document.getElementById('fechaNacimiento') as HTMLInputElement).value;
    const nacionalidad = (document.getElementById('nacionalidad') as HTMLInputElement).value;
    const nombreUsuario = (document.getElementById('usuario') as HTMLInputElement).value;
    const contrasena = ((document.getElementById('contrasena') as HTMLInputElement).value);

    const hoy = new Date().toISOString();

    const fechaNac = new Date(fechaNacimiento);
    const hoyDate = new Date(hoy);
    const diff = hoyDate.getTime() - fechaNac.getTime();
    const edad = Math.floor(diff / (1000 * 60 * 60 * 24 * 365.25));
    
    const atletaData = {
      nombre: nombre,
      apellido1: apellido1,
      apellido2: apellido2,
      fechaNacimiento: fechaNacimiento,
      fechaActual: hoy,
      nacionalidad: nacionalidad,
      foto: this.rutaFoto,
      nombreUsuario: nombreUsuario,
      contrasena: contrasena,
      edad: 0 
    };

    atletaData.edad = edad;
    // Llamar a la API para registrar al atleta
    const url = 'https://serviceapistraviatec.azurewebsites.net/api/Usuario';
    this.apiService.postData(url, atletaData).subscribe(
      (response) => {
        console.log('Atleta registrado:', response);
    },
           (error) => {
              console.error('Error al registrar al atleta:', error);
      }
    );


  }

  

}
