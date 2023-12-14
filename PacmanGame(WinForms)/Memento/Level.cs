
using System.Collections.Generic;

namespace PacmanGame_WinForms_.Memento
{
    public class Level
    {
        private Stack<Memento> history;
        private LevelInfo levelInfo;

        public Level()
        {
            history = new Stack<Memento>();
            levelInfo = new LevelInfo();
        }

        public void Next (LevelInfo levelInfo)
        {
            var level = levelInfo.GetLevel();
            history.Push(levelInfo.TakeSnapshot());
            levelInfo.Set(++level, levelInfo.GetLives());
            Game.LevelInfo = levelInfo;
        }
        public void Previous()
        {
            levelInfo.Restore(history.Pop());
        }
    }
}
