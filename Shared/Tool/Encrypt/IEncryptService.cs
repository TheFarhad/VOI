namespace Tool.Encrypt;

using System.Text;

public interface IEncryptService
{
    byte[] Salt { get; set; }
    string Hash(string password);
    bool Verify(string password, string hashedPassword, byte[] salt);
}

public abstract class EncryptProvider : IEncryptService
{
    public byte[] Salt { get; set; } = [];

    protected byte[] Bytes(string source) => Encoding.UTF8.GetBytes(source);
    protected string GetString(byte[] source) => Encoding.UTF8.GetString(source);

    public abstract string Hash(string password);
    public abstract bool Verify(string password, string hashedPassword, byte[] salt);
}

public enum EncryptFlag : byte { Rfc, Bcrypt, Script }