import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; // Importar
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EditarComponent } from './components/editar/editar.component';
import { ListadoComponent } from './components/listado/listado.component';
import { DeportistaComponent } from './components/deportista/deportista.component';
import { InicioComponent } from './components/inicio/inicio.component';
import { CrearComponent } from './components/crear/crear.component';
import { IniciarSesionComponent } from './components/iniciar-sesion/iniciar-sesion.component';
import { CrearCuentaComponent } from './components/crear-cuenta/crear-cuenta.component';

@NgModule({
  declarations: [
    AppComponent,
    EditarComponent,
    ListadoComponent,
    DeportistaComponent,
    InicioComponent,
    CrearComponent,
    IniciarSesionComponent,
    CrearCuentaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
