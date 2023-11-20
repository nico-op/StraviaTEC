import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from '../../Models/Usuario';
import { PostService } from '../../Services/Post/post.service';

@Component({
  selector: 'app-registrar',
  templateUrl: './registrar.component.html',
  styleUrls: ['./registrar.component.css'] // Corrige 'styleUrl' a 'styleUrls'
})
export class RegistrarComponent implements OnInit { // Corrige 'ngOnInIt' a 'ngOnInit'

  public files: any = [];
  imageSrc: string = "";

  newUsuario: Usuario = {
    nombre: '',
    apellido: '',
    fechaNacimiento: new Date(),
    nacionalidad: '',
    nombreUsuario: '',
    contrasena: '',
    foto: '',
    correo: '',
    edad: 0
  };

  constructor(private router: Router, private postSvc: PostService) { }

  ngOnInit(): void {} // Corrige 'ngOnInIt' a 'ngOnInit'

  signIn() {
    this.router.navigate(["login"]);
  }

  onFileChange(event: any) {
    const reader = new FileReader(); // Agrega esta línea para inicializar el lector de archivos
    const [file] = event.target.files;
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.imageSrc = reader.result as string;
      this.newUsuario.foto = this.imageSrc;
    };
  }

  registrarUsuario() {
    this.postSvc.registrarUsuario(this.newUsuario).subscribe(
      res => {
        if (res == "") {
          this.router.navigate(["login"]);
        } else {
          if (res[0].message_id == 2601) {
            alert("El nombre de usuario ingresado ya existe.");
          }
        }
      }
    );
  }
}
