import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Ciudad } from '../models/ciudad';
import { Clima } from '../models/clima';
import Swal from 'sweetalert2';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})

export class WeatherService {

  constructor(private http: HttpClient) {}

  async getWeather(ciudad: Ciudad, fecha: Date): Promise<Clima | null> {

    const params = `lat=${ciudad.latitud}&lon=${ciudad.longitud}`;
    //Valido que (por lo menos) me llegue la descripción
    if (ciudad.descripcion != '') {
      const climaDiario = this.getClimaDiario(params, fecha);
      return climaDiario;
    }
    return null;
  }

  private async getClimaDiario(params: string, fecha: Date): Promise<Clima> {

    const clima = new Clima();

    try {
      const res = await fetch(environment.apiUrl + params + environment.weatherParams);
      const data = await res.json();
      
      if (data && data.daily) {
        const climaDiario = data.daily.filter(
          (dc: { dt: number }) => this.convertirUnixAString(dc.dt) === new Date(fecha).toDateString()
        );
        if (climaDiario.length > 0)
          clima.estado = climaDiario[0]?.weather[0]?.main;
      } else {
        Swal.fire({
            icon: 'error',
            title: 'Atención',
            html: `
              Ocurrió en error al consultar datos de la fecha: ${fecha}
            `,
            text: 'Consulte la consola.',
            confirmButtonText: 'Cerrar',
          });
        console.error('Error: No se encontraron datos para la fecha: ', fecha);
      }
    } catch (error) {
        Swal.fire({
            icon: 'error',
            title: 'Atención',
            text: 'Error al obtener datos del clima, consulte la consola.',
            confirmButtonText: 'Cerrar',
          });
      console.error('Error al obtener datos del clima: ', error);
    }
    return clima;
  }

  //Funciones auxiliares
  private convertirUnixAString(dia: number): string {
    const date = new Date();
    date.setTime(dia * 1000);
    return date.toDateString();
  }

  //TODO
  //Consumir API de Geolocation cuando se agregue una ciudad para guardar sus coordenadas

  // private async getCiudadCoors(ciudad: string): Promise<Ciudad> {
  //   let ciudadCoor: Ciudad | undefined;
  //   if (ciudadCoor) return ciudadCoor;
  //   return { longitud: 0, latitud: 0, descripcion: '' };
  // }
  
}
