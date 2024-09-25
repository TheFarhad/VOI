namespace Tool.Encrypt;

using Scrypt;

public sealed class ScryptEncryptionService : EncryptProvider
{
    protected readonly ScryptEncoder _scryptEncoder;

    public ScryptEncryptionService()
        => _scryptEncoder = new ScryptEncoder();

    public override string Hash(string password)
        => _scryptEncoder.Encode(password);

    /// <summary>
    /// Salt must be null
    /// </summary>
    /// <returns></returns>
    public override bool Verify(string password, string hashedPassword, byte[] salt = default!)
        => _scryptEncoder.Compare(password, hashedPassword);
}
