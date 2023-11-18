import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { RegistroAtletaComponent } from './registro-atleta/registro-atleta.component';
import { RegistroCarreraComponent } from './registro-carrera/registro-carrera.component';
import { RegistroRetoComponent } from './registro-reto/registro-reto.component';
import { RegistroGrupoComponent } from './registro-grupo/registro-grupo.component';
import { ManejoInscripcionComponent } from './manejo-inscripcion/manejo-inscripcion.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RegistroAtletaComponent,
    RegistroCarreraComponent,
    RegistroRetoComponent,
    RegistroGrupoComponent,
    ManejoInscripcionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { 
  
}
