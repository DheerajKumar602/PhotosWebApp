function sendData() {
    var url = $('#url').val() ;
    //var contact = {};
    var $this = $(this);
    var Id = $('#Id').val();
    var token = $('#token').val();

    if ($('#Id').val() === '') {
        alert("Required Id");
    }
    else {
        var formData = new FormData();
        formData.append('Id', Id);
        formData.append('formFile', $('#formFile')[0].files[0]);
      //  $('.button-loader').button('loading');

        $.ajax({
            url: url,
           
            type: "Post",
            headers: {
                Authorization: 'Bearer ' + token
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
                alert("Server Error while Adding Image (Allowed Extentions are jpg,png,jpeg)");
                $this.button('reset');
                $('#btnSend').prop('disabled', false);
            },
            complete: function () {
                $('.button-loader').button('reset');
               
            }
        });

    }
}

function clearForm() {
    $('#formFile').val(null);
    frame.src = "";
}
