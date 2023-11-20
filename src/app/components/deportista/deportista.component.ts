import { Component } from '@angular/core';
import { DeportistaService } from '../../services/deportista.service';
@Component({
  selector: 'app-deportista',
  templateUrl: './deportista.component.html',
  styleUrl: './deportista.component.css'
})
export class DeportistaComponent {
  actividades: any[] = []; //propiedades actividades
  constructor(private deportistaService: DeportistaService) { }

  ngOnInit(): void {
  //metodos
  this.deportistaService.getActividades().subscribe((data:any)=> {
    this.actividades = data;//configurar el arreglo de actividades

  
  });
}
}
