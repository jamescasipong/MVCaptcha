

document.addEventListener('DOMContentLoaded', () => {
    const input = document.querySelector('.captcha-input');
    const form = document.getElementById('captchaForm');
    const submitBtn = document.querySelector('.submit-btn');

    // Auto-focus & uppercase input
    input.focus();
    input.addEventListener('input', (e) => {
        e.target.value = e.target.value.replace(/[^A-Za-z0-9]/g, '')
        submitBtn.disabled = e.target.value.length < @Model.AnswerLength;
    });

    // Prevent form submission if invalid
    form.addEventListener('submit', (e) => {
        if (input.value.length < @Model.AnswerLength) {
            e.preventDefault();
            input.classList.add('error');
        }
    });

    // Real-time validation
    input.addEventListener('blur', () => {
        if (input.value.length < @Model.AnswerLength) {
            input.classList.add('error');
        } else {
            input.classList.remove('error');
        }
    });
});