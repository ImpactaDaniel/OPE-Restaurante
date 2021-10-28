import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AlertService } from 'src/app/services/alert.service';
import { BasicentitiesService } from 'src/app/services/entities/basicentities.service';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.scss']
})
export class CreateProductComponent implements OnInit {

  categories: any;
  hasPhoto = false;
  photo: any;
  fg: FormGroup;
  error = false;
  erroMsg = "";

  constructor(private basicEntitiesService: BasicentitiesService, private fb: FormBuilder,
    private productService: ProductService, private alertService: AlertService) { }

  async ngOnInit() {
    this.buildForm();
    await this.loadCategories();
  }

  public createProduct() {
    this.error = false;
    this.erroMsg = "";
    if(!this.fg.valid) return;
    this.productService.createProduct(this.fg.value).subscribe(response => {
      if(response.success)
      {
        this.alertService.showSuccess("Sucesso", "Produto Cadastrado Com Sucesso!")
        return;
      }
      this.error = true;
      for (let notification of response.notifications) {
        this.erroMsg += `\n${notification.message}`;
      }
      return;
    });
  }


  private buildForm() {
    this.fg = this.fb.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      quantityStock: ['', [Validators.required]],
      category: this.fb.group({
        id: ['', [Validators.required]]
      }),
      accompaniments: ['', [Validators.required]],
      price: ['', [Validators.required]],
      available: [false],
      photoRequest: this.fb.group({
        fileName: ['', [Validators.required]],
        photoB64: ['', [Validators.required]]
      })
    });
  }

  private async loadCategories() {
    let response = await this.basicEntitiesService.getAll({ url: "ProductCategories", page: 0, pageSize: 45 }).toPromise();
    this.categories = response.response.result.entities;
  }

  public photoChanged(event: any) {
    let file = event.target?.files[0];

    this.photo = {};
    this.hasPhoto = false;
    this.fg.get('photoRequest').get('fileName').setValue('');
    this.fg.get('photoRequest').get('photoB64').setValue('');

    if (!file || file === null) return;

    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.photo = {
        fileName: file.name,
        photob64: reader.result
      };
      this.hasPhoto = true;
      this.fg.get('photoRequest').get('fileName').setValue(this.photo.fileName);
      this.fg.get('photoRequest').get('photoB64').setValue(this.photo.photob64.replace('data:image/jpeg;base64,', ''));
    }
  }

}
