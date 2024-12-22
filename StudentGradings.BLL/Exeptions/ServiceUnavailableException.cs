namespace StudentGradings.BLL.Exeptions;

public class ServiceUnavailableException(string message) : Exception(message)
{
}
