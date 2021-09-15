"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
exports.Phone = exports.Address = exports.Bank = exports.Account = exports.Employee = void 0;
var model_1 = require("../common/models/model");
var Employee = /** @class */ (function (_super) {
    __extends(Employee, _super);
    function Employee() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.phones = [];
        _this.accounts = [];
        return _this;
    }
    return Employee;
}(model_1.Model));
exports.Employee = Employee;
var Account = /** @class */ (function (_super) {
    __extends(Account, _super);
    function Account() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return Account;
}(model_1.Model));
exports.Account = Account;
var Bank = /** @class */ (function (_super) {
    __extends(Bank, _super);
    function Bank() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return Bank;
}(model_1.Model));
exports.Bank = Bank;
var Address = /** @class */ (function (_super) {
    __extends(Address, _super);
    function Address() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return Address;
}(model_1.Model));
exports.Address = Address;
var Phone = /** @class */ (function (_super) {
    __extends(Phone, _super);
    function Phone() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return Phone;
}(model_1.Model));
exports.Phone = Phone;
//# sourceMappingURL=employee.js.map