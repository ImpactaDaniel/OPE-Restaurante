import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FuncionarioService } from '../services/funcionario/funcionario.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public forecasts: WeatherForecast[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private funcionarioService: FuncionarioService) {
   
  }
  ngOnInit(): void {
    console.log("Teste");
    this.funcionarioService.createUser().subscribe(c => {
      console.log(c);
    });
    }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
