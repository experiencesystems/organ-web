var Popup;
$(document).ready(function () {
    $('.tablecontainer').on('click', 'a.popup', function (e) {
        e.preventDefault();
        OpenPopup($(this).attr('href'));

    })
    function OpenPopup(pageUrl) {
        var $pageContent = $('<div/>');
        $pageContent.load(pageUrl, function () {
            $('#popupForm', $pageContent).removeData('validator');
            $('#popupForm', $pageContent).removeData('unobtrusiveValidation');
            $.validator.unobtrusive.parse('form');
        });

        $dialog = $('<div class="popupWindow" style="overflow:auto"></div>')
            .html($pageContent)
            .dialog({
                draggable: false,
                autoOpen: false,
                resizable: false,
                modal: true,
                height: 600,
                width: 650,
                close: function () {
                    $('.overlay').hide(),
                        $dialog.dialog('destroy').remove();
                }
            })

        $('.popupWindow').on('submit', '#popupForm', function (e) {
            var url = $('#popupForm')[0].action;
            $.ajax({
                type: "POST",
                url: url,
                data: $('#popupForm').serialize(),
                success: function (data) {
                    if (data.status) {
                        $dialog.dialog('close');
                        oTable.ajax.reload();
                    }
                }
            })

            e.preventDefault();
        })
        $dialog.dialog('open');
    }
})