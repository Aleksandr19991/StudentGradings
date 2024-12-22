namespace StudentGradings.BLL.Exeptions;

public class AuthorizationFailedException(string message) : Exception(message)
{
}
