import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Viaje, ViajeClima, ViajeGuardar } from 'src/app/models/viaje';
import { ViajeService } from 'src/app/services/viaje.service';
import Swal from 'sweetalert2';
import { WeatherService } from 'src/app/services/weather.service';
import { CiudadService } from 'src/app/services/ciudad.service';
import { VehiculoService } from 'src/app/services/vehiculo.service';
import { Ciudad } from 'src/app/models/ciudad';
import { Vehiculo } from 'src/app/models/vehiculo';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpHeaderResponse } from '@angular/common/http';

interface WeatherData {
  main: {
    temp: number;
  };
  name: string;
}

@Component({
  selector: 'app-viaje',
  templateUrl: './viaje.component.html',
  styleUrls: ['./viaje.component.css'],
})
export class ViajeComponent implements OnInit {
  ciudad = '';
  apiKey = '';

  id?: number;
  weatherData: WeatherData | undefined;
  resultados = new Array<ViajeClima>();
  resultadosFiltrados: ViajeClima[] = [];
  working: boolean = true;
  mostrarFormulario: boolean = false;
  mostrarFormularioEditar: boolean = false;
  formularioCrear: FormGroup;
  formularioEditar: FormGroup;
  ciudadesList: Array<Ciudad> | undefined;
  vehiculosList: Array<Vehiculo> | undefined;
  displayedColumns: string[] = [
    'fecha',
    'vehiculo',
    'ciudad',
    'clima',
    'acciones',
    'condicion',
  ];
  dataSource = new MatTableDataSource<ViajeClima>(this.resultadosFiltrados);
  ciudades: any;
  vehiculos: any;
  data: any;

  //Filtro dinámico
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    const filtro = filterValue.trim().toLowerCase();
    this.resultadosFiltrados = this.resultados.filter(
      (element) =>
        element.ciudad.descripcion.toLowerCase().includes(filtro) ||
        element.vehiculo.patente.toLowerCase().includes(filtro) ||
        element.vehiculo.marca.toLowerCase().includes(filtro) ||
        element.clima?.estado?.toLowerCase().includes(filtro) ||
        element.vehiculo.tipoVehiculo.descripcion.toLowerCase().includes(filtro)
    );

    this.dataSource = new MatTableDataSource<ViajeClima>(
      this.resultadosFiltrados
    );
  }

  constructor(
    private viajesService: ViajeService,
    private ciudadService: CiudadService,
    private vehiculoService: VehiculoService,
    private weatherService: WeatherService,
    private formBuilder: FormBuilder,
    private router: Router,
    private aRoute: ActivatedRoute
  ) {
    //Inicializo mis formularios
    this.formularioCrear = this.formBuilder.group({
      fecha: ['', Validators.required],
      vehiculo: ['', Validators.required],
      ciudad: ['', Validators.required],
      activo: '',
    });

    this.formularioEditar = this.formBuilder.group({
      fecha: [''],
      ciudad: [''],
      vehiculo: [''],
      activo: '',
    });

    this.id = Number(this.aRoute.snapshot.paramMap.get('id'));
  }

  ngOnInit(): void {
    //Cargo la tabla con los resultados de los viajes por defecto
    //Armo mi tabla de viajes
    this.viajesService.getAllViajes().subscribe((v) => {
      this.resultados = this.cargarClima(v);
      this.dataSource = new MatTableDataSource<ViajeClima>(this.resultados);
      this.working = false;
      this.mostrarFormulario = false;
      this.mostrarFormularioEditar = false;
    });
  }

  //Funciones principales
  verDetalleViaje(viaje: Viaje) {
    //Uso Sweetearlert para mostrar los detalles del viaje
    Swal.fire({
      icon: 'info',
      title: 'Detalles del viaje',
      showCloseButton: true,
      html: `
        La fecha del viaje a ${viaje.ciudad.descripcion},
        se realizará en fecha: ${viaje.fecha}, con el vehiculo
        ${viaje.vehiculo.tipoVehiculo.descripcion}
        - ${viaje.vehiculo.marca} - ${viaje.vehiculo.patente}
      `,
      confirmButtonText: 'Cerrar',
    });
  }

  crearViaje() {
    const viajeNuevo: ViajeGuardar = {
      idCiudad: this.formularioCrear.value.ciudad,
      idVehiculo: this.formularioCrear.value.vehiculo,
      fecha: this.formularioCrear.value.fecha,
      activo: 1,
    };

    this.working = true;
    this.viajesService.crearViaje(viajeNuevo).subscribe(() => {}, (e: HttpHeaderResponse) => {
      if (e.status == 200) {
        this.viajesService.getAllViajes().subscribe(v => {this.resultados = this.cargarClima(v);
          this.dataSource = new MatTableDataSource<ViajeClima>(this.resultados);
        })
        Swal.fire({
          icon: 'success',
          title: 'Viaje creado con éxito',
          showCloseButton: true,
          confirmButtonText: 'Cerrar',
        }); 
        this.ocultarFormularioCrear();
      } else {
        Swal.fire({
          icon: "error",
          title: "Error al intentar guardar el viaje",
          showCloseButton: true,
          confirmButtonText: 'Cerrar',
        });
      }});
      this.working = false;
  }
  
  editarForm(viaje: Viaje) {

    this.mostrarFormularioEditar = true;
    this.formularioEditar = this.formBuilder.group({
      ciudad: this.formularioEditar.value.ciudad,
      vehiculo: this.formularioEditar.value.vehiculo,
      fecha: this.formularioEditar.value.fecha,
    });
  
    this.formularioEditar.setValue({
    // id: viaje.id,
    // fechaAnterior: viaje.fechaAnterior,
    // activo: viaje.activo,
      ciudad: viaje.ciudad.descripcion,
      vehiculo: viaje.vehiculo.patente,
      fecha: viaje.fecha,
    });
    //Todo
    //Limpiar el formulario
  }

  editarViaje() {
    
    this.mostrarFormularioEditar = true;
    //this.abrirFormEditar(viaje);
    const viajeEditado: Viaje = {
      ciudad: this.formularioEditar.value.ciudad,
      vehiculo: this.formularioEditar.value.vehiculo,
      fecha: this.formularioEditar.value.fecha,
      activo: this.formularioEditar.value,
    };

    console.log(viajeEditado);

    Swal.fire({
      icon: 'info',
      title: 'Atención, esta a punto de editar un viaje',
      showCloseButton: true,
      confirmButtonText: 'Cerrar',
    }).then((result) => {
      if (result.isConfirmed) {
        this.viajesService.editarViaje(viajeEditado).subscribe(
          () => {
          },
          (e: HttpHeaderResponse) => {
            if (e.status == 200) {
              Swal.fire('¡Registro editado con éxito!', '', 'success');
            }
            this.router.navigate(['']);
          }
        );
      }
    });
  }

  eliminarViaje(id: number) {

    Swal.fire({
      icon: 'info',
      title: 'Atención, esta a punto de eliminar un viaje',
      showCloseButton: true,
      showCancelButton: true,
      confirmButtonText: 'Aceptar',
      cancelButtonText: 'Cancelar',
    }).then((result) => {
      if (result.isConfirmed) {
        try {
          this.viajesService.eliminarViaje(id).subscribe(() => {
           
          }, (e: HttpHeaderResponse) => {            
            if (e.status == 200) {
            Swal.fire({
              icon: 'success',
              title: 'Viaje eliminado con éxito',
              showCloseButton: true,
              confirmButtonText: 'Cerrar',
            });
             this.viajesService.getAllViajes().subscribe( v => {this.resultados = this.cargarClima(v);
              this.dataSource = new MatTableDataSource<ViajeClima>(this.resultados);
            });
          }});
          this.working = false;
        } catch {
          Swal.fire({
            icon: 'error',
            title: 'Ocurrió un error al querer eliminar el viaje',
            showCloseButton: true,
            confirmButtonText: 'Cerrar',
          });
        }
      }
    });
    this.working = false;
  }

  //Fetch del servicio de la API del clima
  cargarClima(resultados: Array<Viaje>): Array<ViajeClima> {

    let viajesConClima = new Array<ViajeClima>();
    const resultadosActuales = resultados.filter(r => r.activo === 1 && (new Date(r.fecha) >= new Date()))
    
    resultadosActuales.forEach((r) => {
      let viaje = new ViajeClima();
      viaje.id = r.id;
      viaje.activo = r.activo;
      viaje.ciudad = r.ciudad;
      viaje.fecha = r.fecha;
      viaje.vehiculo = r.vehiculo;
      viaje.fechaAnterior = r.fechaAnterior;
      this.weatherService.getWeather(viaje.ciudad, viaje.fecha).then((data) => {
        if (data) {
          viaje.clima = data;
          viaje.validado = data.estado != 'Rain' && data.estado != 'Snow';
        }
      });
      viajesConClima.push(viaje);
   });
    
    return viajesConClima;
  }


  //Funciones auxiliares
  mostrarFormularioCrear(): void {
    this.mostrarFormularioEditar = false;
    this.mostrarFormulario = true;
    this.abrirForm();
  }

  ocultarFormularioCrear(): void {
    this.mostrarFormulario = false;
  }

  mostrarFormularioEdit() {
    this.mostrarFormularioEditar = true;
    this.mostrarFormulario = false;
    //this.abrirFormEditar(viaje);
  }

  ocultarFormularioEdit() {
    this.mostrarFormularioEditar = false;
  }

  abrirForm() {
    this.working = true;

    this.ciudadService.getAllCiudades().subscribe((ciudades: any) => {
      this.ciudadesList = ciudades;
      this.working = false;
    });

    this.vehiculoService.getAllVehiculos().subscribe((vehiculos: any) => {
      this.vehiculosList = vehiculos;
      this.working = false;
    });

    this.working = false;
    this.mostrarFormulario = true;

    this.formularioCrear = this.formBuilder.group({
      ciudad: [''],
      vehiculo: [''],
      fecha: [''],
    });
  }

  abrirFormEditar(viaje: Viaje) {
    this.working = true;

    // this.ciudadService.getAllCiudades().subscribe((ciudades: any) => {
    //   this.ciudadesList = ciudades;
    //   this.working = false;
    // });

    // this.vehiculoService.getAllVehiculos().subscribe((vehiculos: any) => {
    //   this.vehiculosList = vehiculos;
    //   this.working = false;
    // });

    this.formularioEditar = this.formBuilder.group({
      ciudad: [viaje.ciudad],
      vehiculo: [viaje.vehiculo],
      fecha: [viaje.fecha],
    });
    this.mostrarFormularioEditar = true;

    this.working = false;
    this.mostrarFormularioEditar = true;
  }

}
