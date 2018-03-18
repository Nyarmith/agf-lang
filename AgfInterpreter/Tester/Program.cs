using System;
using System.Collections.Generic;
using AgfLang;

namespace Tester
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("REPL Commands: eval \"stmt\", exec \"stmt\", printmem");
            string input = "";
            Dictionary<string, Dictionary<string, int>> memory = new Dictionary<string, Dictionary<string, int>>();
            memory["testguy"] = new Dictionary<string, int>();
            memory["testguy"]["buddy"] = 32;

            AgfInterpreter interp = new AgfInterpreter(ref memory);

            while (true)
            {
                string ret = "";
                string command = "";

                Console.Write(">");
                input = Console.ReadLine();

                int i = input.IndexOf(' ');
                if (i != -1)
                {
                    command = input.Substring(0, i).ToUpper();
                }
                else
                {
                    command = input.ToUpper();
                }
                switch (command.ToUpper())
                {
                    case "EVAL":
                        ret = interp.eval(input.Substring(input.IndexOf(' ') + 1));
                        break;
                    case "EXEC":
                        interp.exec(input.Substring(input.IndexOf(' ') + 1));
                        break;
                    case "PRINTMEM":
                        ret = interp.ShowMemory();
                        break;
                }
                Console.WriteLine(ret);
            }
        }
    }
}
