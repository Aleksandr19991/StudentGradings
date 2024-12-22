namespace StudentGradings.BLL.Exeptions;

public class EntityConflictException(string message) : Exception(message)
{
}
