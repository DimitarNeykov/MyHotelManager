window.onload = function () {
    table = $('#example').DataTable({
        paging: true
    });

}

function PrintOnlyTable() {
    table.destroy();
    $('#example').dataTable({
        "pageLength": -1
    });
    var HTML = $("body");
    HTML.html($("#example"));
    window.print();
    location.reload();
}