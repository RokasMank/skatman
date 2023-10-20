using PacmanGame_WinForms_.GameField.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.GameField
{
    public class EnergiserBuilder : IBuilder
    {
        private int x;
        private int y;

        private Energiser _energiser = null;


        public EnergiserBuilder()
        {
            
        }

        public IBuilder SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
            _energiser = new Energiser(this.x, this.y);
            return this;
        }

        public IBuilder BuildImage()
        {
            _energiser.Image = Properties.Resources.Energizer;
            return this;
        }

        
        public IBuilder BuildAction()
        {
            _energiser.Action();
            return this;
        }
        public Energiser Build() => _energiser;
    }
}
