using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//AUTHOR : 梁振东
//DATE : 09/16/2019 15:59:54
//DESC : ****
/*由GetBytes方法重载返回的数组中的字节顺序 
(以及DoubleToInt64Bits由方法返回的整数中的位顺序和该ToString(Byte[])方法返回的十六进制字符串顺序) 取决于计算机体系结构是小字节序或大字节序。 同样, 由To IntegerValue方法和ToChar方法返回的数组中的字节顺序取决于计算机体系结构是小字节序还是大字节序。 结构的字节排序方式由IsLittleEndian属性指示, 属性在小字节序系统和false大字节序系统上返回true 。 在小字节序系统上, 低序位字节优先于高阶字节。 在大字节序系统上, 高位字节优先于低序位字节。
下表说明了将整数 1234567890 (0x499602D2) 传递给GetBytes(Int32)方法所产生的字节数组中的差异。 字
节按顺序列出, 从索引0处的字节到索引3处的字节。
小字节序	D2-02-96-49
大字节序	49-96-02-D2
由于某些方法的返回值取决于系统体系结构, 因此在传输超出计算机边界的字节数据时要小心:
如果保证发送和接收数据的所有系统都具有相同的字节序, 则不会对数据执行任何操作。
如果发送和接收数据的系统可以具有不同的字节顺序, 则始终按特定顺序传输数据。 这意味着数组中的字节顺序可能需要在发送它们之前或接收后进行反向。 常见的约定是以网络字节顺序 (大字节序顺序) 传输数据。 下面的示例提供了以网络字节顺序发送整数值的实现。*/
public class BitConvertTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int value = 12345678;
        byte[] bytes = BitConverter.GetBytes(value);
        Console.WriteLine(BitConverter.ToString(bytes));
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }
        Console.WriteLine(BitConverter.ToString(bytes));

        // Call method to send byte stream across machine boundaries.

        // Receive byte stream from beyond machine boundaries.
        Console.WriteLine(BitConverter.ToString(bytes));
        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);

        Console.WriteLine(BitConverter.ToString(bytes));
        int result = BitConverter.ToInt32(bytes, 0);
        Console.WriteLine("Original value: {0}", value);
        Console.WriteLine("Returned value: {0}", result);
    }
}
// The example displays the following output on a little-endian system:
//       4E-61-BC-00
//       00-BC-61-4E
//       00-BC-61-4E
//       4E-61-BC-00
//       Original value: 12345678
//       Returned value: 12345678
