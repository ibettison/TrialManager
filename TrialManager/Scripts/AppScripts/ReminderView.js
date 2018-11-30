var RemPostData = 0;

$(".stopCount").on("ifChanged",
    function() {
        RemPostData = $(this).attr('id');
        if ($(this).is(':checked')) {
            $('#modal-reminder').modal('toggle');
        } else {
            var postData = {
                Id: RemPostData,
                Delete: false,
                TrialId: @ViewBag.Id
            };
            $.ajax({
                url: '/TrialFeasibility/ChangeReminderStatus',
                type: 'POST',
                data: postData,
                success: function(data) {
                    $("#ListOfReminders").html(data);
                }
            });
        }
    });

$('#modal-reminder').on('hide.bs.modal',
    function(e) {
        var clickedId = $(document.activeElement);
        var reminderDeletion = false;
        if (clickedId.attr('id') === 'remDelete') {
            reminderDeletion = true;
        }
        var postData = {
            Id: RemPostData,
            Delete: reminderDeletion,
            TrialId: @ViewBag.Id
        };

        $.ajax({
            url: '/TrialFeasibility/ChangeReminderStatus',
            type: 'POST',
            data: postData,
            success: function(data) {
                $("#ListOfReminders").html(data);
            }
        });
    });
