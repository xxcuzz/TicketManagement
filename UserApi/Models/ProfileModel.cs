namespace UserApi.Models
{
    public class ProfileModel
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public int Timezone { get; set; }

        public decimal Balance { get; set; }
    }
}
