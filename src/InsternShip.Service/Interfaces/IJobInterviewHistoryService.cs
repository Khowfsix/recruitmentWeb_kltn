using InsternShip.Data.Models;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Application;

namespace InsternShip.Service.Interfaces{
    public interface IJobInterviewHistoryService{
        Task<PositionModel> GetPosition(Guid id);
        Task<List<ApplicationHistoryViewModel>> GetApplicationHistory(Guid candidateId);
        Task<InterviewerModel> GetInterviewerInformation(Guid id);
        //Task<bool> GetRoomInformation();
        Task<CvModel> GetCV(Guid id); 
        Task<RoomModel> GetRoomInformation(Guid id);
    }
}