$(function () {
    $('form').submit(function (e) {
        e.preventDefault();
        var commentViewModel = {
            Body: $('#Body').val(),
            PostId: $('#postId').val(),

        };

        $.ajax({
            type: "POST",
            url: '/Comments/Create',
            data: commentViewModel,
            dataType: "json",
            success: function (result) {
                addNewLI(result);
            },
            error: function (error) {
                alert("Something went wrong, please try again later");
            }
        });
    });


    function addNewLI(model) {
        var d = model.CreatedAt.replace(/\D/g, '');
        var date = new Date(parseInt(d));
        var commentDate = date.getFullYear().toString() + '/' + (date.getMonth()+ 1) + '/' + date.getDate().toString();

        var li = '<li class="media">' +
            '<div class="media-body">' +
            '<span class="text-muted pull-right"><small>' + commentDate + '</small></span>' +
            '<p>' + model.Body + '</p>' +
            '</div>' +
            '</li>' +
            '<hr>';
        $('#comments-list').append(li);
    }
});