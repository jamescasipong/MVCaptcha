

document.addEventListener('DOMContentLoaded', function () {
    const options = document.querySelectorAll('.difficulty-option');
    const selectedInput = document.getElementById('selectedDifficulty');

    options.forEach(option => {
        if (option.dataset.value === (selectedInput.value || 'E')) {
            option.classList.add('active');
        }

        option.addEventListener('click', function () {
            options.forEach(opt => opt.classList.remove('active'));
            this.classList.add('active');
            selectedInput.value = this.dataset.value;

            // Animation
            this.style.transform = 'translateY(-5px)';
            setTimeout(() => this.style.transform = '', 300);
        });
    });

    document.querySelector('form').addEventListener('submit', function (e) {
        if (!selectedInput.value) {
            e.preventDefault();
            alert('Please select a difficulty level');
        }
    });
});