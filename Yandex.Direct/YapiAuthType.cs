namespace Yandex.Direct
{
    /// <summary>
    /// API authorization type
    /// </summary>
    public enum YapiAuthType
    {
        /// <summary>
        /// Use OAuth2 auth
        /// </summary>
        Token,

        /// <summary>
        /// Sign API requests with ceritficate
        /// </summary>
        Certificate,
    }
}