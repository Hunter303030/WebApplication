$(document).ready(function () {
    $('#phone-input').mask('+7 (000) 000-00-00');
});

document.getElementById('fileInput').addEventListener('change', function () {
    var fileName = this.files[0] ? this.files[0].name : "Файл не выбран";
    document.getElementById('file-name').textContent = fileName;
});

// Получение элементов
const fileInput = document.getElementById('fileInput');
const fileNameLabel = document.getElementById('file-name');
const currentAvatar = document.getElementById('current-avatar');
const resetAvatarButton = document.getElementById('resetAvatar');

// Сохраняем исходное изображение
let originalAvatarSrc = currentAvatar.src;

// Обработчик выбора файла
fileInput.addEventListener('change', function () {
    const file = this.files[0];

    // Если выбран файл
    if (file) {
        fileNameLabel.textContent = file.name;

        // Создание объекта URL для предварительного просмотра изображения
        const reader = new FileReader();
        reader.onload = function (e) {
            currentAvatar.src = e.target.result; // Меняем источник изображения
        };
        reader.readAsDataURL(file); // Читаем файл как URL
    } else {
        fileNameLabel.textContent = "Файл не выбран";
    }
});

// Обработчик кнопки сброса аватарки
resetAvatarButton.addEventListener('click', function () {
    currentAvatar.src = originalAvatarSrc; // Возвращаем исходную аватарку
    fileInput.value = ''; // Очищаем выбор файла
    fileNameLabel.textContent = "Файл не выбран"; // Возвращаем текст
});