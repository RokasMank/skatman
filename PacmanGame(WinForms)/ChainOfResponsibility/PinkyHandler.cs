namespace PacmanGame_WinForms_.ChainOfResponsibility
{
    public class PinkyHandler : AbstractHandler
    {
        public override void Handle(Ghost ghost)
        {
            if (ghost.GetType() == typeof(Pinky))
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
