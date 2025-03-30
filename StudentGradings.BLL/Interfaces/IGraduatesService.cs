using StudentGradings.BLL.Models.UserGraduate;

namespace StudentGradings.BLL.Interfaces
{
    public interface IGraduatesService
    {
        void SendGraduate(GraduateModel order);
    }
}