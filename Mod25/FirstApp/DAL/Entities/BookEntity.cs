namespace FirstApp.DAL.Entities
{
    public class BookEntity
    {        
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
    }
}