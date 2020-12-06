var customPrice = document.getElementById('customPrice');
var allPrice = document.getElementById('allPrice');
var nights = document.getElementById('nights');
var roomPrice = document.getElementById('roomPrice');

customPrice.oninput = function () {
    if (customPrice.value > 0) {
        allPrice.value = customPrice.value * nights.value;
    } else {
        allPrice.value = roomPrice.value * nights.value;
    }
};