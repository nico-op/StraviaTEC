import { Component } from '@angular/core';

@Component({
  selector: 'app-registro-atleta',
  templateUrl: './registro-atleta.component.html',
  styleUrls: ['./registro-atleta.component.css']
})
export class RegistroAtletaComponent {
  imagen: any;

  mostrarFoto(event: any) {
    const reader = new FileReader();
    reader.onload = (e: any) => {
      this.imagen = e.target.result;
    };
    reader.readAsDataURL(event.target.files[0]);
  }

}
