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

function toggleDropdown() {
    var dropdown = document.getElementById("dropdownContent");
    var arrow = document.querySelector(".arrow");

    if (dropdown.classList.contains("show")) {
        dropdown.classList.remove("show");
        arrow.style.transform = "rotate(0deg)";
    } else {
        dropdown.classList.add("show");
        arrow.style.transform = "rotate(180deg)";
    }
}

