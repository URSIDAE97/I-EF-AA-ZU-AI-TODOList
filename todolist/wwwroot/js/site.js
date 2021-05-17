$(document).ready(function () {

    $('.todo-task-checkbox').on('click', function (e) {
        markCompleted(e.target);
    });

});

function markCompleted (checkbox) {
    var form = checkbox.closest('form');
    form.submit();
}
