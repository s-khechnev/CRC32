using MyCRC32;

var crc32 = new CRC32(new byte[] { 1, 2, 3, 4 });
//or
crc32 = new CRC32(632745);

//Вычисление контрольной суммы сообщения
var msg = "123456789"u8.ToArray();
var checkSum = crc32.GetCheckSum(msg);

//Проверка контольной суммы
var actualCheckSum = new byte[] { 81, 23, 78, 79 };
var isIntegrity = crc32.Check(msg, actualCheckSum);