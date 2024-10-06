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

let lessonCount = 0;

function addNewLessonForm() {
    const lessonList = document.getElementById('lesson-List');
    const countLesson = document.getElementById('lesson-Count');
    const courseId = lessonList.getAttribute('data-course-id');
    const count = countLesson.getAttribute('data-lesson-count');

    if (lessonList) {
        lessonCount++;
        if (lessonCount == 1) {
            const form = ` 
            <div class="form-data" id="lessonForm${lessonCount}">
                <div class="cont">
                    <form method="post" action="/Lesson/AddLesson" enctype="multipart/form-data">
                        <label class="title">Создать урок</label>
                        <div class="input-box">
                            <span class="input-title">Название</span>
                            <input name="Title" class="input-data" type="text" required />
                        </div>
                        <div class="input-box">
                            <span class="input-title textarea-title">Описание</span>
                            <textarea name="Description" class="input-data-textarea" required></textarea>
                        </div>

                        <div id="progressContainer${lessonCount}" class="progress-container" style="display:none;">
                            <progress id="progressBar${lessonCount}" value="0" max="100"></progress>
                            <span id="progressText${lessonCount}">0%</span>
                        </div>            

                        <div class="input-box">
                            <label for="fileInput${lessonCount}" class="custom-file-upload">Выберите видео</label>
                            <input name="Content" type="file" id="fileInput${lessonCount}" class="file-input" accept="video/*" onchange="handleVideoUpload(${lessonCount})" required>
                            <label id="file-name${lessonCount}">Файл не выбран</label>
                        </div>

                        <video id="lessonVideo${lessonCount}" controls="controls" class="lesson-video" style="display:none;">
                            <source id="videoSource${lessonCount}" src="" type="video/mp4" />
                        </video>

                        <input type="hidden" name="CourseId" value="${courseId}" />
                        <input type="hidden" name="Number" value="${count}" />
                        <input type="submit" class="btn" value="Добавить урок"/>
                        <button type="button" class="btn" onclick="deleteLessonForm(${lessonCount})">Удалить форму</button>
                    </form>
                </div>
            </div>
            `;
            lessonList.insertAdjacentHTML('beforeend', form);

            const videoElement = document.getElementById(`lessonVideo${lessonCount}`);
            videoElement.style.display = 'block';
           
            setTimeout(() => {
                document.querySelector(`#lessonForm${lessonCount}`).classList.add('show');
            }, 500);
        }
    }
}

function deleteLessonForm(id) {
    const form = document.getElementById(`lessonForm${id}`);
    if (form) {
        lessonCount = 0;
        form.classList.add('hide');
       
        setTimeout(() => {
            form.remove();
        }, 500);
    }
}

function handleVideoUpload(lessonCount) {
    const fileInput = document.getElementById(`fileInput${lessonCount}`);
    const videoSource = document.getElementById(`videoSource${lessonCount}`);
    const lessonVideo = document.getElementById(`lessonVideo${lessonCount}`);
    const fileNameLabel = document.getElementById(`file-name${lessonCount}`);
    const progressContainer = document.getElementById(`progressContainer${lessonCount}`);
    const progressBar = document.getElementById(`progressBar${lessonCount}`);
    const progressText = document.getElementById(`progressText${lessonCount}`);

    const file = fileInput.files[0];
    if (file) {
        fileNameLabel.textContent = file.name;

        progressContainer.style.display = 'block';

        const reader = new FileReader();
        reader.onprogress = function (event) {
            if (event.lengthComputable) {
                const percentLoaded = Math.round((event.loaded / event.total) * 100);
                progressBar.value = percentLoaded;
                progressText.textContent = `${percentLoaded}%`;
            }
        };

        reader.onloadend = function () {
            videoSource.src = URL.createObjectURL(file);
            lessonVideo.style.display = 'block';
            progressContainer.style.display = 'none';
            lessonVideo.load();
        };

        reader.onerror = function () {
            alert('Ошибка при загрузке видео.');
        };

        reader.readAsDataURL(file);
    }
}