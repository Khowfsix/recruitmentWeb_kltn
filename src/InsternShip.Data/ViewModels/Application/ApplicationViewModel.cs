using InsternShip.Data.ViewModels.Cv;
using InsternShip.Data.ViewModels.Position;

namespace InsternShip.Data.ViewModels.Application
{
    public class ApplicationViewModel
    {
        public Guid ApplicationId { get; set; }
        public virtual CvViewModel Cv { get; set; }
        public PositionViewModel Position { get; set; }
        public DateTime DateTime { get; set; }
        public string Company_Status { get; set; }
        public string Candidate_Status { get; set; }
        public string Priority { get; set; }
    }
}