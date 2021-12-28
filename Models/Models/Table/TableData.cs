namespace Models.Models.Table
{
    public class TableData<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int Count { get; set; }
    }
}
