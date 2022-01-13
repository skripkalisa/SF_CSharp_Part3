console.clear()
/*
// создадим объект Map для хранения сессии
let session =  new Map();
// Сохраним UserAgent
session.set("userAgent", window.navigator.userAgent)
 
 
// Запросим возраст пользователя и тоже сохраним
session.set("age", prompt("Пожалуйста, введите ваш возраст?"))
 
// Проверка на возраст и сохранение сессии
if(session.get("age") >= 18){
   let startDate = new Date().toLocaleString();
  
   alert("Приветствуем на LifeSpot! " + '\n' +  "Текущее время: " + startDate );
   session.set("startDate", startDate)
}
else{
   alert("Наши трансляции не предназначены для лиц моложе 18 лет. Вы будете перенаправлены");
   window.location.href = "http://www.google.com"
   a = true + 20 + "name"
}
 
// Вывод в консоль
for (let result of session){
   console.log(result)
}
}*/
const inp = document.querySelector("input")

function getElements(){// Получим все контейнеры с видео
   const elements = document.querySelectorAll('.video-container');
// Пробежимся в цикле и выведем все в консоль
elements.forEach(element =>{
   const videoTitle = element.innerText

   if(!videoTitle.toLowerCase().includes(inp.value.toLowerCase()))
      element.style.display = 'none'
   else 
      element.style.display = 'inline-block'
})
}

inp.addEventListener('input', getElements)