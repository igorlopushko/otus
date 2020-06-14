using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class KingTask : ITask
    {
        public string[] Run(string[] data)
        {
            int x = int.Parse(data[0]);
            ulong k = 1ul << x;
            ulong kL = k & 0xFEFEFEFEFEFEFEFE;
            ulong kR = k & 0x7F7F7F7F7F7F7F7F;

            ulong result = (kL << 7) | (k << 8) | (kR << 9) |
                           (kL >> 1) |            (kR << 1) |
                           (kL >> 9) | (k >> 8) | (kR >> 7);

            int count = 0;
            ulong temp = result;
            while (temp > 0)
            {
                count++;
                temp &= temp - 1;
            }

            return new[] {count.ToString(), result.ToString()};
        }
    }
}