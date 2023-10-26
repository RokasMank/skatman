
namespace PacmanGame_WinForms_.Decorator
{
    public class PacLevelDecorator : PacInfoDecorator
    {
        public PacLevelDecorator(IPacInfo pacInfo) : base(pacInfo)
        {
        }

        public override string GetInfo(string info)
        {
            return _pacInfo + info;
        }
    }
}
