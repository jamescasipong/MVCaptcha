

document.addEventListener('DOMContentLoaded', () => {
    const input = document.querySelector('.captcha-input');
    const form = document.getElementById('captchaForm');
    const submitBtn = document.querySelector('.submit-btn');

    const answerLength = parseInt(input.dataset.answerLength, 10);

    input.focus();
    input.addEventListener('input', (e) => {
        e.target.value = e.target.value.replace(/[^A-Za-z0-9]/g, '');
        submitBtn.disabled = e.target.value.length < answerLength;
    });

    form.addEventListener('submit', (e) => {
        if (input.value.length < answerLength) {
            e.preventDefault();
            input.classList.add('error');
        }
    });

    input.addEventListener('blur', () => {
        if (input.value.length < answerLength) {
            input.classList.add('error');
        } else {
            input.classList.remove('error');
        }
    });
});