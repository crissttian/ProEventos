import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  constructor(private http:HttpClient) { }

  public eventos: any;

  //executado antes da aplicação ser carregada
  ngOnInit(): void {
    this.carregarEventos();
  }

  public carregarEventos():any{
    this.http.get('https://localhost:5001/api/eventos').subscribe(
      response => this.eventos = response,
      error => console.log(error),
    )
  }
}
