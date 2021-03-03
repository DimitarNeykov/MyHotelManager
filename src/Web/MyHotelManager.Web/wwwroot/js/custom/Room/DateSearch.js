function availableRooms() {
    var arrivalDate = $("#arrivalDate").val();
    var returnDate = $("#returnDate").val();
    if (new Date(arrivalDate) < new Date(returnDate)) {
        $.ajax({
            cache: false,
            type: "GET",
            url: "/Rooms/AvailableRooms",
            data: { "arrivalDate": arrivalDate, "returnDate": returnDate },
            success: function (data) {
                $('#availableRooms').html(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    } else {
        $('#availableRooms').html("");
    }
};