var UCN = document.getElementById('UCN');
var PNF = document.getElementById('PNF');
var documentNumber = document.getElementById('documentNumber');
var nationalitySelect = document.getElementById('nationalitySelect');
var dateExpiryIssue = document.getElementById('dateExpiryIssue');
var city = document.getElementById('city');
var country = document.getElementById('country');
window.onload = function () {
    if (document.getElementById('countrySelect').selectedIndex === 0 && document.getElementById('citySelect').selectedIndex === 0) {
        nationalitySelect.selectedIndex = 0;
    } else {
        if (document.getElementById('countrySelect').selectedIndex === 0) {
            nationalitySelect.selectedIndex = 1;
            Nationality();
        } else {
            nationalitySelect.selectedIndex = 2;
            Nationality();
        }
    }
};
function Nationality() {

    if (nationalitySelect.selectedIndex === 2) {
        UCN.hidden = "hidden";
        PNF.hidden = "";
        documentNumber.hidden = "";
        dateExpiryIssue.hidden = "";
        city.hidden = "hidden";
        country.hidden = "";
        document.getElementById('citySelect').value = "";
        document.getElementById('UCNinput').value = null;
    } else {
        UCN.hidden = "";
        PNF.hidden = "hidden";
        documentNumber.hidden = "";
        dateExpiryIssue.hidden = "";
        city.hidden = "";
        country.hidden = "hidden";
        document.getElementById('countrySelect').value = "";
        document.getElementById('PNFinput').value = null;
    }
}