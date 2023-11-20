import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Usuario } from '../../Models/Usuario';
import { PostService } from '../../Services/Post/post.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'] // Corrected styleUrl to styleUrls
})
export class LoginComponent implements OnInit {

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


  constructor(private router: Router, private postSvc: PostService, private cookieSvc: CookieService) { }
  
  validation = {
    Existe: ""
  }

  ngOnInit(): void {}

  login() {
    this.router.navigate(["login"]);
  }


  loginUsuario() {
    this.postSvc.loginUsuario(this.newUsuario).subscribe(
      res => {
        this.validation.Existe = res;
        if (this.validation.Existe == "Si") {
          alert("Inicio Correcto");
          this.cookieSvc.set("Usuario", this.newUsuario.nombreUsuario);

        } else {
          alert("El nombre o contraseña son incorrectos.");
        }
      },
    );
  }
}
