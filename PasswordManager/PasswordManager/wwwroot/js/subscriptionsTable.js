$(document).ready(function() {
    $('#super-table').DataTable( {
        "scrollX": true,
        "language": {
            url: '//cdn.datatables.net/plug-ins/1.10.24/i18n/Russian.json'
        },
        "order": [[ 3, "asc" ]],
        "columnDefs": [
            {
                "targets": [ 4 ],
                "searchable": false,
            },
            {
                "targets": [ 1, 2, 4 ],
                "orderable": false
            }
        ]
    });
} );