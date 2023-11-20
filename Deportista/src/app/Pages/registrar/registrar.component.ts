import{Component, OnInit} from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import {UsuarioModel} from 'src/app/Models/Usuario.model';

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html',
  styleUrl: './registrar.component.css'
})
export class RegistrarComponent {

  public files: any = [];
  imageSrc: string="";

  newUsuario: UsuarioModel = {
    nombre: '',
    apellido: '',
    fechaNacimiento: new Date(),
    nacionalidad: '',
    nombreUsuario: '',
    contrasena: '',
    foto: '',
    correo: '',
    edad: 0
}
@param router
@param postSvc
constructor(private router: Router, private postSvc: PostService) { }

ngOnInIt(): void{}
signIn(){
  this.router.navigate(["login"]);
  }

  onFileChange(event:any){
    const [file] = event.target.files;
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.imageSrc = reader.result as string;
      this.newUsuario.foto = this.imageSrc;
    };
  }
  registrarUsuario(){
    this.postSvc.signUpAthlete(this.newAthlete).subscribe(
      res =>{
        if (res == "") {
          this.router.navigate(["login"]);
        }
        else {
          if (res[0].message_id == 2601) {
            alert("El nombre de usuario ingresado ya existe.");
          }
        }
      }
    );
  }
}
