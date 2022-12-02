using Hobbies.Core.Models.Game;
using Hobbies.Infrastructure.Data.Models;

namespace Hobbies.Core.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameViewModel>> GetAllAsync();

        Task<IEnumerable<GameGenre>> GetGenresAsync();

        Task AddGameAsync(AddGameViewModel game);

        Task AddGameToCollectionAsync(Guid gameId, string userId);

        Task<IEnumerable<GameViewModel>> GetMineAsync(string userId);

        Task RemoveGameFromCollectionAsync(Guid gameId, string userId);

        Task<EditGameViewModel> GetForEditAsync(Guid id);

        Task EditAsync(EditGameViewModel game);

        Task DeleteAsync(Guid id);
    }
}
