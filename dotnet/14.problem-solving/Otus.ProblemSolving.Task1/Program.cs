/*
Суслик и Хома нашли кладовку с горохом и решили его поделить.

— Ты чего мой горох берёшь!
— Как твой?
— Подо мной, значит мой!
— Ах так? Тогда давай делить!
— Давай!


Суслик набрал себе a/b гороха, а Хома c/d гороха от общего количества.

Найдите дробь, которая покажет, какую часть
от общего количества гороха они себе набрали.

Ответ записать в виде несократимой дроби.
Значение каждой дроби меньше единицы,
сумма дробей не больше единицы.

Начальные данные: одна строка в виде a/b+c/d.
Строка записана именно в таком формате, вместо букв записаны числа, каждое число от 1 до 10000.
Вывод результата: ответ в виде x/y, представляющих собой несократимую дробь.
Максимальное значение дроби 1/1.

 x     a     c

--- = --- + ---

 y     b     d
  
 */

using System;

namespace Otus.ProblemSolving.Task1
{
    class Program
    {
        static int GCD(int a, int b)
        {
            if (a == b) return a;
            
            if (a == 0) return b;
            
            if (b == 0) return a;
            
            if (IsEven(a) && IsEven(b)) return GCD(a >> 1, b >> 1) * 2;
            
            if (IsEven(a) && IsOdd(b)) return GCD(a >> 1, b);
            
            if (IsOdd(a) && IsEven(b)) return GCD(a, b >> 1);
            
            if (a > b) return GCD((a - b) >> 1, b);
            return GCD(a, (b - a) >> 1);
        }

        static bool IsEven(int number)
        {
            return (number & 1) == 0;
        }
        
        static bool IsOdd(int number)
        {
            return (number & 1) == 1;
        }
        
        static void Main()
        {
            string[] line = Console.ReadLine().Split('+', '/');

            var a = int.Parse(line[0]);
            var b = int.Parse(line[1]);
            var c = int.Parse(line[2]);
            var d = int.Parse(line[3]);

            int x = a * d + b * c;
            int y = b * d;

            int gcd = GCD(x, y);
            x /= gcd;
            y /= gcd;
            
            Console.WriteLine(x + "/" + y);
        }
    }
}