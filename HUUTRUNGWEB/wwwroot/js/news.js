
//--------------------------------------------
var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            url: '/admin/news/getall',
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
            { data: 'tittle', width: "15%" },
            { data: 'author', width: "5%" },
            {
                data: 'publishDate',
                width: "15%",
                render: function (data, type, row) {
                    if (data) {
                        let date = new Date(data); // Convert thành đối tượng Date
                        return date.toLocaleDateString('vi-VN'); // Hiển thị dd/MM/yyyy
                    }
                    return ""; // Xử lý nếu giá trị null
                }
            },
            { data: 'newsCategory.name', width: "5%" },              
            {
                data: 'id',
                render: function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/admin/news/Upsert?id=${data}" class="btn btn-primary mx-2">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                             <a onClick=Delete('/admin/news/Delete/${data}') class="btn btn-danger mx-2">
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



//const btnHam = document.querySelector('.ham-btn');
//const btnTimes = document.querySelector('.times-btn');
//const navBar = document.getElementById('nav-bar');

//btnHam.addEventListener('click', function () {
//    if (btnHam.className !== "") {
//        btnHam.style.display = "none";
//        btnTimes.style.display = "block";
//        navBar.classList.add("show-nav");
//    }
//})

//btnTimes.addEventListener('click', function () {
//    if (btnHam.className !== "") {
//        this.style.display = "none";
//        btnHam.style.display = "block";
//        navBar.classList.remove("show-nav");
//    }
//})