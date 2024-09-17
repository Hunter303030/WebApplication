$(document).ready(function () {
    $('#price-input').on('input', function (e) {
        let value = $(this).val().replace(/[^\d]/g, '');
        if (value.length > 6) {
            value = value.slice(0, 6);
        }

        if (value.length > 0) {
            $(this).val('₽ ' + value);
        } else {
            $(this).val('');
        }

        $('#price-hidden').val(value);
    });

    $('#price-input').on('focus', function () {
        let value = $(this).val();
        if (value === '₽ ') {
            $(this).val('');
        }
    });

    $('#price-input').on('blur', function () {
        let value = $(this).val();
        if (value === '') {
            $(this).val('₽ ');
        }
    });
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
