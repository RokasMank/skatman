namespace PacmanGame_WinForms_
{
    public class Run : Movement
    {
        public Run()
        {
        }

        public override void Move(Ghost ghost)
        {
            ghost.ChaseMode = false;
        }
    }
}