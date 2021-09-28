using System.Threading.Tasks;

namespace IRobotAlina.Web.Services.HeartbeatProvider
{
    public interface IHeartbeatProvider
    {
        public Task SetKnockKnock();
    }
}