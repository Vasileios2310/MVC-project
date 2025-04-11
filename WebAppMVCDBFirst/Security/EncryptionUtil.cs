namespace WebAppMVCDBFirst.Security;

public static class EncryptionUtil
{
    /// <summary>
    /// Securely hashes the password using the BCrypt algorithm, which is a strong one-way hashing function.
    /// One-way hashing means you can’t reverse it back to the original password.
    /// It automatically adds a salt (a random string) to prevent the same password from having the same hash.
    /// </summary>
    /// <param name="rawPassword"></param>
    /// <returns></returns>
    public static string Encrypt(string rawPassword)
    {
        var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(rawPassword);
        return encryptedPassword;
    }
    
    public static bool IsValidPassword(string rawPassword, string cipherText)
    {
        return BCrypt.Net.BCrypt.Verify(rawPassword, cipherText);
    }
}