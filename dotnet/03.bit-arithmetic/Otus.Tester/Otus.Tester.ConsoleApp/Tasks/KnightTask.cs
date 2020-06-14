using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class KnightTask : ITask
    {
        public string[] Run(string[] data)
        {
            int x = int.Parse(data[0]);
            ulong n = 1ul << x;
            ulong nA = 0xFEFEFEFEFEFEFEFE;
            ulong nAB = 0xFCFCFCFCFCFCFCFC;
            ulong nH = 0x7F7F7F7F7F7F7F7F;
            ulong nGH = 0x3F3F3F3F3F3F3F3F;

            ulong result = nGH & (n << 6 | n >> 10)
                           | nH & (n << 15 | n >> 17)
                           | nA & (n << 17 | n >> 15)
                           | nAB & (n << 10 | n >> 6);

            int count = 0;
            ulong temp = result;
            while (temp > 0)
            {
                count++;
                temp &= temp - 1;
            }

            return new[] { count.ToString(), result.ToString() };
        }
    }
}
