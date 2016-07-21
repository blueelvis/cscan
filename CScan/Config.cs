namespace CScan
{
    internal struct Config
    {
        public bool EnableFiles;
        public bool EnableJson;
        public string EncryptionKey;

        public Config(bool EnableFiles = false, bool EnableJson = false, string EncryptionKey = null)
        {
            this.EnableFiles = EnableFiles;
            this.EnableJson = EnableJson;
            this.EncryptionKey = EncryptionKey;
        }
    }
}