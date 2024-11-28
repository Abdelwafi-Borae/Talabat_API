using Data.ModelViews;

namespace Data.Repositories.Account
{
    public interface IServiceAccount<T>
    {
        public Task<ModelError> Add(T model);
    }
}
