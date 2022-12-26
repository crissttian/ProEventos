import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  constructor(private http:HttpClient) { }

  public eventos: any = [];
  public eventosFiltrados: any = [];
  larguraImagem:number = 150;
  marginImagem:number = 2;
  mostrarImagem:boolean = true;
  private _filtroLista: string = "";

public get filtroLista() : string{
  return this._filtroLista;
}

public set filtroLista(value: string){
  this._filtroLista = value;
  this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
}


filtrarEventos(filtrarPor: string): any {
  filtrarPor = filtrarPor.toLocaleLowerCase();
  return this.eventos.filter((evento: { tema: string; local: string; }) =>
    evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
    evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
  )
}

  //executado antes da aplicação ser carregada
  ngOnInit(): void {
    this.carregarEventos();
  }

  alterarImagem(): void{
    this.mostrarImagem = !this.mostrarImagem;
  }

  public carregarEventos():any{
    this.http.get('https://localhost:5001/api/eventos').subscribe(
      response => {
        this.eventos = response
        this.eventosFiltrados = this.eventos
      },
      error => console.log(error),
    )
  }
}
