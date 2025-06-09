document.addEventListener('DOMContentLoaded', function () {
    const booksContainer = document.getElementById('books-container');
    const loadingElement = document.getElementById('loading');
    const errorElement = document.getElementById('error');

    let currentPage = 1;
    let isLoading = false;
    let hasMore = true;

    async function loadBooks() {
        if (isLoading || !hasMore) return;

        isLoading = true;
        loadingElement.style.display = 'block';
        errorElement.style.display = 'none';

        try {
            const response = await fetch(`/api/books/page?page=${currentPage}&pageSize=10`);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data = await response.json();

            // 🔹 Исправлено: проверяем data.books или data.Books в зависимости от API
            const books = data.books || data.Books || []; // Если оба варианта возможны

            if (books.length === 0) {
                hasMore = false;
                return;
            }

            books.forEach(book => {
                const bookElement = document.createElement('div');
                bookElement.className = 'book-card';
                bookElement.innerHTML = `
                    <div class="book-info">
                        <div class="book-title">${book.title}</div>
                        <div class="book-author">${book.author}</div>
                    </div>
                `;
                booksContainer.appendChild(bookElement);
            });

            currentPage++;
        } catch (error) {
            console.error('Ошибка:', error);
            errorElement.style.display = 'block';
        } finally {
            isLoading = false;
            loadingElement.style.display = 'none';
        }
    }

    window.addEventListener('scroll', function () {
        const { scrollTop, scrollHeight, clientHeight } = document.documentElement;

        if (scrollTop + clientHeight >= scrollHeight * 0.8) {
            loadBooks();
        }
    });

    loadBooks();
});