$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
        $('#content').toggleClass('active');
    });
});

var OrderPostBackURL = '/Home/ProceedToPayment';
$(function () {
  
    $(".ProceedOrder").click(function () {
        
        var x = document.getElementById("cartDetails");
        x.style.display = "none";
        $.ajax({
            type: "GET",
            url: OrderPostBackURL,
            contentType: "application/json; charset=utf-8",

            datatype: "json",
            success: function (data) {
                $('#proceedOrder').html(data);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });
    
});


var ConfirmPostBackURL = '/Home/NewConfirmedOrder';
function ConfirmOrder() {
    var x = document.getElementById("proceedOrder");
        x.style.display = "none";
    var y = document.getElementById("confirmedOrder");
    y.style.display = "block";
        $.ajax({
            type: "GET",
            url: ConfirmPostBackURL,
            contentType: "application/json; charset=utf-8",

            datatype: "json",
            success: function (data) {
                $('#confirmedOrder').html(data);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
}


//var CartBackURL = '/Home/GetCarttInfo';
//function GotoCart() {
//    var x = document.getElementById("cartDetails");
//    x.style.display = "block";
//    var y = document.getElementById("confirmedOrder");
//    y.style.display = "none";
//    $.ajax({
//        type: "GET",
//        url: CartBackURL,
//        contentType: "application/json; charset=utf-8",

//        datatype: "json",
//        success: function (data) {
//            $('#cartDetails').html(data);
//        },
//        error: function () {
//            alert("Dynamic content load failed.");
//        }
//    });
//}