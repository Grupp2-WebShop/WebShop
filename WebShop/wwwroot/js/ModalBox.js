var TeamDetailPostBackURL = '/Home/GetProductInfo';
$(function () {
    $(".anchorDetail").click(function () {
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { backdrop: true, keyboard: true }
        $.ajax({
            type: "GET",
            url: TeamDetailPostBackURL,
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

//$('#editProduct').on('show.bs.modal', function (event) {
//    var button = $(event.relatedTarget)
//    var product = button.data('id')
//    var modal = $(this)
//    modal.find(#productName).text()
//})

$('#editProduct').on('show.bs.modal', function (e) {

    //get data-id attribute of the clicked element
    var productId = $(e.relatedTarget).data('id');

    //populate the textbox
    $(e.currentTarget).find('input[name="productId"]').val(productId);
});

var EditPostBackURL = '/Home/EditClicked';
$(function () {
    $(".anchorEdit").click(function () {
        var $buttonClicked = $(this);
        var Editid = $buttonClicked.attr('data-id');
        var options = { backdrop: true, keyboard: true }
        $.ajax({
            type: "GET",
            url: EditPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: { "productId": Editid },
            datatype: "json",
            success: function (data) {
                $('#editProductContent').html(data);
                $('#editProduct').modal(options);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
    $("#closbtn").click(function () {
        $('#editProduct').modal('hide');
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

