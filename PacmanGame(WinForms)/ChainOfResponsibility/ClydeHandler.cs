

namespace PacmanGame_WinForms_.ChainOfResponsibility
{
    public class ClydeHandler : AbstractHandler
    {
        public override void Handle(Ghost ghost)
        {
            if (ghost.GetType() == typeof(Clyde))
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
