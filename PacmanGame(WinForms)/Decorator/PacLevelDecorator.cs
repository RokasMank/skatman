
namespace PacmanGame_WinForms_.Decorator
{
    public class PacLevelDecorator : PacInfoDecorator
    {
        private string _pacInfo;

        public PacLevelDecorator(IPacInfo pacInfo, string info) : base(pacInfo)
        {
            _pacInfo = pacInfo.GetInfo() + info;

        }

        public override string GetInfo()
        {
            return _pacInfo;
        }
    }
}
