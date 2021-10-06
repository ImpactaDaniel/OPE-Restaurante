import { Phone } from './../../../models/funcionario/employee';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit, Inject } from '@angular/core';
import { ConsultaCepService } from 'src/app/services/consulta-cep.service';
import { Deliveryman } from 'src/app/models/deliveryman/deliveryman';

@Component({
  selector: 'app-createDeliveryman',
  templateUrl: './create-deliveryman.component.html',
  styleUrls: ['./create-deliveryman.component.scss']
})
export class CreateDeliverymanComponent implements OnInit {

  form: FormGroup;

  constructor(private fb: FormBuilder, private consultaCepService: ConsultaCepService) { }

  ngOnInit() {
    this.buildForm();
  }

  public registerDeliveryman(): void{
    let deliveryman = this.getDeiveryman();
  }

  public async consultarCep() {
    let cep = this.form.get('address').get('cep');
    if (cep.invalid)
      return;
    var endereco = await this.consultaCepService.consultaCep(cep.value).toPromise();
    this.form.get('address').get('street').setValue(endereco.logradouro);
    this.form.get('address').get('state').setValue(endereco.uf);
    this.form.get('address').get('city').setValue(endereco.localidade);
    this.form.get('address').get('district').setValue(endereco.bairro);

  }

  private getDeiveryman(): Deliveryman {
    let employee = new Deliveryman(this.form.value);
    var phone = new Phone();
    phone.phoneNumber = this.form.get('phones').get('phoneNumber').value;
    phone.ddd = this.form.get('phones').get('ddd').value;
    employee.phones.push(phone);
    return employee;
  }

  private buildForm(): void {
    this.form = this.fb.group({
      name: ["", Validators.required],
      lastname: ["", Validators.required],
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required, Validators.pattern(/(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])/)]],
      address: this.fb.group({
        street: [""],
        number: ["", Validators.required],
        district: [""],
        cep: ["", [Validators.minLength(8), Validators.pattern(/[0-9]{8}/), Validators.required]],
        city: [""],
        state: [""],
      }),
      phones: this.fb.group({
        ddd: ["", [Validators.required, Validators.pattern(/\d+/g), Validators.maxLength(2)]],
        phoneNumber: ["", [Validators.required, Validators.pattern(/\d+/g), Validators.maxLength(10)]],
      }),
      account: this.fb.group({
        bank: this.fb.group({
          bankId: ["", Validators.required],
        }),
        branch: ["", Validators.required],
        accountNumber: ["", [Validators.required, Validators.pattern(/\d+/g)]],
        digit: ["", [Validators.required, Validators.pattern(/\d+/g)]],
      }),
      motorcycle: this.fb.group({
        model: ['', Validators.required],
        year: ['', [Validators.required, Validators.pattern(/\d+/g), Validators.maxLength(4), Validators.minLength(4)]],
        brand: ['', Validators.required]
      }),
      document: ["", [Validators.required, Validators.pattern(/\d+/g)]],
      birthDate: ["", Validators.required]
    });
  }
}
