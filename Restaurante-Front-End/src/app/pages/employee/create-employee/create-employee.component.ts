import { Component, OnInit } from "@angular/core";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { AlertService } from "src/app/services/alert.service";
import { ConsultaCepService } from "src/app/services/consulta-cep.service";
import { BasicentitiesService } from "src/app/services/entities/basicentities.service";
import {
  Employee,
  Phone,
} from "../../../models/funcionario/employee";
import { EmployeeService } from "../service/employee.service";

@Component({
  selector: "app-create-employee",
  templateUrl: "./create-employee.component.html",
  styleUrls: ["./create-employee.component.scss"],
})
export class CreateEmployeeComponent implements OnInit {
  error = false;
  erroMsg = "";
  funcionario: Employee;
  form: FormGroup;
  banks: any;

  constructor(
    private formbuilder: FormBuilder,
    private employeeService: EmployeeService,
    private consultaCepService: ConsultaCepService,
    private alertService: AlertService,
    private basicEntitiesService: BasicentitiesService
  ) { }

  ngOnInit(): void {
    this.buildForm();
    this.getBanks();
  }

  private buildForm(): void {
    this.form = this.formbuilder.group({
      name: ["", Validators.required],
      lastname: ["", Validators.required],
      email: ["", [Validators.required, Validators.email]],
      password: [
        "",
        [
          Validators.required,
          Validators.pattern(/(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])/),
        ],
      ],
      address: this.formbuilder.group({
        street: [""],
        number: ["", Validators.required],
        district: [""],
        cep: [
          "",
          [
            Validators.minLength(8),
            Validators.pattern(/[0-9]{8}/),
            Validators.required,
          ],
        ],
        city: [""],
        state: [""],
      }),
      phones: this.formbuilder.group({
        ddd: [
          "",
          [
            Validators.required,
            Validators.pattern(/\d+/g),
            Validators.maxLength(2),
          ],
        ],
        phoneNumber: [
          "",
          [
            Validators.required,
            Validators.pattern(/\d+/g),
            Validators.maxLength(10),
          ],
        ],
      }),
      account: this.formbuilder.group({
        bank: this.formbuilder.group({
          bankId: ["", Validators.required],
        }),
        branch: ["", Validators.required],
        accountNumber: ["", [Validators.required, Validators.pattern(/\d+/g)]],
        digit: ["", [Validators.required, Validators.pattern(/\d+/g)]],
      }),
      document: ["", [Validators.required, Validators.pattern(/\d+/g)]],
      birthDate: ["", Validators.required],
    });
  }

  private async getBanks() {
    let response = await this.basicEntitiesService.getAll({ url: "Banks", page: 0, pageSize: 50 }).toPromise();
    this.banks = response.response.result.entities;
    console.log(this.banks);
  }

  async consultarCep() {
    let cep = this.form.get("address").get("cep");
    if (cep.invalid) return;
    var endereco = await this.consultaCepService
      .consultaCep(cep.value)
      .toPromise();
    this.form.get("address").get("street").setValue(endereco.logradouro);
    this.form.get("address").get("state").setValue(endereco.uf);
    this.form.get("address").get("city").setValue(endereco.localidade);
    this.form.get("address").get("district").setValue(endereco.bairro);
  }

  async cadastrarFuncionario() {
    if (!this.form.valid) return;
    this.funcionario = this.getEmployee();

    this.error = false;
    this.erroMsg = "";

    let retorno = await this.employeeService
      .createEmployee(this.funcionario)
      .toPromise();

    this.error = !retorno.success;

    if (!retorno.success) {
      for (let notification of retorno.notifications) {
        this.erroMsg += `\n${notification.message}`;
      }
      return;
    }

    this.alertService.showSuccess("Sucesso", "Funcionário cadastrado com sucesso!")
  }

  private getEmployee(): Employee {
    let employee = new Employee(this.form.value);
    var phone = new Phone();
    phone.phoneNumber = this.form.get("phones").get("phoneNumber").value;
    phone.ddd = this.form.get("phones").get("ddd").value;
    employee.phones.push(phone);
    return employee;
  }
}
