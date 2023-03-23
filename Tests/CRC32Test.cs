using System.Text;
using MyCRC32;

namespace Tests;

public class CRC32Test
{
    private CRC32 _crc;
    private string _message;
    private byte[] _byteMessage1;
    private byte[] _byteMessage2;

    private const int Number = 0x04C11DB7;

    [SetUp]
    public void Setup()
    {
        _crc = new CRC32(Number);
        _message = "0123456789";
        _byteMessage1 = Encoding.UTF8.GetBytes(_message);
        _byteMessage2 = _byteMessage1.Reverse().ToArray();
    }

    [Test]
    public void WrongCountBytesExceptionInCtorTest()
    {
        var exception = Assert.Throws<ArgumentException>(() => new CRC32(new byte[] { 1, 2, 3, 4, 5 }));

        Assert.That(exception.Message, Is.EqualTo("Wrong count bytes, must be 4 bytes"));
    }

    [Test]
    public void GetCertainCheckSumTest()
    {
        var expectedCheckSum = new byte[] { 104, 169, 157, 91 };

        var actualCheckSum = _crc.GetCheckSum(_byteMessage1);

        Assert.IsTrue(actualCheckSum.SequenceEqual(expectedCheckSum));
    }

    [Test]
    public void EqualCheckSumTrueTest()
    {
        var checkSum1 = _crc.GetCheckSum(_byteMessage1);
        var checkSum2 = _crc.GetCheckSum(_byteMessage1);

        Assert.IsTrue(checkSum1.SequenceEqual(checkSum2));
    }

    [Test]
    public void EqualCheckSumFalseTest()
    {
        var checkSum1 = _crc.GetCheckSum(_byteMessage1);
        var checkSum2 = _crc.GetCheckSum(_byteMessage2);

        Assert.IsFalse(checkSum1.SequenceEqual(checkSum2));
    }

    [Test]
    public void CheckTrueTest()
    {
        var checkSum1 = _crc.GetCheckSum(_byteMessage1);

        Assert.IsTrue(_crc.Check(_byteMessage1, checkSum1));
    }

    [Test]
    public void CheckFalseTest()
    {
        var checkSum2 = _crc.GetCheckSum(_byteMessage2);

        Assert.IsFalse(_crc.Check(_byteMessage1, checkSum2));
    }
}