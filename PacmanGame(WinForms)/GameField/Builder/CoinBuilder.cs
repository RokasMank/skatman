using PacmanGame_WinForms_.GameField.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.GameField
{
    public class CoinBuilder : IBuilder
    {
        private int x;
        private int y;

        private Coin _coin = null;

        public CoinBuilder()
        {
            
        }

        public IBuilder SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
            _coin = new Coin(this.x, this.y);
            return this;
        }

        public IBuilder BuildImage()
        {
            _coin.Image = Properties.Resources.Coin;
            return this;
        }

        public IBuilder BuildAction()
        {
            _coin.Action();
            return this;
        }
        public Coin Build() => _coin;
    }
}
