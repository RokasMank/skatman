namespace PacmanGame_WinForms_
{
    public class Chase : Movement
    {
        public Chase()
        {
        }
        public override void Move(Ghost ghost)
        {
            ghost.ChaseMode = true;
        }
    }
}