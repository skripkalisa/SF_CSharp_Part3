namespace FirstApp.BLL.Models
{
    // public enum Genre
    // {
    //     Adventure,
    //     Biography,
    //     Classics,
    //     Comic,
    //     Detective,
    //     Fantasy,
    //     Fiction,
    //     Horror,
    //     Memoir,
    //     Poetry,
    //     Romance,
    //     SciFi,
    //     Suspense
    // }
    public class Book
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        
        public int UserId { get; set; }
        public bool Available { get; set; }
    }
}