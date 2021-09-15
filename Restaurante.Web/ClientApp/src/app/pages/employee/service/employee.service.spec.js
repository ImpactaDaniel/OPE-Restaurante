"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var employee_service_1 = require("./employee.service");
describe('FuncionarioService', function () {
    beforeEach(function () { return testing_1.TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = testing_1.TestBed.get(employee_service_1.EmployeeService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=employee.service.spec.js.map