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