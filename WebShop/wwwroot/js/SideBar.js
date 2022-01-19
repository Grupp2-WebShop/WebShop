CartPostBackURL = '/Home/GetCarttInfo';
$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $.ajax({
            type: "GET",
            url: CartPostBackURL,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (data) {
                $('#cartInfo').html(data);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });

        //$('#sidebar').toggleClass('active');
        //$('#content').toggleClass('active');
    });
});