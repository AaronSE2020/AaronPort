async function fetchAstronomyPicture() {
    const apiKey = "xqoYgBDv0DLkGAcJ7Ehn11Q4fk2aXWRVwNHQQYqN";
    let response = await fetch("https://api.nasa.gov/planetary/apod?api_key=" + apiKey);
    let data = await response.json();
    return data;
}

$(document).ready(function () {
    fetchAstronomyPicture().then(data => {
        $('#nasaImage').attr('src', data.url);
        $('#nasaTitle').text(data.title);
        $('#nasaDesc').text(data.explanation);
    });
});

function counter() {
    let count = 0;
    return function () {
        return count++;
    };
}
const increment = counter();
console.log(increment()); // Outputs: 0
console.log(increment()); // Outputs: 1

// Using Promises
let fetchData = new Promise((resolve, reject) => {
    setTimeout(() => {
        resolve("Data fetched!");
    }, 2000);
});

fetchData.then(data => console.log(data));

// Using Async/Await
async function getData() {
    let response = await fetchData;
    console.log(response);
}
getData();

let obj = {
    name: "John",
    sayHello: function () {
        console.log("Hello, " + this.name);
    }
};
obj.sayHello(); // Outputs: Hello, John

let globalSayHello = obj.sayHello;
globalSayHello(); // Outputs: Hello, undefined or throws an error depending on strict mode

function Person(name) {
    this.name = name;
}
Person.prototype.greet = function () {
    console.log("Hello, " + this.name);
};

function Student(name, subject) {
    Person.call(this, name);
    this.subject = subject;
}
Student.prototype = Object.create(Person.prototype);

const alice = new Student("Alice", "Math");
alice.greet(); // Outputs: Hello, Alice

// In userModule.js
export function displayUser(name) {
    console.log("User: " + name);
}

// In your main JS file
import { displayUser } from './userModule.js';
displayUser('John');

// Destructuring
let user = { firstName: "John", lastName: "Doe" };
let { firstName, lastName } = user;
console.log(firstName); // Outputs: John

// Spread
let arr1 = [1, 2, 3];
let arr2 = [...arr1, 4, 5];
console.log(arr2); // Outputs: [1, 2, 3, 4, 5]

// Rest
function gatherArgs(...args) {
    console.log(args);
}
gatherArgs(1, 2, 3); // Outputs: [1, 2, 3]

let uniqueSymbol = Symbol("This is a unique symbol");
let obj = {};
obj[uniqueSymbol] = "Secret Information";
    console.log(obj[uniqueSymbol]); // Outputs: Secret Information

