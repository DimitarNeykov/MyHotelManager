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

function availableRooms() {
    var arrivalDate = $("#arrivalDate").val();
    var returnDate = $("#returnDate").val();
    if (new Date(arrivalDate) < new Date(returnDate)) {

        var diffDays = (date, otherDate) => Math.ceil(Math.abs(date - otherDate) / (1000 * 60 * 60 * 24));

        document.getElementById('nights').value = diffDays(new Date(returnDate), new Date(arrivalDate));

        document.getElementById('roomId').value = null;
        document.getElementById('roomNumber').value = null;
        document.getElementById('roomType').value = null;
        document.getElementById('roomPrice').value = 0;

        document.getElementById('MaxAdultCount').value = null;
        document.getElementById('MaxChildCount').value = null;

        if (customPrice.value > 0) {
            allPrice.value = customPrice.value * nights.value;
        } else {
            allPrice.value = roomPrice.value * nights.value;
        }
        var reservationId = $("#reservationId").val();
        $.ajax({
            cache: false,
            type: "GET",
            url: "/Rooms/AvailableRoomsWithReservationRoom",
            data: { "arrivalDate": arrivalDate, "returnDate": returnDate, "reservationId": reservationId },
            success: function (data) {
                $('#availableRooms').html(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    } else {
        $('#availableRooms').html("");
        document.getElementById('nights').value = 0;
        document.getElementById('roomId').value = null;
        document.getElementById('roomNumber').value = null;
        document.getElementById('roomType').value = null;
        document.getElementById('roomPrice').value = 0;
        document.getElementById('MaxAdultCount').value = null;
        document.getElementById('MaxChildCount').value = null;
    }
};

function selectRoom(siteUrl) {
    var roomId = arguments[0];

    $.ajax({
        cache: false,
        type: "GET",
        url: "/Rooms/GetRoomByIdInJson",
        data: { "roomId": roomId },
        success: function (data) {
            document.getElementById('roomId').value = data['id'];
            document.getElementById('roomNumber').value = data['number'];
            document.getElementById('roomType').value = data['roomTypeName'];
            document.getElementById('roomPrice').value = data['price'];
            document.getElementById('MaxAdultCount').value = data['maxAdultCount'];
            document.getElementById('MaxChildCount').value = data['maxChildCount'];
            if (customPrice.value > 0) {
                allPrice.value = customPrice.value * nights.value;
            } else {
                allPrice.value = roomPrice.value * nights.value;
            }
            $('#availableRooms').html("");
        },
        error:
            function (xhr, ajaxOptions, thrownError) {
            }
    });
}