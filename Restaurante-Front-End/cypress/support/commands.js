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
    cy.get('[name="nome"]').type('Daniel');
    cy.get('[name="sobrenome"]').type('Santos');
    cy.get('[name="email"]').type('danielcity1@gmail.com');
    cy.get('[name="senha"]').type('123456');
    cy.get('[name="cep"]').type('02998190');
    cy.get('[name="number"]').type('120');
    cy.get('[name="ddd"]').type('11');
    cy.get('[name="phoneNumber"]').type('910702074');
    cy.get('[name="bankId"]').type('1');
    cy.get('[name="branch"]').type('120');
    cy.get('[name="digit"]').type('0');
    cy.get('[name="accountNumber"]').type('1200');
})