document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("registerForm");
    const message = document.getElementById("message");
    const infoDiv = document.getElementById("info"); // Добавлено получение infoDiv

    form.addEventListener("submit", async (e) => {
        e.preventDefault();

        // Валидация паролей
        if (form.password.value !== form.confirmPassword.value) {
            showMessage("Пароли не совпадают", "red");
            return;
        }

        const formData = {
            userName: form.userName.value,
            phoneNumber: form.phoneNumber.value,
            password: form.password.value
        };

        try {
            // 1. Отправка запроса
            const response = await fetch('/api/auth/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData)
            });

            // 2. Чтение ответа (безопасно, как текст)
            const responseText = await response.text();
            let responseData;

            // 3. Попытка парсинга JSON
            try {
                responseData = responseText ? JSON.parse(responseText) : null;
            } catch {
                // Если не JSON, используем текст как сообщение об ошибке
                throw new Error(responseText || 'Неверный формат ответа сервера');
            }

            // 4. Проверка статуса ответа
            if (!response.ok) {
                throw new Error(responseData?.message || 'Ошибка регистрации');
            }

            // 5. Успешная обработка
            showMessage("Регистрация прошла успешно!", "green");
            form.reset();

            if (infoDiv) {
                infoDiv.style.display = "block";
                infoDiv.className = "alert alert-success";
                infoDiv.textContent = "Вы успешно зарегистрированы!";
            }

        } catch (error) {
            // 6. Обработка всех ошибок
            showMessage(error.message, "red");
        }
    });

    function showMessage(text, color) {
        if (message) {
            message.textContent = text;
            message.style.color = color;
        }
    }
});