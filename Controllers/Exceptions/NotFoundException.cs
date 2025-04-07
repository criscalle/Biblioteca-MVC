namespace Biblioteca_MVC.Controllers.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) no fue encontrado")
        {
        }
    }
}
