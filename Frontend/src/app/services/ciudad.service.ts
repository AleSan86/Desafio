import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Ciudad } from '../models/ciudad';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CiudadService {

  controlador = 'Ciudad/';
  listaCiudades: Ciudad[] | undefined;

  constructor(private http: HttpClient) { }

  getAllCiudades() : Observable<Array<Ciudad>> {
    return this.http.get<Array<Ciudad>>(environment.backendApiUrl + this.controlador + 'GetAllCiudades');
  }

  getCiudadById(id: number) {
    return this.http.get<Ciudad>(environment.backendApiUrl + this.controlador + id);
  }

  crearCiudad(ciudad: Ciudad) : Observable<Ciudad> {
    return this.http.post<Ciudad>(`${environment.backendApiUrl}${this.controlador}NuevaCiudad`, ciudad);
  }

  editarCiudad(ciudad: Ciudad): Observable<Ciudad> {
    return this.http.put<Ciudad>(`${environment.backendApiUrl}${this.controlador}EditarCiudad`, ciudad);
  }

  eliminarCiudad(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.backendApiUrl}${this.controlador}EliminarCiudad/` + id);
  }
  
}
