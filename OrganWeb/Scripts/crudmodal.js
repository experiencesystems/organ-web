$(function () {
    var placeholderElement = $('#modal-placeholder');


    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');


        $('#add-item').modal({
            show: true
        });

        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.hide();
            placeholderElement.html('button[data-toggle="ajax-modal"]');
        });
    });

    placeholderElement.on('click', '[data-save="ajax-modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal-dialog').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);

            var isValid = newBody.find('[name="IsValid"]').val() == 'True';
            if (isValid) {
                placeholderElement.find('.modal').modal('hide');
            }
        });
    });
});

$(function () {
    var placeholderElement = $('#modal-placeholder');

    $('.edit').click(function (event) {
        var url = $(this).data('url');
        $("#newmodal").load(url, function () {
            $("#newmodal").modal("show", "static");
        })

        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.hide();
            placeholderElement.html('button[data-toggle="newmodal"]');
        });
    });

    placeholderElement.on('click', '[data-save="newmodal"]', function (event) {
        event.preventDefault();
        alert("S.O.S.");

        var form = $(this).parents('.modal-dialog').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);

            var isValid = newBody.find('[name="IsValid"]').val() == 'True';
            if (isValid) {
                placeholderElement.find('.modal').modal('hide');
            }
        });
    });
});