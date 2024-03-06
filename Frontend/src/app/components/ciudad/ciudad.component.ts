import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Ciudad } from 'src/app/models/ciudad';
import { CiudadService } from 'src/app/services/ciudad.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-ciudad',
  templateUrl: './ciudad.component.html',
  styleUrls: ['./ciudad.component.css']
})
export class CiudadComponent implements OnInit {

  resultados = new Array<Ciudad>();
  resultadosFiltrados: Ciudad[] = [];
  working: boolean = true;
  displayedColumns: string[] = ['ciudad', 'acciones'];
  dataSource = new MatTableDataSource<Ciudad>(this.resultados);

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  constructor(public ciudadesService: CiudadService) { }

  ngOnInit(): void {
    this.ciudadesService.getAllCiudades().subscribe(c => {
      this.resultados = c;
      this.resultadosFiltrados = c; 
      this.dataSource = new MatTableDataSource<Ciudad>(c);
      this.working = false;
    });
  }

  //Funciones principales
  verDetalleCiudad(ciudad: Ciudad) {
    //Uso Sweetearlert para mostrar los detalles
    Swal.fire({
      icon: 'info',
      title: 'Detalles de ciudad',
      showCloseButton: true,
      html: `
        <h3>Nombre de ciudad: </h3> ${ciudad.descripcion},
        sus coordenadas son: ${ciudad.latitud}, ${ciudad.longitud}.
      `,
      confirmButtonText: 'Cerrar',
    });
  }

  crearCiudad() {
    alert("Sin terminar");
    console.log("Sin terminar");
  }

  editarCiudad() {
    alert("Sin terminar");
    console.log("Sin terminar");
  }

  eliminarCiudad() {
    alert("Sin terminar");
    console.log("Sin terminar");
  }

}
