import * as $ from "jquery";
import "bootstrap";

function getUsers(url: string = './person-list.json'): Promise<Person[]> {
        return fetch(url)
                .then(res => res.json())
                .then(res => {
                        return res as Person[];
                });
}

function getUsersPromise(url: string = './person-list.json'): Promise<Person[]> {
        return fetch(url)
                .then(res => res.json())
                .then(res => {
                        return res as Person[];
                });
}

function createUser(name: string, email: string) {
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
                        return res as Person[];
                })
                .catch(err => {
                        console.log(err.message);
                });
}

function createUserSubmit(form: HTMLFormElement) {
        const elements: HTMLFormControlsCollection = form.elements;
        if (elements) {
                const newPerson: Person = {
                        name: elements['personName'].value,
                        email: elements['personEmail'].value,
                };
                createUser(newPerson.name, newPerson.email);
                return JSON.stringify(newPerson);
        }
}