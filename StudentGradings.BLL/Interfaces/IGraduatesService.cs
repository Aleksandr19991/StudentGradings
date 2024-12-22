using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface IGraduatesService
    {
        void SendGraduate(GraduateModelBll order);
    }
}