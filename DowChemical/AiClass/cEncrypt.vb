Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Public Class cEncrypt
    Public Function AesEncrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""

        Dim hash(31) As Byte
        Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
        Array.Copy(temp, 0, hash, 0, 16)
        Array.Copy(temp, 0, hash, 15, 16)
        AES.Key = hash
        AES.Mode = Security.Cryptography.CipherMode.ECB
        Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
        Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
        encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        Return encrypted

        'Try
        '    Dim hash(31) As Byte
        '    Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
        '    Array.Copy(temp, 0, hash, 0, 16)
        '    Array.Copy(temp, 0, hash, 15, 16)
        '    AES.Key = hash
        '    AES.Mode = Security.Cryptography.CipherMode.ECB
        '    Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
        '    Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
        '    encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        '    Return encrypted
        'Catch ex As Exception
        'End Try
    End Function
    Public Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""

        Dim hash(31) As Byte
        Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
        Array.Copy(temp, 0, hash, 0, 16)
        Array.Copy(temp, 0, hash, 15, 16)
        AES.Key = hash
        AES.Mode = Security.Cryptography.CipherMode.ECB
        Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
        Dim Buffer As Byte() = Convert.FromBase64String(input)
        decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        Return decrypted

        'Try
        '    Dim hash(31) As Byte
        '    Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
        '    Array.Copy(temp, 0, hash, 0, 16)
        '    Array.Copy(temp, 0, hash, 15, 16)
        '    AES.Key = hash
        '    AES.Mode = Security.Cryptography.CipherMode.ECB
        '    Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
        '    Dim Buffer As Byte() = Convert.FromBase64String(input)
        '    decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        '    Return decrypted
        'Catch ex As Exception
        'End Try
    End Function

    Dim aiPass As String = "aicomnaja"
    Public Function AES_Encrypt_aiPass(ByVal input As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""

        Dim hash(31) As Byte
        Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(aiPass))
        Array.Copy(temp, 0, hash, 0, 16)
        Array.Copy(temp, 0, hash, 15, 16)
        AES.Key = hash
        AES.Mode = Security.Cryptography.CipherMode.ECB
        Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
        Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
        encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        Return encrypted

        'Try
        '    Dim hash(31) As Byte
        '    Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(aiPass))
        '    Array.Copy(temp, 0, hash, 0, 16)
        '    Array.Copy(temp, 0, hash, 15, 16)
        '    AES.Key = hash
        '    AES.Mode = Security.Cryptography.CipherMode.ECB
        '    Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
        '    Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
        '    encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        '    Return encrypted
        'Catch ex As Exception
        'End Try
    End Function
    Public Function AES_Decrypt_aiPass(ByVal input As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""

        Dim hash(31) As Byte
        Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(aiPass))
        Array.Copy(temp, 0, hash, 0, 16)
        Array.Copy(temp, 0, hash, 15, 16)
        AES.Key = hash
        AES.Mode = Security.Cryptography.CipherMode.ECB
        Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
        Dim Buffer As Byte() = Convert.FromBase64String(input)
        decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        Return decrypted

        'Try
        '    Dim hash(31) As Byte
        '    Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(aiPass))
        '    Array.Copy(temp, 0, hash, 0, 16)
        '    Array.Copy(temp, 0, hash, 15, 16)
        '    AES.Key = hash
        '    AES.Mode = Security.Cryptography.CipherMode.ECB
        '    Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
        '    Dim Buffer As Byte() = Convert.FromBase64String(input)
        '    decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
        '    Return decrypted
        'Catch ex As Exception
        'End Try
    End Function

    Dim aiPass1 As String = "aIcomNaja"
    Public Function Encrypt_aiPass(input As String) As String
        Dim EncryptionKey As String = aiPass1
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(input)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                End Using
                input = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return input
    End Function
    Public Function Decrypt_aiPass(input As String) As String
        Dim EncryptionKey As String = aiPass1
        input = input.Replace(" ", "+")
        Dim cipherBytes As Byte() = Convert.FromBase64String(input)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                End Using
                input = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return input
    End Function

End Class
