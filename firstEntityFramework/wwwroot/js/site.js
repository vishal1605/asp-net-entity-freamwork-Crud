const tbody = document.getElementById('tbody-row');
const formSubmission = document.getElementById('form-submission');
var myModal = new bootstrap.Modal(document.getElementById('exampleModal'));

(function initSetup() {
    getAllData();
}());

formSubmission.onsubmit = function (e) {

    e.preventDefault();

    let formData = new FormData(e.target);
    let email = formData.get('Email');
    let name = formData.get('Name');
    let psw = formData.get('Psw');
    let allData = {
        Email: email,
        Name: name,
        Psw: psw
    }
    if (!(e.target.children[3].getAttribute('data-operation') == 'update')) {
        $.ajax({
            type: 'POST',
            url: '/Home/Insert',
            data: allData,
            success: function (responseObject) {
                myModal.hide();
                getAllData();
                
            }
        })
    } else {
        let updateData = { ...allData, UID: e.target.children[3].value }
        $.ajax({
            type: 'POST',
            url: '/Home/Update',
            data: updateData,
            success: function (responseObject) {
                myModal.hide();
                getAllData();
                e.target.children[3].setAttribute('data-operation', '');
                e.target.children[3].value = '';

            }
        })
    }

    formSubmission.querySelectorAll('input').forEach(e => {
        e.value = '';
    })
}

function getAllData() {
    tbody.innerHTML = "";
    $.ajax({
        type: 'GET',
        url: '/Home/AllData',
        success: function (responseObject) {
            responseObject.forEach(e => {
                let row = document.createElement('tr');
                row.id = e.uid;
                row.innerHTML = `
                                <td>${e.uid}</td>
                                <td>${e.email}</td>
                                <td>${e.name}</td>
                                <td>${e.psw}</td>
                                <td>${e.entryDateTime}</td>
                                <td>${e.updateDate}</td>
                                <td><button class="btn btn-primary" value="${e.uid}" onclick="updateUser(event)">Edit</button></td>
                                <td><button class="btn btn-danger" value="${e.uid}" onclick="deleteUser(event)">Delete</button></td>
                                `;
                tbody.append(row);
            })
        }
    })
}

function deleteUser(e) {
    $.ajax({
        type: 'GET',
        url: '/Home/Delete',
        data: {
            BtDeleted: 1,
            UID: e.target.value
        },
        success: function (responseObject) {
            getAllData();
        }
    })
}

function updateUser(e) {
    $.ajax({
        type: 'GET',
        url: '/Home/GetById',
        data: {
            UID: e.target.value
        },
        success: function (responseObject) {
            console.log(responseObject)
            formSubmission.querySelectorAll('input').forEach(e => {
                if (e.name == 'Email')
                    e.value = responseObject.email;
                if (e.name == 'Name')
                    e.value = responseObject.name;
                if (e.name == 'Psw')
                    e.value = responseObject.psw;
            })
            formSubmission.children[3].value = responseObject.uid;
        }
    })
    myModal.show();
    formSubmission.children[3].setAttribute('data-operation', 'update');
}