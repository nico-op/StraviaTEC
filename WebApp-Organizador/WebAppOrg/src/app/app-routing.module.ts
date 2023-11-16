import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegistroAtletaComponent } from './registro-atleta/registro-atleta.component';
import { RegistroCarreraComponent } from './registro-carrera/registro-carrera.component';
import { RegistroRetoComponent } from './registro-reto/registro-reto.component';
import { RegistroGrupoComponent } from './registro-grupo/registro-grupo.component';
import { ManejoInscripcionComponent } from './manejo-inscripcion/manejo-inscripcion.component';

const routes: Routes = [
  {path: '', component : HomeComponent},
  {path: 'registro-atleta', component: RegistroAtletaComponent},
  {path: 'registro-carrera', component: RegistroCarreraComponent},
  {path: 'registro-reto', component: RegistroRetoComponent},
  {path: 'registro-grupo', component: RegistroGrupoComponent},
  {path: 'manejo-incripcion', component: ManejoInscripcionComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
