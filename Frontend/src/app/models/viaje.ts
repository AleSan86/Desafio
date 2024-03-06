import { Ciudad } from "./ciudad";
import { Clima } from "./clima";
import { Vehiculo } from "./vehiculo";

export class Viaje {
    id?: number;
    fecha!: Date;
    fechaAnterior?: Date;
    activo!: number;
    vehiculo!: Vehiculo;
    ciudad!: Ciudad;
}

export class ViajeClima extends Viaje {
    clima!: Clima;
    validado!: boolean;
}

export class ViajeGuardar {
    id?: number;
    fecha?: Date;
    fechaAnterior?: Date;
    activo?: number;
    idVehiculo!: number;
    idCiudad!: number;
}