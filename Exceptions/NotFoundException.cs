namespace ProjetoFinalPos.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
    
    public NotFoundException(string resource, int id) 
        : base($"{resource} com ID {id} não encontrado(a)") { }
}
