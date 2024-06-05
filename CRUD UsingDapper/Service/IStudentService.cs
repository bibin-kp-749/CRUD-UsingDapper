using CRUD_UsingDapper.Model;

namespace CRUD_UsingDapper.Service
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<int> AddStudent(StudentDto student);
        Task<dynamic> UpdateStudent(Student student);
        Task DeleteStudent(int id);
    }
}
