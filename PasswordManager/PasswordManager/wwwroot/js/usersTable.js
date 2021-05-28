$(document).ready(function() {
    $('#super-table').DataTable( {
        "scrollX": true,
        "language": {
            url: '//cdn.datatables.net/plug-ins/1.10.24/i18n/Russian.json'
        },
        "columnDefs": [
            {
                "targets": [ 3, 4 ],
                "searchable": false,
                "orderable": false
            }
        ]
    });
} );