// type definitions for Cypress object "cy"
/// <reference types="cypress" />

describe("Create employee tests", function () {
  beforeEach(() => {
    cy.loginDefaultUser();
    cy.visit(`${Cypress.env("urlSite")}/employee/create`);
  });

  it("Fill CEP should Fill the rest of the address", function () {
    //Visit the Demo QA Website
    cy.fillFormCreateEmployee();
    cy.wait(500);
    cy.get('[name="district"]')
      .invoke("val")
      .then((val) => expect(val).include("City"));
    cy.get('[name="street"]')
      .invoke("val")
      .then((val) => expect(val).include("Ângelo"));
    cy.get('[name="city"]')
      .invoke("val")
      .then((val) => expect(val).include("São Paulo"));
    cy.get('[name="state"]')
      .invoke("val")
      .then((val) => expect(val).include("SP"));
  });
  it("Should create new employee", () => {
    cy.fillFormCreateEmployee();

    cy.wait(500);
    cy.get("#btn_logar").click();
  });
});
