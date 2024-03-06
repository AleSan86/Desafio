import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { VehiculoComponent } from './components/vehiculo/vehiculo.component';
import { CiudadComponent } from './components/ciudad/ciudad.component';
import { ViajeComponent } from './components/viaje/viaje.component';

const routes: Routes = [
  {
      path: '', children: [
    { path: '', pathMatch: 'full', component: ViajeComponent },
    { path: 'detalleViaje/:id', component: ViajeComponent },
    { path: 'vehiculos', component: VehiculoComponent },
    { path: 'detalleVehiculo/:id', component: VehiculoComponent },
    { path: 'ciudades', component: CiudadComponent },
    { path: 'detalleCiudad/:id', component: CiudadComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes,
    {
      useHash: true,
      anchorScrolling: 'enabled',
      onSameUrlNavigation: 'reload',
      enableTracing: true,
      scrollPositionRestoration: 'enabled'
    })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
