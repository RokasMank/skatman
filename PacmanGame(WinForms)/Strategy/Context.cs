using PacmanGame_WinForms_;

public class Context
{
    private Movement strategy;

    public Context(Movement strategy)
    {
        this.strategy = strategy;
    }

    public void executeStrategy(Ghost ghost)
    {
        strategy.Move(ghost);
    }
}