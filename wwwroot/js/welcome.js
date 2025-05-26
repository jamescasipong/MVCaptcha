$(document).ready(function () {
    const $options = $('.difficulty-option');
    const $selectedInput = $('#selectedDifficulty');

    // Set initial active class
    $options.each(function () {
        if ($(this).data('value') === ($selectedInput.val() || 'N')) {
            $(this).addClass('active');
        }
    });

    // Handle option click
    $options.on('click', function () {
        $options.removeClass('active');
        $(this).addClass('active');
        $selectedInput.val($(this).data('value'));

        // Animation
        $(this).css('transform', 'translateY(-5px)');
        setTimeout(() => $(this).css('transform', ''), 300);
    });

    // Validate on form submit
    $('form').on('submit', function (e) {
        if (!$selectedInput.val()) {
            e.preventDefault();
            alert('Please select a difficulty level');
        }
    });
});
