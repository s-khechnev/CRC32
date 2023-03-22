# MyCRC-32
## Контрольная сумма, напоминающая CRC-32
### Пример использования
Для инициализации необходимо задать 4 байта или 32-битное число.
```c#
var crc32 = new CRC32(new byte[] { 1, 2, 3, 4 });
//or
var crc32 = new CRC32(632745);
```
Вычисление контрольной суммы сообщения
```c#
var msg = "123456789"u8.ToArray();
var checkSum = crc32.GetCheckSum(msg);
```
Проверка контрольной суммы
```c#
var actualCheckSum = new byte[] { 81, 23, 78, 79 };
var isIntegrity = crc32.Check(msg, actualCheckSum);
```