﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                },
                width: "3%",
                class: "text-center"
            },
            { "data": "name", "width": "10%" },
            { "data": "email", "width": "10%" },
            { "data": "phoneNumber", "width": "10%" },
            { "data": "company.name", "width": "10%" },
            { "data": "role", "width": "10%" },
            {
                data: { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `
                        <div class="text-center" style="display: inline-flex; gap: 10px; align-items: center; justify-content: center;">
                            <a onclick=LockUnlock('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                <i class="bi bi-lock-fill"></i> Lock
                            </a> 
                            <a href="/admin/user/RoleManagment?userId=${data.id}" class="btn btn-danger text-white" style="cursor:pointer; width:150px;">
                                <i class="bi bi-pencil-square"></i> Permission
                            </a>
                        </div>
                        `;
                        } else {
                            return `
                        <div class="text-center" style="display: inline-flex; gap: 10px; align-items: center; justify-content: center;">
                            <a onclick=LockUnlock('${data.id}') class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="bi bi-unlock-fill"></i> UnLock
                            </a>
                            <a href="/admin/user/RoleManagment?userId=${data.id}" class="btn btn-danger text-white" style="cursor:pointer; width:150px;">
                                <i class="bi bi-pencil-square"></i> Permission
                            </a>
                        </div>
                        `;
                    }
                },
                "width": "15%"
            }

        ]
    });
}


function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}