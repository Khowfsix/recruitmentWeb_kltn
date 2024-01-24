using InsternShip.Data.Entities;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Shift;
using System.Data.SqlTypes;

namespace InsternShip.Data.Interfaces
{
    public interface IShiftRepository : IRepository<Shift>
    {
        Task<IEnumerable<ShiftModel>> GetAllShifts(int? request);
        Task<ShiftModel> SaveShift(ShiftModel request);
        Task<bool> UpdateShift(ShiftModel request, Guid requestId);
        Task<bool> DeleteShift(Guid requestId);
    }
}