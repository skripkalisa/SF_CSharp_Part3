// Запросим возраст пользователя и сохраним в переменную
let age = prompt("Пожалуйста, введите ваш возраст");
 
if(age >= 18){
   // Те, кто старше 18, увидят приветствие и будут направлены на сайт
   alert("Приветствуем на LifeSpot! " + new Date().toLocaleString());
}
else{
   // Выполним проверку. Если введенное число < 18, либо если введено не число -
   // пользователь будет перенаправлен
   alert("Наши трансляции не предназначены для лиц моложе 18 лет. Вы будете перенаправлены");
   window.location.href = "http://www.google.com"
}