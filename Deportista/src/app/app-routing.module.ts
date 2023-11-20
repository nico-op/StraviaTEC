import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ActividadComponent } from './Pages/actividad/actividad.component';
import { BuscarUsuarioComponent } from './Pages/buscar-usuario/buscar-usuario.component';
import { CrearActividadComponent } from './Pages/crear-actividad/crear-actividad.component';
import { InicioComponent } from './Pages/inicio/inicio.component';
import { LoginComponent } from './Pages/login/login.component';
import { RegistrarComponent } from './Pages/registrar/registrar.component';


const routes: Routes = [ 
  {path: '', component: InicioComponent},
  {path: 'actividad', component: ActividadComponent},
  {path: 'buscar-usuario', component: BuscarUsuarioComponent},
  {path: 'crear-actividad', component: CrearActividadComponent},
  {path: 'login', component: LoginComponent},
  {path: 'registrar', component: RegistrarComponent},
  {path: '**', redirectTo: 'registrar', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
