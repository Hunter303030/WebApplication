document.addEventListener('DOMContentLoaded', function () {
    const menu = document.querySelector('.menu');
    const menuIcon = document.querySelector('.menu-icon');

    menuIcon.addEventListener('click', function (e) {
        e.preventDefault();
        menu.classList.toggle('open');
    });

    menuIcon.addEventListener('click', function (e) {
        e.preventDefault();
        menu.classList.toggle('close');
    });
});
