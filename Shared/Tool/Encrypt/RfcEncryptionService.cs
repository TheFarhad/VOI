namespace Tool.Encrypt;

using System.Security.Cryptography;

public sealed class RfcEncryptionService : EncryptProvider
{
    private const int _keySize = 64;
    private const int _iterations = 350_000;

    public override string Hash(string password)
    {
        Salt = RandomNumberGenerator.GetBytes(_keySize);
        var hash = Pbkdf2(password, Salt);
        return Convert.ToHexString(hash);
    }

    public override bool Verify(string password, string hashedPassword, byte[] salt)
    {
        var hash = Pbkdf2(password, salt);
        return CryptographicOperations
            .FixedTimeEquals(hash, Convert.FromHexString(hashedPassword));
    }

    byte[] Pbkdf2(string password, byte[] salt)
        => Rfc2898DeriveBytes
        .Pbkdf2(Bytes(password), salt, _iterations, HashAlgorithmName.SHA512, _keySize);

}