var dataTable;

$(document).ready(function () {
    loadDataTable()
});
function loadDataTable() {
    dataTable = $('#tbldata').DataTable({
        "lengthMenu": [[3,6,9,-1],[3,6,9,"All"]],
        "pageLength":3,
        "ajax": {
            "url":"/Admin/CoverType/GetAll"
        },
        "columns": [
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-align-left">
                        <a href="/Admin/CoverType/Upsert/${data}" class="btn btn-info">
                        <i class="fas fa-edit"></i></a></div>
                    `;
                }
            },
            { "data": "name", "width": "60%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-align-left">
                        <a onclick=Delete("/Admin/CoverType/Delete/${data}") class="btn btn-danger">
                        <i class="fas fa-trash-alt"></i></a></div>
                    `;
                }
            },
        ]
    })
}
function Delete(url) {
    swal({
        title: "Delete Info",
        text: "sure Want to Delete",
        buttons: true,
        icon: "warning",
        dangerModel: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    }

    )
}