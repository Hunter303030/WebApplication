function showPopup(message, type = 'success') {
    const popup = document.getElementById('popup');
    popup.textContent = message;

    switch (type.toLowerCase()) {
        case 'success':
            popup.className = 'show success';
            break;
        case 'error':
            popup.className = 'show error';
            break;
        case 'warning':
            popup.className = 'show warning';
            break;
        case 'info':
            popup.className = 'show info';
            break;
        default:
            popup.className = 'show';
    }

    popup.style.display = 'block';

    setTimeout(() => {
        popup.classList.add('show');
    }, 20);

    setTimeout(() => {
        popup.classList.remove('show');
        setTimeout(() => {
            popup.style.display = 'none';
        }, 500);
    }, 3000);
}

