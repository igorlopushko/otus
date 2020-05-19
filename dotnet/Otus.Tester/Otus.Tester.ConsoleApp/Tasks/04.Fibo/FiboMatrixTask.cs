using Otus.Tester.ConsoleApp.Base;

namespace Otus.Tester.ConsoleApp.Tasks
{
    public class FiboMatrixTask : ITask
    {
        private readonly int[,] BaseMatrix = new int[,] { { 1, 1 }, { 1, 0 } };

        public string Run(string[] data)
        {
            int number = int.Parse(data[0]);
            if (number == 0)
            {
                return 0.ToString();
            }
            var result = GetMatrixPower(BaseMatrix, number - 1);
            return result[0, 0].ToString();
        }

        private int[,] GetMatrixPower(int[,] matrix, int power)
        {
            for(var i = 2; i <= power; i++)
            {
                matrix = MultiplyMatrix(matrix, BaseMatrix);
            }

            return matrix;
        }

        private int[,] MultiplyMatrix(int[,] left, int[,] rigth)
        {
            int[,] result = new int[2,2];

            result[0, 0] = left[0, 0] * rigth[0, 0] + left[0, 1] * rigth[1, 0];
            result[0, 1] = left[0, 0] * rigth[0, 1] + left[0, 1] * rigth[1, 1];
            result[1, 0] = left[1, 0] * rigth[0, 0] + left[1, 1] * rigth[1, 0];
            result[1, 1] = left[1, 0] * rigth[0, 1] + left[1, 1] * rigth[1, 1];

            return result;
        }
    }
}
