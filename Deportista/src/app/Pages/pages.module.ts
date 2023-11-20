import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ComponentsModule} from '../Components/components.module';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule} from '@angular/forms';
import { ActividadComponent } from './actividad/actividad.component';
import { BuscarUsuarioComponent } from './buscar-usuario/buscar-usuario.component';
import { CrearActividadComponent } from './crear-actividad/crear-actividad.component';
import { InicioComponent } from './inicio/inicio.component';
import{ LoginComponent } from './login/login.component';
import { RegistrarComponent } from './registrar/registrar.component';
import { EncabezadoComponent } from '../Components/encabezado/encabezado.component';
import { PiePaginaComponent } from '../Components/pie-pagina/pie-pagina.component';


@NgModule({
  declarations: [
    EncabezadoComponent,
    PiePaginaComponent
  ],
  imports: [
    CommonModule,
  ],
  exports: [
    EncabezadoComponent,
    PiePaginaComponent
  ]
})
export class PagesModule { }
