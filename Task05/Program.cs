using System;

/*
Источник: https://metanit.com/

Класс Dollar представляет сумму в долларах, а Euro - сумму в евро.

Определите операторы преобразования от типа Dollar в Euro и наоборот.
Допустим, 1 евро стоит 1,14 долларов. При этом один оператор должен подразумевать явное,
и один - неявное преобразование. Обработайте ситуации с отрицательными аргументами
(в этом случае должен быть выброшен ArgumentException).

Тестирование приложения выполняется путем запуска разных наборов тестов, например,
на вход поступает две строки - количество долларов и количество евро.
10
100
Программа должна вывести на экран количество евро и долларов, соответственно,
с использованием перегруженных операторов (округлять до 2 знаков после запятой):
8,77
114,00

Никаких дополнительных символов выводиться не должно.

Код метода Main можно подвергнуть изменениям, но вывод меняться не должен.
*/

namespace Task05
{
    class Dollar
    {
        const decimal course = 1.14M;
        public decimal Sum { get; set; }

        public Dollar(decimal sum)
        {
            if (sum < 0)
            {
                throw new ArgumentException();
            }
            this.Sum = sum;
        }

        public static explicit operator Dollar(Euro euro)
        {
            if (euro.Sum < 0)
            {
                throw new ArgumentException();
            }
            return new Dollar(euro.Sum * course);
        }

        public override string ToString()
        {
            return $"{Sum:F2}";
        }
    }
    class Euro
    {
        const decimal course = 1.14M;
        public decimal Sum { get; set; }

        public Euro(decimal sum)
        {
            if (sum < 0)
            {
                throw new ArgumentException();
            }
            this.Sum = sum;
        }

        public static explicit operator Euro(Dollar dollar)
        {
            if (dollar.Sum < 0)
            {
                throw new ArgumentException();
            }
            return new Euro(dollar.Sum / course);
        }

        public override string ToString()
        {
            return $"{Sum:F2}";
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Dollar dollar = new Dollar(decimal.Parse(Console.ReadLine()));
            Euro euro = new Euro(decimal.Parse(Console.ReadLine()));

            try
            {
                Console.WriteLine((Euro)dollar);
                Console.WriteLine((Dollar)euro);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("error");
            }
        }
    }
}
