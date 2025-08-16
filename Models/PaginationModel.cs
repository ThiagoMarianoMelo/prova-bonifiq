namespace ProvaPub.Models
{
    public class PaginationModel<Entity>
    {
        public List<Entity> Data { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
    }
}
