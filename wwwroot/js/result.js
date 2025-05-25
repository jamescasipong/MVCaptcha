// Global error handling
$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
    if (jqxhr.status === 400) {
        alert(`Validation error: ${jqxhr.responseJSON?.title || thrownError}`);
    } else if (jqxhr.status === 500) {
        alert('Server error occurred. Please try again.');
    }
});

// Initialize tooltips
$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});