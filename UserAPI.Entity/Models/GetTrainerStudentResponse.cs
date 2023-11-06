namespace UserAPI.Entity.Models
{
    public class GetTrainerStudentResponse<T>
    {

        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public List<T> Role { get; set; }


    }
}
