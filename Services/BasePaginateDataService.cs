using ProvaPub.Models;

namespace ProvaPub.Services
{
    public abstract class BasePaginateDataService<Entity>
    {
        protected PaginationModel<Entity> CreatePaginatedResponse(List<Entity> data)
        {
            var totalCount = data.Count;

            var hasNext = totalCount == 11;

            if (hasNext)
            {
                data.Remove(data[10]);
                totalCount = totalCount - 1;
            }

            return new PaginationModel<Entity>() { HasNext = hasNext, TotalCount = totalCount, Data = data };
        }
    }
}
