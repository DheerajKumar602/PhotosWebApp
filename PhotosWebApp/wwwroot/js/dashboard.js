function sendData() {
    var url = "https://localhost:7184/api/Protected/AddImage";
    //var contact = {};
    var $this = $(this);
    var Id = $('#Id').val();

    if ($('#Id').val() === '') {
        alert("Required Id");
    }
    else {
        var formData = new FormData();
        formData.append('Id', Id);
        formData.append('formFile', $('#formFile')[0].files[0]);
        $('.button-loader').button('loading');

        $.ajax({
            url: url,
           
            type: "Post",
            headers: {
                Authorization: 'Bearer ' + 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Imxhc3R1c2VyIiwianRpIjoiZGMwYmE5MDQtNWVkMy00NjQ1LWJmOGUtMjY4YjAzZWI1YjcyIiwiZW1haWwiOiJsYXN0dXNlckBnbWFpbC5jb20iLCJpYXQiOjE2ODE5OTQyNTksInJvbGUiOiJVc2VyIiwibmJmIjoxNjgxOTk0MjU5LCJleHAiOjE2ODIwODA2NTl9.GzVTCcuBhGA_4ryBYAdjFRtk0zjqVvYGTKII6jJUEbk'
            },
            data: formData,
            processData: false,  // tell jQuery not to process the data
            contentType: false,  // tell jQuery not to set contentType
            beforeSend: function () {
                $('#btnSend').prop('disabled', true);

            },
            success: function (result) {
                alert("Image Added successfully");
                $('#btnSend').prop('disabled', false);


                clearForm();
            },
            error: function (message) {
                alert("Server Error while Adding Image");
                $this.button('reset');

            },
            complete: function () {
                $('.button-loader').button('reset');
            }
        });

    }
}

function clearForm() {
    $('#formFile').val(null);
}
