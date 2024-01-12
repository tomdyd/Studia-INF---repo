namespace pracaDomowa
{
    class Program
    {
        static void Main(string[] args)
        {
            Print();
            var userColor = GetColor();
            
            Console.WriteLine("Twój kolor to: {0}", userColor);
        }

        static ConsoleColor GetColor()
        {
            while (true)
            {
                var str = Console.ReadLine();
                object obj;
                bool isColor = Enum.TryParse(typeof(ConsoleColor), str, out obj);
                if (isColor)
                    return (ConsoleColor)obj;
                Console.WriteLine("To nie jest kolor, wpisz jeszcze raz");
            }
        }

        static void Print()
        {
            var array = Enum.GetValues(typeof(ConsoleColor));
            foreach (var item in array)
                Console.WriteLine("{0} - {1}", (int)item, item);
        }
    }
}
