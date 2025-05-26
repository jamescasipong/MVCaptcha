$(document).ready(function () {
    const $input = $('.captcha-input');
    const $form = $('#captchaForm');
    const $submitBtn = $('.submit-btn');

    const answerLength = parseInt($input.data('answerLength'), 10);

    $input.focus();

    $input.on('input', function () {
        const sanitized = this.value.replace(/[^A-Za-z0-9]/g, '');
        this.value = sanitized;
        $submitBtn.prop('disabled', sanitized.length < answerLength);
    });

    $form.on('submit', function (e) {
        if ($input.val().length < answerLength) {
            e.preventDefault();
            $input.addClass('error');
        }
    });

    $input.on('blur', function () {
        if ($input.val().length < answerLength) {
            $input.addClass('error');
        } else {
            $input.removeClass('error');
        }
    });
});
