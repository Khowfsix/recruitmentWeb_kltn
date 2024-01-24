using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Round;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsternShip.Data.Interfaces
{
    public interface IRoundRepository : IRepository<Round>
    {
        Task<IEnumerable<RoundModel>> GetAllRounds(string? request);
        Task<RoundModel> SaveRound(RoundModel request);
        Task<bool> UpdateRound(RoundModel request, Guid requestId);
        Task<bool> DeleteRound(Guid requestId);
    }
}
