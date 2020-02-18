using System;
namespace Simpletron
{
    public class Simulator
    {
        const int READ = 10;
        const int WRITE = 11;
        const int LOAD = 20;
        const int STORE = 21;
        const int ADD = 30;
        const int SUBTRACT = 31;
        const int DIVIDE = 32;
        const int MULTIPLY = 33;
        const int BRANCH = 40;
        const int BRANCH_NEG = 41;
        const int BRANCH_ZERO = 42;
        const int HALT = 43;

        static int accumulator;
        static int instructionCounter;
        static int operand;
        static int operationCode;
        static int instructionRegister;

        static int[] memory;
        static int index = 0;

        public static void Main(string[] args)
        {
            InitializeRegisters();

            PrintInstructions();
            LoadInstructions();

            Execute();
            Dump();
        }

        public static void InitializeRegisters()
        {
            memory = new int[100];
            accumulator = 0;
            instructionCounter = 0;
            instructionRegister = 0;
            operand = 0;
            operationCode = 0;

            for(int k = 0; k < memory.Length; k++)
            {
                memory[k] = 0;
            }
        }

        public static void PrintInstructions()
        {
            Console.WriteLine("*** Welcome to Simpletron! ***");
            Console.WriteLine("*** Please enter your program one instruction ***");
            Console.WriteLine("*** (or data word) at a time into the input ***");
            Console.WriteLine("*** text field. I will display the location ***");
            Console.WriteLine("*** number and a question mar (?). You then ***");
            Console.WriteLine("*** type the word for that location. Enter ***");
            Console.WriteLine("*** -99999 to stop entering your program ***");
        }

        public static void LoadInstructions()
        {
            Console.Write($"{index:D2}?");
            int instruction = int.Parse(Console.ReadLine());
            while( instruction != -99999 && index < 100)
            {
                Console.WriteLine("Instruction =>" + instruction);
                Console.ReadLine(instruction);
            }
        }

        public static void Execute()
        {

        }

        public static void Dump()
        {

        }
    }
}
