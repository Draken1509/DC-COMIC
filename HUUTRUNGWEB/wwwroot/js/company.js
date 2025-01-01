var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            url: '/admin/company/getall',               
        },
      
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                },               
                width: "3%",
                class: "text-center" 
            },
            { data: 'name', width: "15%" },
            { data: 'city', width: "5%" },
            { data: 'province', width: "15%" },
            { data: 'streetAddress', width: "5%" },
            { data: 'postal', width: "5%" },
            { data: 'phoneNumber', width: "5%" },
           
            {
                data: 'id',
                render: function (data) { 
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/admin/company/Upsert?id=${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                             <a onClick=Delete('/admin/company/Delete/${data}') class="btn btn-danger mx-2">
                                <i class="bi bi-trash-fill"></i> Delete
                            </a>
                        </div>`;
                },
                width: "20%" 
            }
        ],
       

    });
}


function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
   
}

