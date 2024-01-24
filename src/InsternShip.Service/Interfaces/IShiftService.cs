using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Shift;

namespace InsternShip.Service.Interfaces
{
    public interface IShiftService
    {
        Task<IEnumerable<ShiftViewModel>> GetAllShifts(int? request);

        Task<ShiftViewModel> SaveShift(ShiftAddModel request);

        Task<bool> UpdateShift(ShiftUpdateModel request, Guid requestId);

        Task<bool> DeleteShift(Guid requestId);
    }
}