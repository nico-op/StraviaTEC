// app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DeportistaComponent } from './components/deportista/deportista.component';
import { IniciarSesionComponent } from './components/iniciar-sesion/iniciar-sesion.component';
import { CrearCuentaComponent } from './components/crear-cuenta/crear-cuenta.component';
import { EditarComponent } from './components/editar/editar.component';
import { ListadoComponent } from './components/listado/listado.component';
import { InicioComponent } from './components/inicio/inicio.component';
import { CrearComponent } from './components/crear/crear.component';

const routes: Routes = [
  { path: 'deportista', component: DeportistaComponent, children: [
    { path: 'iniciar-sesion', component: IniciarSesionComponent },
    { path: 'crear-cuenta', component: CrearCuentaComponent },
  ]},
  { path: 'editar', component: EditarComponent },
  { path: 'listado', component: ListadoComponent },
  { path: 'inicio', component: InicioComponent },
  { path: 'crear', component: CrearComponent },
  { path: '', redirectTo: '/deportista', pathMatch: 'full' },
  { path: '**', redirectTo: '/deportista' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
