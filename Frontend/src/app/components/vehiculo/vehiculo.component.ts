import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Vehiculo } from 'src/app/models/vehiculo';
import { VehiculoService } from 'src/app/services/vehiculo.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-vehiculo',
  templateUrl: './vehiculo.component.html',
  styleUrls: ['./vehiculo.component.css']
})
export class VehiculoComponent implements OnInit {

  resultados = new Array<Vehiculo>();
  resultadosFiltrados: Vehiculo[] = [];
  working: boolean = true;
  displayedColumns: string[] = ['vehiculo', 'marca', 'patente','acciones'];
  dataSource = new MatTableDataSource<Vehiculo>(this.resultados);


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  constructor(private vehiculosService: VehiculoService) { }

  ngOnInit(): void {
    this.working = true;
    this.vehiculosService.getAllVehiculos().subscribe(v => {
      this.resultados = v;
      this.resultadosFiltrados = v; 
      this.dataSource = new MatTableDataSource<Vehiculo>(v);
      this.working = false;
    });
  }

  //Funciones principales
  verDetalleVehiculo(vehiculo: Vehiculo) {
  //Uso Sweetearlert para mostrar los detalles
  Swal.fire({
    icon: 'info',
    title: 'Detalles del vehiculo',
    showCloseButton: true,
    html: `
      <h3>Vehiculo: </h3> ${vehiculo.tipoVehiculo.descripcion} 
      ${vehiculo.marca}, con patente: ${vehiculo.patente}
      del año: ${vehiculo.anio}, con tara ${vehiculo.tara}.
    `,
    confirmButtonText: 'Cerrar',
  });
  }

  crearVehiculo() {
    alert("Sin terminar");
    console.log("Sin terminar");
  }

  editarVehiculo() {
    alert("Sin terminar");
    console.log("Sin terminar");
  }

  eliminarVehiculo() {
    alert("Sin terminar");
    console.log("Sin terminar");
  }

}
