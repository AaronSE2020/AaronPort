// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const newElement = document.createElement('p');

function fetchDataWithCallback(callback) {
    // The global setTimeout() method sets a timer which executes a function or specified
    // piece of code once the timer expires.
    setTimeout(() => {
        const data = { message: 'Data fetched with callback' };
        callback(data);
    }, 1000);
}

fetchDataWithCallback((data) => {
    console.log(data);
});
function fetchDataWithPromise() {
    return new Promise((resolve) => {
        setTimeout(() => {
            const data = { message: 'Data fetched with Promise' };
            resolve(data);
        }, 1000);
    });
}

var USD = 0;
var GBP = 0;
var EUR = 0;

// Get the container element by its ID
const container = document.getElementById('nav-container');
async function fetchBitcoinPrice() {
    try {
        const response = await fetch('https://api.coindesk.com/v1/bpi/currentprice.json');
        const data = await response.json();

        // Extract the Bitcoin price from the data
        USD = data.bpi.USD.rate;
        GBP = data.bpi.GBP.rate;
        EUR = data.bpi.EUR.rate;

        newElement.textContent = USD;
        console.log("Current Bitcoin price: "+USD+" USD");
    } catch (error) {   
        console.error('Error fetching data:', error);
    }
}

// Call the async function to fetch Bitcoin price
fetchBitcoinPrice();

// Append the new element to the container
container.appendChild(newElement);
let currentNumber = 1;

function switchNumber() {
    if (currentNumber === 1) {
        newElement.textContent = USD;
        currentNumber = 2;
    } else {
        newElement.textContent = EUR;
        currentNumber = 1;
    }
}

setInterval(switchNumber, 1000); // Switch numbers every 1000 milliseconds (1 second)

async function fetchAstronomyPicture() {
    const apiKey = "xqoYgBDv0DLkGAcJ7Ehn11Q4fk2aXWRVwNHQQYqN";
    let response = await fetch("https://api.nasa.gov/planetary/apod?api_key="+apiKey);
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