import { Component, Inject, OnInit } from '@angular/core';
import { BasicentitiesService } from 'src/app/services/entities/basicentities.service';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-list-product',
  templateUrl: './list-product.component.html',
  styleUrls: ['./list-product.component.scss']
})
export class ListProductComponent implements OnInit {

  public products: any;
  public page = 0;
  public limit = 5;
  public listSize: number;
  public searchField = "name";
  public searchValue: string;
  public categories: any[];
  private isSearching = false;
  public displayedColumns = ['photo', 'productId', 'name', 'quantityStock', 'category', 'price', 'available', 'edit', 'remove']

  constructor(@Inject('BASE_URL') public url: string, private productsService: ProductService, private basicEntitiesService: BasicentitiesService) { }

  ngOnInit() {
    this.getProductList();
    this.getCategories();
  }

  private getCategories() {
    this.basicEntitiesService.getAll({ url: 'ProductCategories', page: 0, pageSize: 50 }).subscribe(res => {
      this.categories = res.response.result.entities;
    })
  }

  private getProductList() {

    if (this.isSearching) {
      this.productsService.searchProducts(this.searchField, this.searchValue, this.page, this.limit).subscribe(res => {
        this.products = res.response.result?.entities;
        this.listSize = res.response.result?.size;
      });
      return;
    }

    this.productsService.getAllProducts(this.page, this.limit).subscribe(res => {
      this.products = res.response.result.entities;
      this.listSize = res.response.result.size;
    })
  }

  public changePaginator(event: any) {
    this.page = event.pageIndex;
    this.limit = event.pageSize;
    this.getProductList();
  }

  public searchFieldChanged() {
    this.searchValue = "";
  }

  public removeFilters() {
    this.searchField = "name";
    this.searchValue = "";
    this.isSearching = false;
    this.getProductList();
  }

  public search(event: any) {
    this.searchValue = event.value || event.target.value;
    if (this.searchField === 'createdDate') {
      let date = new Date(this.searchValue);
      this.searchValue = date.toLocaleString();
    }
    this.isSearching = true;
    this.page = 0;
    this.limit = 5;
    this.getProductList();
  }

}
