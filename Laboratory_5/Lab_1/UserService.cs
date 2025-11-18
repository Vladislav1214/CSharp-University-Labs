using System.IO;
using System.Text.Json;

namespace Lab_1
{
    public static class UserService
    {
        private static List<User> _users = new List<User>();

        private static string dbFilePath = "users_database.json";

        public static bool RegisterUser(User user)
        {
            if (_users.Any(u => u.Username == user.Username))
                return false;

            _users.Add(user);
            SaveUsers();
            return true;
        }

        public static bool UserUniqueEmail(User user)
        {
            if (_users.Any(u => u.Email == user.Email))
                return false;

            return true;
        }

        public static User? LoginUserName(string username, string password)
        {
            password = PasswordHasher.Hash(password);

            var user = _users.FirstOrDefault(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase) && u.Password == password);

            return user;
        }

        public static User? LoginUserEmail(string email, string password)
        {
            password = PasswordHasher.Hash(password);

            var user = _users.FirstOrDefault(u => u.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase) && u.Password == password);

            return user;
        }

        public static async void LoadUsers()
        {
            if (!File.Exists(dbFilePath))
                return;

            var fileInfo = new FileInfo(dbFilePath);
            if (fileInfo.Length == 0)
                return;

            try
            {
                await using (FileStream fs = new FileStream(dbFilePath, FileMode.Open))
                {
                    var users = await JsonSerializer.DeserializeAsync<List<User>>(fs);

                    _users = users ?? new List<User>();
                }
            }
            catch (JsonException ex)
            {
                return;
            }

        }

        public static async void SaveUsers()
        {
            using (FileStream fs = new FileStream(dbFilePath, FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<List<User>>(fs, _users);
            }
        }
    }
}
