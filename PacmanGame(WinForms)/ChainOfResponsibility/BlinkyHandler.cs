

namespace PacmanGame_WinForms_.ChainOfResponsibility
{
    public class BlinkyHandler : AbstractHandler
    {
        public override void Handle(Ghost ghost)
        {
            if (ghost.GetType() == typeof(Blinky))
            {
                ghost.SetMovement(new Run());
                ghost.executeStrategy();
            }
            else
            {
                base.Handle(ghost);
            }
        }
    }
}
