import { Injectable } from '@angular/core';
import { Viaje, ViajeGuardar } from '../models/viaje';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ViajeService {

  controlador = 'Viaje/';
  listaViajes: Viaje[] | undefined;

  constructor(private http: HttpClient) { }

  getAllViajes() : Observable<Array<Viaje>> {
    return this.http.get<Array<Viaje>>(environment.backendApiUrl + this.controlador + 'GetAllViajes');
  }

  getViajeById(id: number) {
    return this.http.get<Viaje>(environment.backendApiUrl + this.controlador + id);
  }

  crearViaje(viaje: ViajeGuardar) : Observable<ViajeGuardar> {
    return this.http.post<ViajeGuardar>(`${environment.backendApiUrl}${this.controlador}NuevoViaje`, viaje);
  }

  editarViaje(viaje: Viaje) : Observable<Viaje> {
    return this.http.put<Viaje>(`${environment.backendApiUrl}${this.controlador}EditarViaje`, viaje);
  }

  eliminarViaje(id: number) : Observable<void> {
    return this.http.put<void>(`${environment.backendApiUrl}${this.controlador}EliminarViaje/${id}`, id).pipe();
  }


}
