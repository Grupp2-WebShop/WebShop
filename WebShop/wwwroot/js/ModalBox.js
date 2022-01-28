var DetailPostBackURL = '/Home/GetProductInfo';
$(function () {
    $(".anchorDetail").click(function () {
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { backdrop: true, keyboard: true }
        $.ajax({
            type: "GET",
            url: DetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "productId": id },
            datatype: "json",
            success: function (data) {
                $('#myModalContent').html(data);
                $('#myModal').modal(options);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
});

var EditPostBackURL = '/Home/EditClicked';
$(function () {
    $(".anchorEdit").click(function () {
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { backdrop: true, keyboard: true }
        $.ajax({
            type: "GET",
            url: EditPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "productId": id },
            datatype: "json",
            success: function (data) {
                $('#myModalContent').html(data);
                $('#myModal').modal(options);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
});

var DeletePostBackURL = '/Home/DeleteClicked';
$(function () {
    $(".anchorDelete").click(function () {
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { backdrop: true, keyboard: true }
        $.ajax({
            type: "GET",
            url: DeletePostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "productId": id },
            datatype: "json",
            success: function (data) {
                $('#myModalContent').html(data);
                $('#myModal').modal(options);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
    $("#closbtn").click(function () {
        $('#myModal').modal('hide');
    });
});

var CartPostBackURL = '/Home/GetCarttInfo';
$(function () {
    $(".anchorCartList").click(function () {
        var $buttonClicked = $(this);
        var options = { backdrop: true, keyboard: true }
        $.ajax({
            type: "GET",
            url: CartPostBackURL,
            contentType: "application/json; charset=utf-8",
            
            datatype: "json",
            success: function (data) {
                $('#CartModalContent').html(data);
                $('#CartModal').modal(options);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
    $("#closbtn").click(function () {
        $('#CartModal').modal('hide');
    });
});

var RemoveFromCartPostBackURL = '/Home/RemoveFromCart';
$(function () {
    $(".anchorRemoveFromCart").click(function () {
            var $buttonClicked = $(this);
        var Removeid = $buttonClicked.attr('removedata-id');
            var options = { backdrop: true, keyboard: true }
            $.ajax({
                type: "GET",
                url: RemoveFromCartPostBackURL,
                contentType: "application/json; charset=utf-8",
                data: { "productId": Removeid },
                datatype: "json",
                success: function (data) {
                    $('#CartModalContent').html(data);
                    $('#CartModal').modal(options);
                },
                error: function () {
                    alert("Dynamic content load failed.");
                }
            });
        });
        $("#closbtn").click(function () {
            $('#myModal').modal('hide');
        });
    });

