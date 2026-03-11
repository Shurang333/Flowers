namespace Flowers.Models
{
    public static class CurrentUser
    {
        public static int Id { get; set; }
        public static string FullName { get; set; }
        public static string Email { get; set; }
        public static string RoleName { get; set; } = "Guest";

        public static bool IsInRole(string role)
        {
            return string.Equals(RoleName, role, System.StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsAtLeastManager()
        {
            return RoleName == "Manager" || RoleName == "Admin";
        }

        public static void Clear()
        {
            Id = 0;
            FullName = null;
            Email = null;
            RoleName = "Guest";
        }
    }
}

