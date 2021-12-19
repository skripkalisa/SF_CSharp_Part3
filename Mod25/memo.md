```cs 
static void Main(string[] args)
{
    using var db = new AppContext();
    var user1 = new User { Name = "Alice", Role = "User" };
    var user2 = new User { Name = "Bob", Role = "User" };
    var user3 = new User { Name = "Bruce", Role = "User" };
    
    // Добавление одиночного пользователя
    db.Users.Add(user1);
    
    // Добавление нескольких пользователей
    db.Users.AddRange(user2, user3);
    
    
    db.Users.Remove(user3);
    
    // Выбор всех пользователей
    var allUsers = db.Users.ToList();
    
    // Выбор пользователей с ролью "Admin"
    var admins = db.Users.Where(user => user.Role == "Admin").ToList();
    
    // Выбор первого пользователя в таблице
    var firstUser = db.Users.FirstOrDefault();
    
    if (firstUser != null) firstUser.Email = "simpleemail@gmail.com";
    db.SaveChanges();
    
    var user1 = new User { Name = "Arthur", Role = "Admin" };
    var user2 = new User { Name = "Bob", Role = "Admin" };
    var user3 = new User { Name = "Clark", Role = "User" };
    var user4 = new User { Name = "Dan", Role = "User" };
    
    // Добавляем user2 и сохраняем, чтобы получить Id
    db.Users.Add(user2);
    db.SaveChanges();
    
    db.Users.AddRange(user1, user3, user4);
    
    var user1Creds = new UserCredential { Login = "ArthurL", Password = "qwerty123" };
    var user2Creds = new UserCredential { Login = "BobJ", Password = "asdfgh585" };
    var user3Creds = new UserCredential { Login = "ClarkK", Password = "111zlt777" };
    var user4Creds = new UserCredential { Login = "DanE", Password = "zxc333vbn" };
    
    user1Creds.User = user1;
    user2Creds.UserId = user2.Id;
    user3.UserCredential = user3Creds;
    user4.UserCredential = user4Creds;
    
    // Не добавляем user1Creds в БД
    db.UserCredentials.AddRange(user2Creds, user3Creds, user4Creds);
    
    db.SaveChanges();
    
    
    // Создаем контекст для добавления данных
    using (var db = new AppContext())
    {
        // Пересоздаем базу
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    
        // Заполняем данными
        var company1 = new Company { Name = "SF" };
        var company2 = new Company { Name = "VK" };
        var company3 = new Company { Name = "FB" };
        db.Companies.AddRange(company1, company2, company3);
    
        var user1 = new User { Name = "Arthur", Role = "Admin", Company = company1 };
        var user2 = new User { Name = "Bob", Role = "Admin", Company = company2 };
        var user3 = new User { Name = "Clark", Role = "User", Company = company2 };
        var user4 = new User { Name = "Dan", Role = "User", Company = company3 };
    
        db.Users.AddRange(user1, user2, user3, user4);
    
        db.SaveChanges();
    }
    
    // Создаем контекст для выбора данных
    using (var db = new AppContext())
    {
        var usersQuery =
            from user in db.Users
            where user.CompanyId == 2
            select user;
    
        var usersQuery = db.Users.Where(u => u.CompanyId == 2);
        var usersQuery =
            from user in db.Users.Include(u => u.Company)
            where user.CompanyId == 2
            select user;
    
        // Или:
    
        var usersQuery = db.Users.Include(u => u.Company).Where(u => u.CompanyId == 2);
        var usersQuery = db.Users.Include(u => u.Company).Where(u => u.Company.Id == 2);
        var usersQuery = db.Users.Include(u => u.Company).Where(u => u.Company.Name == "VK");
        var users = usersQuery.ToList();
    
        foreach (var user in users)
        {
            // Вывод Id пользователей
            Console.WriteLine(user.Id);
        }
    }
}
```