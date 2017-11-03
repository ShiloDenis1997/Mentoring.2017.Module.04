using System.Threading.Tasks;

namespace FilesDistributor.Abstract
{
    public interface IDistributor<TModel>
    {
        Task Move(TModel item);
    }
}
