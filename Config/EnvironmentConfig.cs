namespace HMRC.CTP.Specs.Config
{
    using System.Collections.Generic;
    using YamlDotNet.Serialization;

    /// <summary>
    /// Class to hold the Environment Config and User Configuration
    /// </summary>
    public class EnvironmentConfig
    {
        private string environmentUrl = string.Empty;
        private string clientId = string.Empty;
        private string clientSecret = string.Empty;

        /// <summary>
        /// Gets or sets url of the environment being tested.
        /// </summary>
        [YamlMember(Alias = "url")]
        public string EnvironmentUrl { get => ConfigHelper.GetEnvironmentVariableIfExists(this.environmentUrl); set => this.environmentUrl = value; }

        /// <summary>
        /// Gets or sets the Users who are set in the configuration file.
        /// </summary>
        [YamlMember(Alias = "users")]
        public List<UserConfig>? Users { get; set; }

        /// <summary>
        /// Gets or sets the ClientId.
        /// </summary>
        [YamlMember(Alias = "clientId")]
        public string ClientId { get => ConfigHelper.GetEnvironmentVariableIfExists(this.clientId); set => this.clientId = value; }

        /// <summary>
        /// Gets or sets the ClientSecret.
        /// </summary>
        [YamlMember(Alias = "clientSecret")]
        public string ClientSecret { get => ConfigHelper.GetEnvironmentVariableIfExists(this.clientSecret); set => this.clientSecret = value; }
    }
}
