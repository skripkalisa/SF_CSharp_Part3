console.clear()

/*
* Конструктор, через который создаётся комментарий
*
* */
function Comment() {
    // Запросим имя
    this.author = prompt("Как вас зовут ?")
    if(this.author == null){
        this.empty = true
        return
    }

    // Запросим текст
    this.text = prompt("Оставьте отзыв")
    if(this.text == null){
        this.empty = true
        return
    }

    // Сохраним текущее время
    this.date = new Date().toLocaleString()
}
/*
* Оставить комментарий
*
* */
function addComment() {
   let comment = new Comment()
  
   // проверяем, успешно ли юзер осуществил ввод
   if(comment.empty){
       return;
   }
  
   // Запросим, хочет ли пользователь оставить полноценный отзыв или это будет обычный комментарий
   let enableLikes = confirm('Разрешить пользователям оценивать ваш отзыв?')
  
   if(enableLikes){
       // Создадим для отзыва новый объект из прототипа - комментария
       let review = Object.create(comment)
       // и добавим ему нужное свойство
       review.rate = 0;
 
       // Добавляем отзыв с возможностью пользовательских оценок
       writeComment(review)
   } else{
       // Добавим простой комментарий без возможности оценки
       writeComment(comment)
   }
}


/*
* Запишем объект на страницу
*
* */
const writeComment = review => {
    let likeCounter = '';

    // Если публикуется отзыв - добавляем ему кнопку с лайками.
    if (review.hasOwnProperty('rate')) {

        // Генерим идентификатор комментария.
        let commentId = Math.random();
        // Для кнопки лайков добавляем: идентификатор, атрибут onclick для передачи идентификатора в функцию, значок лайка, и само значение счётчика отделяем пробелом
        // Также мы добавили стиль, чтобы кнопка смотрелась лучше и не имела рамок
        likeCounter += 
            '<button id="' + 
            commentId + 
            '" style="border: none" onclick="addLike(this.id)">' + 
            `❤️ ${review.rate}</button>`
    }
    // Запишем результат 
    document.getElementsByClassName('reviews')[0].innerHTML += 
        ' <div class="review-    text">\n' + 
        `<p> <i> <b>${review['author']}</b> ${review['date']}${likeCounter}</i></p>` + 
        `<p>${review['text']}</p>` + 
        '</div>';
}
/*
* Увеличивает счётчик лайков
*
* */
function addLike(id) {
   // Найдём нужный элемент по id
   let element = document.getElementById(id);
  
   // Преобразуем текст элемента в массив, разбив его по пробелам (так как счётчик лайков у нас отделен от символа ❤️пробелом)
   let array = element.innerText.split(' ')
  
   // Вытащим искомое значение счётчика и сразу же преобразуем его в число, так как
   // при сложении любого значения со строкой в JS будет строка, а нам этого не требуется
   let resultNum = parseInt(array[array.length - 1], 10);
  
   // Увеличим счётчик
   resultNum += 1
  
   // Сохраним измененное значение обратно в массив
   array[array.length - 1] =  `${resultNum}`
  
   // Обновим текст элемента
   element.innerText = array.join(' ')
}

/*
*
* 
// Slider logic
* 
* 
 */

const sliderIcons = document.querySelectorAll('.slider-column');
const flippers = document.querySelectorAll('.flipper')

sliderIcons.forEach(icon =>{
    icon.addEventListener('click', currentSlide)
})

flippers.forEach(flipper =>{
    flipper.addEventListener('click', plusSlides)
})

let slideIndex = 0;
showSlides(slideIndex);

function getSlideIndex(obj){
    const list = Array.prototype.slice.call(sliderIcons)
    slideIndex =  list.indexOf(obj)   
}

function plusSlides() {
    if(this.classList.contains('prev')) showSlides(--slideIndex);
    else
    showSlides(++slideIndex );
}

function currentSlide() {
    getSlideIndex(this)
    showSlides(slideIndex);
}

function showSlides(n) {
    const slides = document.querySelectorAll(".slide");
    const icons = document.querySelectorAll(".img-dimmed");
    const captionText = document.querySelector("#caption");
    
    for(const slide of slides){
        slide.classList.remove('active')
        slide.style.display = 'none'
    }

    if (n >= slides.length) {slideIndex = 0}
    if (n < 0) {slideIndex = slides.length -1 }
    
    for(const icon of icons){
        icon.classList.remove('active')
    }
    slides[slideIndex].style.display = "block";
    icons[slideIndex].classList.add('active')
    captionText.innerHTML = icons[slideIndex].alt;
}