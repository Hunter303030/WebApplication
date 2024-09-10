const passwordInput = document.getElementById('password');
const confirmPasswordInput = document.getElementById('confirm-password');
const submitBtn = document.getElementById('submit-btn');

passwordInput.addEventListener('input', checkPassword);
confirmPasswordInput.addEventListener('input', checkPassword);

function checkPassword() {
    const password = passwordInput.value;
    const confirmPassword = confirmPasswordInput.value;

    const hasUpperCase = /[A-ZА-Я]/.test(password);
    const hasNumber = /\d/.test(password);
    const hasSpecialChar = /[\W_]/.test(password);

    let passwordValid = true;

    if (password.length < 8) {
        passwordInput.classList.remove('yellow-border', 'green-border');
        passwordInput.classList.add('red-border');
        passwordValid = false;
    } else if (hasUpperCase && hasNumber && hasSpecialChar) {
        passwordInput.classList.remove('yellow-border', 'red-border');
        passwordInput.classList.add('green-border');
    } else if (hasUpperCase && hasNumber) {
        passwordInput.classList.remove('green-border', 'red-border');
        passwordInput.classList.add('yellow-border');
    } else {
        passwordInput.classList.remove('yellow-border', 'green-border');
        passwordInput.classList.add('red-border');
        passwordValid = false;
    }

    if (password !== confirmPassword || confirmPassword === "") {
        confirmPasswordInput.classList.remove('green-border');
        confirmPasswordInput.classList.add('red-border');
        passwordValid = false;
    } else {
        confirmPasswordInput.classList.remove('red-border');
        confirmPasswordInput.classList.add('green-border');
    }

    submitBtn.disabled = !passwordValid;
}

$(document).ready(function () {
    $('#phone-input').mask('+7 (000) 000-00-00');
});