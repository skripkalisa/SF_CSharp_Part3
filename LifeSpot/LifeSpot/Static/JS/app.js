const inp = document.querySelector(".form__input_search")

function getElements() {// Получим все контейнеры с видео
    const elements = document.querySelectorAll('.video-container');
// Пробежимся в цикле и выведем все в консоль
    elements.forEach(element => {
        const videoTitle = element.innerText

        if (!videoTitle.toLowerCase().includes(inp.value.toLowerCase()))
            element.style.display = 'none'
        else
            element.style.display = 'inline-block'
    })
}

inp.addEventListener('input', getElements)

/*
* Всплывающее окно будет показано по таймауту
* 
* */
setTimeout(() =>
        alert("Нравится LifeSpot? " + '\n' + "Подпишитесь на наш Instagram @lifespot999!"),
    30000);
