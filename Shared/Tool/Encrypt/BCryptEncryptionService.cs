namespace Tool.Encrypt;

using static BCrypt.Net.BCrypt;

public sealed class BCryptEncryptionService : EncryptProvider
{
    private const int _workFactor = 13;

    public override string Hash(string password)
    {
        var base64Salt = GenerateSalt(_workFactor);
        Salt = Convert.FromBase64String(base64Salt);
        return HashPassword(password, base64Salt, true);
    }

    /// <summary>
    /// Salt must be null
    /// </summary>
    /// <returns></returns>
    public override bool Verify(string password, string hashedPassword, byte[] salt = default!) =>
             EnhancedVerify(password, hashedPassword);
}