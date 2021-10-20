import { BasicentitiesService } from './../../../services/entities/basicentities.service';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-list-bank',
  templateUrl: './list-bank.component.html',
  styleUrls: ['./list-bank.component.scss']
})
export class ListBankComponent implements OnInit {

  public banks = new MatTableDataSource<any>();

  public displayedColumns = ['id', 'name', 'edit', 'remove']

  constructor(private basicEntitiesService: BasicentitiesService) { }

  ngOnInit() {
    this.basicEntitiesService.getAll({ url: "Banks" }).subscribe(r => {
      this.banks.data = r.response.result;
    })
  }

}
