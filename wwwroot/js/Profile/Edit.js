$(document).ready(function () {
    $('#phone-input').mask('+7 (000) 000-00-00');
});

document.getElementById('fileInput').addEventListener('change', function () {
    var fileName = this.files[0] ? this.files[0].name : "Файл не выбран";
    document.getElementById('file-name').textContent = fileName;
});

const fileInput = document.getElementById('fileInput');
const fileNameLabel = document.getElementById('file-name');
const currentAvatar = document.getElementById('current-avatar');
const resetAvatarButton = document.getElementById('resetAvatar');

let originalAvatarSrc = currentAvatar.src;

fileInput.addEventListener('change', function () {
    const file = this.files[0];

    if (file) {
        fileNameLabel.textContent = file.name;

        const reader = new FileReader();
        reader.onload = function (e) {
            currentAvatar.src = e.target.result;
        };
        reader.readAsDataURL(file);
    } else {
        fileNameLabel.textContent = "Файл не выбран";
    }
});

resetAvatarButton.addEventListener('click', function () {
    currentAvatar.src = originalAvatarSrc;
    fileInput.value = '';
    fileNameLabel.textContent = "Файл не выбран";
});