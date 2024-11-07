namespace Entities.Exceptions;


public sealed class BookNotFoundException : NotFoundException //kalıtılamaz,hiçbir şekilde kalıtılma işlemi yapılamayacak.
{
    public BookNotFoundException(int id) : base($"The Book with id :{id} could not found.")
    {

    }
}

