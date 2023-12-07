namespace PacmanGame_WinForms_.ChainOfResponsibility
{
    public interface IHandler
    {
         IHandler SetNext(IHandler handler);

         void Handle(Ghost ghost);
    }
}
