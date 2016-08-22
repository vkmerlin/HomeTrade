function SubmitGet(webUrl, postData, callBack) {
    $.get(webUrl, postData)
    .done(function (data) {
        callBack(data);
    });
};

function SubmitPostWithParams(webUrl, postData, callback) {
    $.ajax({
        type: "POST",
        url: webUrl,
        data: postData,
        success: callback,
        statusCode: {
            400: function (message) {
                if (message) {
                    showBlockMessage(message.responseText);
                }
                else {
                    showBlockMessage('An error occurred.');
                }
            }
        }
    });
};

function SubmitPostWithParamsNoTypes(webUrl, postData, callback) {
    $.ajax({
        type: "POST",
        url: webUrl,
        data: postData,
        contentType: false,
        processData: false,
        success: callback,
        statusCode: {
            406: function () {
                $.blockUI({ message: '<h1>Image size is not valid. Photos should be less than 5 MB and saved as JPG, PNG or GIF files.</h1>', timeout: 5000 });
            }
        }
    });
};

function blockScreen(message) {
    if (message) {
        $.blockUI({ message: '<h1>' + message + '</h1>', timeout: 0 });
    }
    else {
        $.blockUI({ timeout: 0 });
    }
};

function showBlockMessage(message) {
    if (message) {
        $.blockUI({ message: '<h1>' + message + '</h1>' });
    }
    else {
        $.blockUI();
    }
};

function unblockScreen() {
    $.unblockUI();
};