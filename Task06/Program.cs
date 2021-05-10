using System;

/*
Источник: https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/operator-overloading

Fraction - упрощенная структура, представляющая рациональное число.
Необходимо перегрузить операции:
+ (бинарный)
- (бинарный)
*
/ (в случае деления на 0, выбрасывать DivideByZeroException)

Тестирование приложения выполняется путем запуска разных наборов тестов, например,
на вход поступает две строки, содержацие числители и знаменатели двух дробей, разделенные /, соответственно.
1/3
1/6
Программа должна вывести на экран сумму, разность, произведение и частное двух дробей, соответственно,
с использованием перегруженных операторов (при необходимости, сокращать дроби):
1/2
1/6
1/18
2

Обратите внимание, если дробь имеет знаменатель 1, то он уничтожается (2/1 => 2). Если дробь в числителе имеет 0, то 
знаменатель также уничтожается (0/3 => 0).
Никаких дополнительных символов выводиться не должно.

Код метода Main можно подвергнуть изменениям, но вывод меняться не должен.
*/

public struct Fraction
{
    private int num;
    private int den;

    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException();
        }
        this.num = numerator;
        this.den = denominator;
    }

    public static Fraction Parse(string input)
    {
        if (input.Length == 1 || input.Length == 2)
        {
            return new Fraction(int.Parse(input), 1);
        }
        string[] parameters = input.Split('/');

        return new Fraction(int.Parse(parameters[0]), int.Parse(parameters[1]));
    }

    public static Fraction operator +(Fraction r1, Fraction r2)
    {
        return new Fraction(r1.num * r2.den + r2.num * r1.den,
            r1.den * r2.den).Simpify();
    }

    public static Fraction operator -(Fraction r1, Fraction r2)
    {
        return new Fraction(r1.num * r2.den - r2.num * r1.den,
            r1.den * r2.den).Simpify();
    }

    public static Fraction operator *(Fraction r1, Fraction r2)
    {
        if (r1.den * r2.den == 0)
        {
            throw new ArgumentException();
        }
        return new Fraction(r1.num * r2.num, r1.den * r2.den).Simpify();
    }

    public static Fraction operator /(Fraction r1, Fraction r2)
    {
        if (r1.den * r2.num == 0)
        {
            throw new ArgumentException();
        }
        return new Fraction(r1.num * r2.den, r1.den * r2.num).Simpify();
    }

    public override string ToString()
    {
        if (this.num == 0)
        {
            return "0";
        }
        if (this.den == 1)
        {
            return $"{num}";
        }
        if (this.den == 0)
        {
            throw new ArgumentException();
        }
        return $"{num}/{den}";
    }

    public Fraction Simpify()
    {
        int divisor = GCD(num, den);
        num /= divisor;
        den /= divisor;

        if (den < 0)
        {
            int newNom = -num,
            newDen = -den;
            return new Fraction(newNom, newDen);
        }
        if (den == 0)
        {
            return new Fraction(0, 1);
        }
        if (num == den)
        {
            return new Fraction(1, 1);
        }
        if (num == -den)
        {
            return new Fraction(-1, 1);
        }

        return this;
    }

    static int GCD(int a, int b)
    {
        int Remainder;

        while (b != 0)
        {
            Remainder = a % b;
            a = b;
            b = Remainder;
        }

        return a;
    }
}

public static class OperatorOverloading
{
    public static void Main()
    {
        try
        {
            Fraction a = Fraction.Parse(Console.ReadLine());
            Fraction b = Fraction.Parse(Console.ReadLine());
            Console.WriteLine(a + b);
            Console.WriteLine(a - b);
            Console.WriteLine(a * b);
            Console.WriteLine(a / b);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("error");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("zero");
        }
    }
}
