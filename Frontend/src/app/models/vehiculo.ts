export class Vehiculo {
    id?: number;
    patente!: string;
    marca!: string;
    anio!: number;
    tara?: number;
    tipoVehiculo!: TipoVehiculo;
}

export class TipoVehiculo {
    id!: number;
    descripcion!: string;
}