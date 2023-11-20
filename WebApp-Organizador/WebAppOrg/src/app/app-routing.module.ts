import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegistroCarreraComponent } from './registro-carrera/registro-carrera.component';
import { RegistroRetoComponent } from './registro-reto/registro-reto.component';
import { RegistroGrupoComponent } from './registro-grupo/registro-grupo.component';
import { ManejoInscripcionComponent } from './manejo-inscripcion/manejo-inscripcion.component';
import { ReporteComponent } from './reporte/reporte.component';
import { RegistroAtletaComponent } from './registro-atleta/registro-atleta.component';

const routes: Routes = [
  {path: '', component : HomeComponent},
  {path: 'registro-atleta', component: RegistroAtletaComponent},
  {path: 'registro-carrera', component: RegistroCarreraComponent},
  {path: 'registro-reto', component: RegistroRetoComponent},
  {path: 'registro-grupo', component: RegistroGrupoComponent},
  {path: 'manejo-incripcion', component: ManejoInscripcionComponent},
  {path: 'reporte', component: ReporteComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
