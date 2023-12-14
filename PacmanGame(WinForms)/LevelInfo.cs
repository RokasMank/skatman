
namespace PacmanGame_WinForms_.Memento
{

    public class LevelInfo
    {
        private int Level;
        private int Lives;

        public void Set(int level, int lives)
        {
            Level = level;
            Lives = lives;
        }

        public Memento TakeSnapshot()
        {
            var levelInfo = new LevelInfo();
            levelInfo.Set(Level, Lives);
            return new Memento(levelInfo);
        }

        public void Restore(Memento memento)
        {
            var levelInfo = memento.GetState();

            Game.LevelInfo.Set(levelInfo.Level, levelInfo.Lives);
        }

        public int GetLevel()
        {
            return Level;
        }
        public int GetLives()
        {
            return Lives;
        }
    }
}
