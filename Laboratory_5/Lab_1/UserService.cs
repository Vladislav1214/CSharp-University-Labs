using System.IO;
using System.Text.Json;

namespace Lab_1
{
    public static class UserService
    {
        private static List<User> _users = new List<User>();
        private static string dbFilePath = "users_database.json";

        static UserService()
        {
            LoadUsers();
        }

        public static bool RegisterUser(User user)
        {
            if (IsUsernameNotUnique(user.Username)) return false;
            if (IsEmailNotUnique(user.Email)) return false;

            _users.Add(user);
            SaveUsers();
            return true;
        }

        public static bool IsUsernameNotUnique(string username)
        {
            return _users.Any(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
        }

        public static bool IsEmailNotUnique(string email)
        {
            return _users.Any(u => u.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase));
        }

        public static User? LoginUserName(string username, string password)
        {
            string hashedPassword = PasswordHasher.Hash(password);
            return _users.FirstOrDefault(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase)
            && u.Password == hashedPassword);
        }

        public static User? LoginUserEmail(string email, string password)
        {
            string hashedPassword = PasswordHasher.Hash(password);
            return _users.FirstOrDefault(u => u.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase)
            && u.Password == hashedPassword);
        }

        private static void LoadUsers()
        {
            if (!File.Exists(dbFilePath)) return;

            try
            {
                string json = File.ReadAllText(dbFilePath);
                var users = JsonSerializer.Deserialize<List<User>>(json);
                if (users != null) _users = users;
            }
            catch { }
        }

        private static void SaveUsers()
        {
            string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dbFilePath, json);
        }
    }
}