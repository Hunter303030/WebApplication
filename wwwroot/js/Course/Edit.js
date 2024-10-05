﻿document.addEventListener('DOMContentLoaded', function () {
    const priceInput = document.getElementById('price-input');

    priceInput.addEventListener('input', function (e) {
        let value = this.value.replace(/\D/g, '');

        if (value.length > 6) {
            value = value.slice(0, 6);
        }

        this.value = value;
    });
});

document.getElementById('fileInput').addEventListener('change', function () {
    var fileName = this.files[0] ? this.files[0].name : "Файл не выбран";
    document.getElementById('file-name').textContent = fileName;
});

const fileInput = document.getElementById('fileInput');
const fileNameLabel = document.getElementById('file-name');
const currentPreview = document.getElementById('current-preview');
const resetPreviewButton = document.getElementById('resetPreview');

let originalPreviewSrc = currentPreview.src;

fileInput.addEventListener('change', function () {
    const file = this.files[0];

    if (file) {
        fileNameLabel.textContent = file.name;

        const reader = new FileReader();
        reader.onload = function (e) {
            currentPreview.src = e.target.result;
        };
        reader.readAsDataURL(file);
    } else {
        fileNameLabel.textContent = "Файл не выбран";
    }
});

resetPreviewButton.addEventListener('click', function () {
    currentPreview.src = originalPreviewSrc;
    fileInput.value = '';
    fileNameLabel.textContent = "Файл не выбран";
});