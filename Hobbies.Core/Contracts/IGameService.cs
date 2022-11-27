using Hobbies.Core.Models.Game;

namespace Hobbies.Core.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GameViewModel>> GetAllAsync();
    }
}
