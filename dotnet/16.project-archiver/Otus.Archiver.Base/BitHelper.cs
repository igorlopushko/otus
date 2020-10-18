using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Otus.Archiver.Base
{
    public static class BitHelper
    {
        public static byte[] BitArrayToByteArray(BitArray bits)
        {
            var result = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(result, 0);
            return result;
        }

        public static byte[] IntArrayToByteArray(int[] intArray)
        {
            var bytes = new byte[intArray.Length * 4];

            for (var i = 0; i < intArray.Length; i++)
            {
                var temp = BitConverter.GetBytes(intArray[i]);
                for (var j = 0; j < temp.Length; j++)
                {
                    var index = 4 * i + j;
                    bytes[index] = temp[j];
                }
            }

            return bytes;
        }

        public static int[] ByteArrayToIntArray(byte[] bytes)
        {
            var ints = new int[bytes.Length / 4];
            
            for (var i = 0; i < bytes.Length; i += 4)
            {
                var temp = new byte[4];

                for (int j = 0; j < temp.Length; j++)
                {
                    temp[j] = bytes[i + j];
                }

                var index = i == 0 ? 0 : i / 4;
                ints[index] = BitConverter.ToInt32(temp);
            }

            return ints;
        }
    }
}