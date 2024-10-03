document.addEventListener("DOMContentLoaded", function () {
    const statuses = document.querySelectorAll('.course-status');

    statuses.forEach(function (statusElement) {
        const status = statusElement.getAttribute('data-status');

        if (status === 'Подтверждено') {
            statusElement.classList.add('approved');
        } else if (status === 'Проверка') {
            statusElement.classList.add('pending');
        } else if (status === 'Отклонено') {
            statusElement.classList.add('rejected');
        }
    });
});

function filterLessons() {
    var input = document.getElementById('lessonSearchInput');
    var filter = input.value.toLowerCase();

    var lessonList = document.getElementById('lessonList');
    var lessons = lessonList.getElementsByClassName('lesson-item');

    for (var i = 0; i < lessons.length; i++) {
        var lessonTitle = lessons[i].getElementsByClassName('lesson-title')[0];

        if (lessonTitle) {
            var titleText = lessonTitle.textContent || lessonTitle.innerText;

            if (titleText.toLowerCase().indexOf(filter) > -1) {
                lessons[i].style.display = '';
            } else {
                lessons[i].style.display = 'none';
            }
        }
    }
}
