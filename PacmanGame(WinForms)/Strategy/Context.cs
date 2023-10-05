public class Context
{
    private Chasing strategy;

    public Context(Chasing strategy)
    {
        this.strategy = strategy;
    }

    public void executeStrategy(GhostTeam ghostTeam)
    {
        return strategy.chase(ghostTeam);
    }
}