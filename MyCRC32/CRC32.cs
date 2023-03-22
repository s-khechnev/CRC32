using FiniteFieldLibrary;

namespace MyCRC32;

public class CRC32
{
    private readonly FiniteField.BinaryFiniteField _gf256;
    private readonly Polynomial<FiniteFieldElement> _q;

    public CRC32(byte[] bytes)
    {
        if (bytes.Length != 4)
            throw new ArgumentException("Wrong count bytes, must be 4 bytes");

        _gf256 = FiniteField.GetBinary(8, new[] { 1, 1, 0, 1, 1, 0, 0, 0, 1 });
        var coefficients = bytes.Select(item => _gf256.GetElementFromByte(item)).Append(_gf256.One).ToArray();
        _q = new Polynomial<FiniteFieldElement>(coefficients, _gf256.Zero);
    }

    public CRC32(int number) : this(BitConverter.GetBytes(number))
    {
    }

    public byte[] GetCheckSum(params byte[] message)
    {
        var coefficients = message.Select(item => _gf256.GetElementFromByte(item)).ToArray();
        var p = new Polynomial<FiniteFieldElement>(coefficients, _gf256.Zero);
        var remainder = p % _q;

        return remainder.Coefficients.Select(item => _gf256.GetBytesFromElement(item)[0]).ToArray();
    }

    public bool Check(byte[] message, byte[] actualCheckSum)
        => GetCheckSum(message).SequenceEqual(actualCheckSum);
}