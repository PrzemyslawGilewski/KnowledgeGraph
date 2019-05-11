$(document).ready(function () {
    $(".itemRow").hover(function () {
        $("#actionButtonsForId-" + $(this).attr("data-id")).removeClass("hidden");
    }, function () {
        $("#actionButtonsForId-" + $(this).attr("data-id")).addClass("hidden");
    });

});