namespace HMRC.CTP.Specs.Config
{
    /// <summary>
    /// Gets or sets a user Profile.
    /// </summary>
    public class User
    {
        private string username = string.Empty;
        private string password = string.Empty;

        /// <summary>
        /// Gets or sets a friendly alias for the profile e.g 'a basic user'.
        /// </summary>
        public string? Alias { get; set; }

        /// <summary>
        /// Gets or sets a the email / username for the profile.
        /// </summary>
        public string Name { get => ConfigHelper.GetEnvironmentVariableIfExists(this.username); set => this.username = value; }

        /// <summary>
        /// Gets or sets a the password for the profile.
        /// </summary>
        public string Password { get => ConfigHelper.GetEnvironmentVariableIfExists(this.password); set => this.password = value; }

        /// <summary>
        /// Gets or sets a the type for the profile e.g User/ServicePrincipal.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets a the current systemuserid of the user.
        /// </summary>
        public Guid SystemUserId { get; set; }
    }
}
