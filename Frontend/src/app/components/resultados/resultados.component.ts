import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Viaje } from 'src/app/models/viaje';
import { CiudadService } from 'src/app/services/ciudad.service';
import { VehiculoService } from 'src/app/services/vehiculo.service';
import { ViajeService } from 'src/app/services/viaje.service';

@Component({
  selector: 'app-resultados',
  templateUrl: './resultados.component.html',
  styleUrls: ['./resultados.component.css']
})
export class ResultadosComponent implements OnInit, AfterViewInit {

  resultados = new Array<Viaje>();
  resultadosFiltrados: Viaje[] = [];

  displayedColumns: string[] = ['fecha', 'vehiculo', 'ciudad', 'acciones'];
  dataSource = new MatTableDataSource<Viaje>(this.resultados);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.paginator._intl.itemsPerPageLabel = 'Items por página';
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  constructor(
    public vehiculosService: VehiculoService,
    public ciudadesService: CiudadService,
    private viajesService: ViajeService
  ) { }

  ngOnInit(): void {
    //Cargo la tabla con los resultados de los viajes por defecto
    this.viajesService.getAllViajes().subscribe(v => {
      this.resultados = v;
      this.resultadosFiltrados = this.resultados; 
    });
  }

}
