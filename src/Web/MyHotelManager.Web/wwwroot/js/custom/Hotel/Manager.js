var hotelInformationButton = document.getElementById("hotelInformationButton");
var companyInformationButton = document.getElementById("companyInformationButton");
var usersInformationButton = document.getElementById("usersInformationButton");

function hotelInformation() {
    removeAllActiveClass();
    hotelInformationButton.classList.add("active");
}

function companyInformation() {
    removeAllActiveClass();
    companyInformationButton.classList.add("active");
}

function usersInformation() {
    removeAllActiveClass();
    usersInformationButton.classList.add("active");
}

function removeAllActiveClass() {
    companyInformationButton.classList.remove("active");
    usersInformationButton.classList.remove("active");
    hotelInformationButton.classList.remove("active");
}