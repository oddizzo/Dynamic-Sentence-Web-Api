namespace Dynamic_Sentence_Web_Api.Models
{
    public class WordUnit
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public int WordTypeId { get; set; }
        public virtual WordType WordType { get; set; }
    }
}
