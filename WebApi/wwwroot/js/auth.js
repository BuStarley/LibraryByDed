async function handleFormSubmit(formId, endpoint, successCallback) {
    const form = document.getElementById(formId);
    const messageDiv = document.getElementById('message');

    form.addEventListener('submit', async (e) => {
        e.preventDefault();

        const formData = new FormData(form);
        const payload = Object.fromEntries(formData.entries());

        try {
            const response = await fetch(`/api/auth/${endpoint}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(payload)
            });

            const result = await response.json();

            if (response.ok) {
                if (successCallback) successCallback(result);
                showMessage('Успешно!', 'success');
            } else {
                showMessage(result.message || 'Ошибка', 'danger');
            }
        } catch (error) {
            showMessage('Ошибка сети', 'danger');
        }
    });

    function showMessage(text, type) {
        messageDiv.textContent = text;
        messageDiv.className = `alert alert-${type}`;
        messageDiv.style.display = 'block';
    }
}

document.addEventListener('DOMContentLoaded', () => {
    if (document.getElementById('loginForm')) {
        handleFormSubmit('loginForm', 'login', (result) => {
            localStorage.setItem('authToken', result.token);
            setTimeout(() => window.location.href = 'api/auth/dashboard', 1000);
        });
    }

    if (document.getElementById('registerForm')) {
        handleFormSubmit('registerForm', 'register', (result) => {
            setTimeout(() => window.location.href = 'api/auth/login', 1000);
        });
    }
});