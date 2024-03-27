var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {        
    dataTable = $('#tbldata').DataTable({
        "lengthMenu": [[5, 10, 15, -1], [5, 10, 15, "All"]],
        "pageLength": 5,
        "ajax": ({
            "url":"/Admin/Product/GetAll"
        }),
        "columns": [
            {"data":"title","width":"15%"},
            {"data":"author","width":"15%"},
            {"data":"description","width":"20%"},
            {"data":"isbn","width":"10%"},
            { "data": "price", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                          <a href="/Admin/Product/Credit/${data}" class="btn btn-info">
                            <i class="fas fa-edit"></i></a>
                          <a class="btn btn-danger" onclick=Delete("/Admin/Product/Delete/${data}")>
                              <i class="fas fa-trash-alt"></i></a>
                        <div>
                    `;

                }
            }
        ]
    })
}
function Delete(url) {
    swal({
        title: "|! Delete Info |!",
        text: "WAnt to Delete?",
        buttons: true,
        icon: "warning",
        dangerModel: true
    }).then((willdelete) => {
        if (willdelete) {
            $.ajax({
                url: url,
                type: "Delete",
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
    })
}