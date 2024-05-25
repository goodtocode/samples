"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
require("bootstrap");
function createUsersTest() {
    const newPerson = {
        name: 'hi',
        email: 'hi@person.com',
    };
    return JSON.stringify(newPerson);
}
function getUsers(url = './person-list.json') {
    return fetch(url)
        .then(res => res.json())
        .then(res => {
        return res;
    });
}
function getUsersPromise(url = './person-list.json') {
    return fetch(url)
        .then(res => res.json())
        .then(res => {
        return res;
    });
}
function createUser(name, email) {
    fetch('http://localhost:7071/api/person-mock?name=jo&email=test', {
        method: 'GET',
        mode: "cors",
        headers: {
            accept: 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then(res => res.json())
        .then(res => {
        return res;
    })
        .catch(err => {
        console.log(err.message);
    });
}
function createUserSubmit(form) {
    const elements = form.elements;
    if (elements) {
        const newPerson = {
            name: elements['personName'].value,
            email: elements['personEmail'].value,
        };
        createUser(newPerson.name, newPerson.email);
        return JSON.stringify(newPerson);
    }
}
//# sourceMappingURL=index.js.map