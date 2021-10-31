import { BasicentitiesService } from './../../../services/entities/basicentities.service';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-list-bank',
  templateUrl: './list-bank.component.html',
  styleUrls: ['./list-bank.component.scss']
})
export class ListBankComponent implements OnInit {

  public banks = new MatTableDataSource<any>();

  public pageSize: number = 5;
  public page: number = 0;
  public listSize: number = 0;
  private isSearching = false;
  public name: string;

  public displayedColumns = ['id', 'name', 'edit', 'remove']

  constructor(
    private basicEntitiesService: BasicentitiesService,
    private router: Router
    ) { }

  ngOnInit() {
    this.loadTable();
  }

  private loadTable() {
    this.basicEntitiesService.getAll({ url: "Banks", pageSize: this.pageSize, page: this.page }).subscribe(r => {
      this.banks.data = r.response.result.entities;
      this.listSize = r.response.result.size;
    })
  }

  public search(){
    let value = this.name;

    this.isSearching = true;

    if(value.length <= 2) {
      this.loadTable();
      this.isSearching = false;
      return;
    }

    this.basicEntitiesService.search({ url: "Banks", pageSize: this.pageSize, page: this.page, field: 'name', value }).subscribe(r => {
      this.banks.data = r.response.result.entities;
      this.listSize = r.response.result.size;
    })
  }

  public details(id: number) {
  }

  public remove(id: number) {
  }

  public changePaginator(event: any) {
    this.pageSize = event.pageSize;
    this.page = event.pageIndex;
    if(!this.isSearching){
      this.loadTable();
      return;
    }

    this.search()
  }

  public toNewBank(): void {
    this.router.navigate(['bank/create'])
  }

}
