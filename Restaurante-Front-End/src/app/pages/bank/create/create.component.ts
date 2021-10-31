import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BasicentitiesService } from './../../../services/entities/basicentities.service';
import { Component, OnInit } from '@angular/core';
import { Bank } from 'src/app/models/funcionario/employee';
import { AlertService } from 'src/app/services/alert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit {

  private bankUrl = "Banks";
  public fg: FormGroup;

  constructor(private basicEntitiesService: BasicentitiesService, private fb: FormBuilder, private alertService: AlertService, private router: Router) { }

  ngOnInit() {
    this.fg = this.fb.group({
      name: this.fb.control('', [Validators.required])
    });
  }

  public create() {
    if (!this.fg.valid) return;

    let bankEntity = {
      url: this.bankUrl,
      entityRequest: this.fg.value
    };

    this.basicEntitiesService.createEntity(bankEntity).subscribe(res => {
      if (res.success === true) {
        this.alertService.showSuccess("Criado", "Banco Salvo com Sucesso!");
      }

    });

  }

  public toBanks(): void {
    this.router.navigate(['bank/list'])
  }

}
