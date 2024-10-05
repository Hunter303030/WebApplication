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

function filterCourses() {
    var input = document.getElementById('courseSearchInput');
    var filter = input.value.toLowerCase();

    var courseList = document.getElementById('courseList');
    var courses = courseList.getElementsByClassName('course-item');

    for (var i = 0; i < courses.length; i++) {
        var courseTitle = courses[i].getElementsByClassName('course-title')[0];
        var coursePrice = courses[i].getElementsByClassName('course-description')[1];
        var courseDate = courses[i].getElementsByClassName('course-dates')[0];

        if (courseTitle && coursePrice && courseDate) {
            var titleText = courseTitle.textContent || courseTitle.innerText;
            var priceText = coursePrice.textContent || coursePrice.innerText;
            var dateText = courseDate.textContent || courseDate.innerText;

            if (titleText.toLowerCase().indexOf(filter) > -1 ||
                priceText.toLowerCase().indexOf(filter) > -1 ||
                dateText.toLowerCase().indexOf(filter) > -1) {
                courses[i].style.display = '';
            } else {
                courses[i].style.display = 'none';
            }
        }
    }
}
