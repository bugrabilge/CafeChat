namespace CafeChat.Constants
{
    public static class UsersConstants
    {
        public enum UserTypes
        {
            Admin = 1,
            CafeManager,
            CafePersonel,
        }

        public class UserStatus
        {
            public bool Id { get; set; }
            public string Name { get; set; }
        }

        public static readonly List<UserStatus> StatusList = new List<UserStatus>
        {
            new UserStatus { Id = true, Name = "Active" },
            new UserStatus { Id = false, Name = "Inactive" }
        };

    }
}
