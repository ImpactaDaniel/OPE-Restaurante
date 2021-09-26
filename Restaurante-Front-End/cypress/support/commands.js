// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
// Cypress.Commands.add('login', (email, password) => { ... })
//
//
// -- This is a child command --
// Cypress.Commands.add('drag', { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add('dismiss', { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This will overwrite an existing command --
// Cypress.Commands.overwrite('visit', (originalFn, url, options) => { ... })
Cypress.Commands.add('fillFormCreateEmployee', () => {
    cy.get('[name="name"]').type('Daniel');
    cy.get('[name="lastName"]').type('Santos');
    cy.get('[name="email"]').type('denists88@gmail.com');
    cy.get('[name="password"]').type('Restaurante@123456');
    cy.get('[name="cep"]').type('02998190');
    cy.get('[name="number"]').type('120');
    cy.get('[name="ddd"]').type('11');
    cy.get('[name="phoneNumber"]').type('910702074');
    cy.get('[name="bankId"]').click();
    cy.get('[name="bb"]').click();
    cy.get('[name="branch"]').type('120');
    cy.get('[name="digit"]').type('0');
    cy.get('[name="accountNumber"]').type('1200');
});

Cypress.Commands.add('loginDefaultUser', () => {
  cy.visit(`${Cypress.env('urlSite')}/employee/login`)
  cy.get('[name=email]').type(Cypress.env('email'));
  cy.get('[name=password]').type(Cypress.env('senha'));
  cy.get('#btn-login').click();
  cy.wait(1000);
})
