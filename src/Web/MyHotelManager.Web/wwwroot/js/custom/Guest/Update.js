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