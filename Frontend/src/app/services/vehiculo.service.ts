import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Vehiculo } from '../models/vehiculo';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VehiculoService {

  controlador = 'Vehiculo/';
  listaVehiculos: Vehiculo[] | undefined;

  constructor(private http: HttpClient) { }

  getAllVehiculos() : Observable<Array<Vehiculo>> {
    return this.http.get<Array<Vehiculo>>(environment.backendApiUrl + this.controlador + 'GetAllVehiculos');
  }

  getVehiculoById(id: number) {
    return this.http.get<Vehiculo>(environment.backendApiUrl + this.controlador + id);
  }

  crearVehiculo(vehiculo: Vehiculo) : Observable<Vehiculo> {
    return this.http.post<Vehiculo>(`${environment.backendApiUrl}${this.controlador}NuevoVehiculo`, vehiculo);
  }

  editarVehucilo(vehiculo: Vehiculo) : Observable<Vehiculo> {
    return this.http.put<Vehiculo>(`${environment.backendApiUrl}${this.controlador}EditarVehiculo`, vehiculo);
  }

  eliminarVehiculo(id: number) : Observable<void> {
    return this.http.delete<void>(`${environment.backendApiUrl}${this.controlador}EliminarVehiculo/` + id);
  }

}
