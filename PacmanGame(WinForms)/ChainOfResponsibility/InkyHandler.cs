
namespace PacmanGame_WinForms_.ChainOfResponsibility
{
    public class InkyHandler : AbstractHandler
    {
        public override void Handle(Ghost ghost)
        {
            if (ghost.GetType() == typeof(Inky))
            {
                ghost.SetMovement(new Chase());
                ghost.executeStrategy();
            }
            else
            {
                base.Handle(ghost);
            }
        }
    }
}
