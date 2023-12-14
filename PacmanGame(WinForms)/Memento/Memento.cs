namespace PacmanGame_WinForms_.Memento
{
    public class Memento : IMemento
    {
        private LevelInfo _status;

        public Memento(LevelInfo levelInfo)
        {
            _status = levelInfo;
        }
        public LevelInfo GetState()
        {
            return _status;
        }
    }
}
