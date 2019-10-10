$(function () {

    $("#filter").change(function () {
        var value = $(this).val();
        $("td.categoryColumn:contains('" + value + "')").parent().show();
        $("td.categoryColumn:not(:contains('" + value + "'))").parent().hide();
    });

});