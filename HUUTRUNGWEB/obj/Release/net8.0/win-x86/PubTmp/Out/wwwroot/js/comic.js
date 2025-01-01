var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            url: '/admin/comic/getall',               
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
            { data: 'writer', width: "5%" },
            { data: 'artBy', width: "15%" },
            { data: 'price', width: "5%" },
            {
                data: 'onSaleDate',
                width: "15%",
                render: function (data) {
                    if (data) {
                        // Chuyển đổi từ chuỗi ISO sang đối tượng Date
                        const date = new Date(data);
                        const day = String(date.getDate()).padStart(2, '0');
                        const month = String(date.getMonth() + 1).padStart(2, '0'); // Tháng bắt đầu từ 0
                        const year = date.getFullYear();
                        return `${day}/${month}/${year}`; // Trả về định dạng dd/MM/yyyy
                    }
                    return ''; // Nếu không có dữ liệu, trả về chuỗi rỗng
                }
            },

            { data: 'pageCount', width: "5%" },
            { data: 'rated', width: "5%" },
            { data: 'comicCategory.name', width: "15%" },
            {
                data: 'isNew',
                width: "5%",
                render: function (data, type, row) {
                    // Trả về checkbox có checked nếu isNew là true
                    return `<input class="form-check-input isNewCheckbox" type="checkbox" data-id="${row.id}" ${data ? 'checked' : ''} />`;
                },              
            },
            {
                data: 'id',
                render: function (data) { 
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/admin/comic/Upsert?id=${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                             <a onClick=Delete('/admin/comic/Delete/${data}') class="btn btn-danger mx-2">
                                <i class="bi bi-trash-fill"></i> Delete
                            </a>
                        </div>`;
                },
                width: "20%" 
            }
        ],
       

    });

    $(document).on('change', '.isNewCheckbox', function () {
        var comicId = $(this).data('id');  // Lấy ID của comic
        var isChecked = $(this).prop('checked');  // Lấy trạng thái của checkbox (checked / unchecked)

        // Gửi yêu cầu Ajax để cập nhật giá trị isNew
        $.ajax({
            url: '/admin/comic/UpdateIsNew',  // URL cập nhật
            type: 'POST',
            data: {
                id: comicId,
                isNew: isChecked
            },
            success: function (response) {
                // Xử lý khi cập nhật thành công
                toastr.success(response.message);
                dataTable.ajax.reload();
            },
            error: function (xhr, status, error) {
                // Xử lý khi có lỗi xảy ra
                toastr.error(response.message);
            }
        });
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

