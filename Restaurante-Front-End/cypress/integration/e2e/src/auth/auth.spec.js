// type definitions for Cypress object "cy"
/// <reference types="cypress" />

describe('Authentication tests', () => {
  beforeEach(() => {
    cy.visit(`${Cypress.env('urlSite')}/employee/login`);
  });
  it('Should authenticate correctly', () => {
    cy.get('[name=email]').type(Cypress.env('email'));
    cy.get('[name=password]').type(Cypress.env('senha'));
    cy.get('#btn-login').click();
    cy.url().should('equal', `${Cypress.env('urlSite')}/`);
  });
});
