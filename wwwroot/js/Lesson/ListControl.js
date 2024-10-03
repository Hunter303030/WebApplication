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

let lessonCount = 0;

function addNewLessonForm() {
    lessonCount++;

    const form = `
                <div id="lessonForm${lessonCount}" class="lesson-form">
                    <h2>Новый урок</h2>                    
                    <label>Название:</label>
                    <input type="text" id="lessonTitle${lessonCount}" class="lesson-title" /><br />
                    <label>Описание:</label>
                    <input type="text" id="lessonDescription${lessonCount}" class="lesson-description" /><br />
                    <label>Ссылка на видео:</label>
                    <input type="text" id="lessonContent${lessonCount}" class="lesson-content" /><br />

                    <button class="save-lesson-button" onclick="saveLesson(${lessonCount})">Сохранить</button>
                    <button class="delete-lesson-button" onclick="deleteLessonForm(${lessonCount})">Удалить</button>
                </div>
            `;

    document.getElementById('lessonList').insertAdjacentHTML('beforeend', form);
}

function deleteLessonForm(id) {
    const form = document.getElementById(`lessonForm${id}`);
    if (form) {
        form.remove();
    }
}

function saveLesson(id) {
    const lessonTitle = document.getElementById(`lessonTitle${id}`).value;
    const lessonDescription = document.getElementById(`lessonDescription${id}`).value;
    const lessonContent = document.getElementById(`lessonContent${id}`).value;

    const lessonData = {
        Title: lessonTitle,
        Description: lessonDescription,
        Content: lessonContent
    };

    $.ajax({
        url: '/Lesson/AddLesson',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(lessonData),
        success: function (response) {
            alert('Урок успешно сохранён!');
            deleteLessonForm(id);
        },
        error: function () {
            alert('Ошибка при сохранении урока.');
        }
    });
}
