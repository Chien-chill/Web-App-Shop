// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
    document.addEventListener('DOMContentLoaded', function () {
        const navbar = document.querySelector('.navbar'); // Chọn Navbar của Bootstrap

        window.addEventListener('scroll', () => {
            if (window.scrollY > 50) {
        navbar.classList.add('scrolled'); // Thêm class khi cuộn xuống
            } else {
        navbar.classList.remove('scrolled'); // Loại bỏ class khi cuộn lên
            }
        });
    });


function initSwiper() {
    let swiperInstance;

    function applySwiper() {
        if (swiperInstance) swiperInstance.destroy(true, true); // Xóa Swiper cũ nếu đã tồn tại

        if (window.innerWidth <= 768) {
            // Độ rộng màn hình <= 768px
            swiperInstance = new Swiper(".mySwiper", {
                effect: "coverflow",
                grabCursor: true,
                centeredSlides: true,
                slidesPerView: "auto",
                coverflowEffect: {
                    rotate: 50,
                    stretch: 0,
                    depth: 100,
                    modifier: 1,
                    slideShadows: true,
                },
                pagination: {
                    el: ".swiper-pagination",
                },
            });
        } else {
            // Độ rộng màn hình > 768px
            swiperInstance = new Swiper(".mySwiper", {
                spaceBetween: 30,
                centeredSlides: true,
                autoplay: {
                    delay: 2500,
                    disableOnInteraction: false,
                },
                pagination: {
                    el: ".swiper-pagination",
                    clickable: true,
                },
                navigation: {
                    nextEl: ".swiper-button-next",
                    prevEl: ".swiper-button-prev",
                },
            });
        }
    }

    // Gọi khi trang tải lần đầu
    applySwiper();

    // Lắng nghe sự kiện resize
    window.addEventListener("resize", applySwiper);
}

// Gọi hàm khi trang được tải
document.addEventListener("DOMContentLoaded", initSwiper);

