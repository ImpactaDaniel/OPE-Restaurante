import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BasicentitiesService } from 'src/app/services/entities/basicentities.service';
import { ProductService } from '../services/product.service';
import { AlertService } from 'src/app/services/alert.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent implements OnInit {

  categories: any;
  hasPhoto = false;
  photo: any;
  fg: FormGroup;
  error = false;
  currentPhoto = false;
  private id: number;
  erroMsg = "";

  constructor(private basicEntitiesService: BasicentitiesService, private fb: FormBuilder, private route: ActivatedRoute,
    private productsService: ProductService, @Inject('BASE_URL') public url: string, private alertService: AlertService,
    private router: Router) { }

  async ngOnInit() {

    this.route.queryParams.subscribe(p => {
      this.id = p['id'];
    });

    if (this.id === 0) {
      this.router.navigate(['/products/list']);
      return;
    };

    var res = await this.productsService.getProduct(this.id).toPromise();

    let product = res.response.result;

    this.buildForm(product);

    await this.loadCategories();

  }

  public editProduct() {

    this.error = false;
    this.erroMsg = "";
    if(!this.fg.valid) return;
    this.productsService.updateProduct(this.fg.value, this.id).subscribe(response => {
      if(response.success)
      {
        this.alertService.showSuccess("Sucesso", "Produto Atualizado Com Sucesso!", () => {
          this.router.navigate(['/products/list']);
        })
        return;
      }
      this.error = true;
      for (let notification of response.notifications) {
        this.erroMsg += `\n${notification.message}`;
      }
      return;
    });
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
      this.currentPhoto = false;
      this.fg.get('photoRequest').get('fileName').setValue(this.photo.fileName);
      this.fg.get('photoRequest').get('photoB64').setValue(this.photo.photob64.replace('data:image/jpeg;base64,', '').replace('data:image/gif;base64,', ''));
    }
  }

  private async loadCategories() {
    let response = await this.basicEntitiesService.getAll({ url: "ProductCategories", page: 0, pageSize: 45 }).toPromise();
    this.categories = response.response.result.entities;
  }

  private buildForm(product: any) {
    this.fg = this.fb.group({
      name: [product?.name, [Validators.required]],
      description: [product?.description, [Validators.required]],
      quantityStock: [product?.quantityStock, [Validators.required]],
      category: this.fb.group({
        id: [product?.category?.id, [Validators.required]]
      }),
      accompaniments: [product?.accompaniments, [Validators.required]],
      price: [product?.price, [Validators.required]],
      available: [product?.available],
      photoRequest: this.fb.group({
        fileName: [product?.photo?.fileName],
        photoB64: [product?.photo?.photoPath]
      })
    });
    this.photo = product?.photo;
    this.currentPhoto = true;
  }
}
