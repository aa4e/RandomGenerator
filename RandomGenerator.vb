''' <summary>
''' A random number generator based on the RNGCryptoServiceProvider.
''' </summary>
Public Class RandomGenerator

    Private ReadOnly Csp As System.Security.Cryptography.RNGCryptoServiceProvider

    ''' <summary>
    ''' Создаёт экземпляр генератора случайных чисел.
    ''' </summary>
    Public Sub New()
        Csp = New System.Security.Cryptography.RNGCryptoServiceProvider()
    End Sub

    Public Function [Next](minValue As Integer, maxExclusiveValue As Integer) As Integer
        If (minValue >= maxExclusiveValue) Then
            Throw New ArgumentOutOfRangeException("MinValue must be lower than MaxExclusiveValue.")
        End If

        Dim diff As Long = maxExclusiveValue - minValue
        Dim upperBound As Long = CLng(UInteger.MaxValue / diff * diff)

        Dim ui As UInteger
        Do
            ui = GetRandomUInt()
        Loop While (ui >= upperBound)
        Return CInt(minValue + (ui Mod diff))
    End Function

    Private Function GetRandomUInt() As UInteger
        Dim randomBytes = GenerateRandomBytes(4)
        Return BitConverter.ToUInt32(randomBytes, 0)
    End Function

    Private Function GenerateRandomBytes(bytesNumber As Integer) As Byte()
        Dim buffer As Byte() = New Byte(bytesNumber - 1) {}
        Csp.GetBytes(buffer)
        Return buffer
    End Function

End Class
