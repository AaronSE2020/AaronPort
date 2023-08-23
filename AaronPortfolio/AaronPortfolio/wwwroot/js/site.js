// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const newElement = document.createElement('p');

function fetchDataWithCallback(callback) {
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

var GlobalBitcoinPrice = 0;

// Get the container element by its ID
const container = document.getElementById('nav-container');
async function fetchBitcoinPrice() {
    try {
        const response = await fetch('https://api.coindesk.com/v1/bpi/currentprice.json');
        const data = await response.json();

        // Extract the Bitcoin price from the data
        GlobalBitcoinPrice = data.bpi.USD.rate;
        newElement.textContent = GlobalBitcoinPrice;
        console.log("Current Bitcoin price: "+GlobalBitcoinPrice+" USD");
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

// Call the async function to fetch Bitcoin price
fetchBitcoinPrice();

// Append the new element to the container
container.appendChild(newElement);