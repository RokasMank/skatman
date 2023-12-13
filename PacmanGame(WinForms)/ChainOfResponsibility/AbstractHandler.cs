namespace PacmanGame_WinForms_.ChainOfResponsibility
{
    public abstract class AbstractHandler : IHandler
    {
        public IHandler _nextHandler;
        public virtual void Handle(Ghost ghost)
        {
            if (this._nextHandler != null)
            {
                this._nextHandler.Handle(ghost);
            }
            else
            {

            }
        }

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }
    }
}
