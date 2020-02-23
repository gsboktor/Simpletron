/* Simpletron by:
 * Nibraas Khan
 * George Boktor
 * Matthew Drescher
 * Kaleb Askren
 * Michael Ketzner
 * Sam Waymire
 */

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
        static int instructionCounter = 0;
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

            for (int k = 0; k < memory.Length; k++)
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
            Console.Write($"{index:D2}? ");
            int instruction = int.Parse(Console.ReadLine());
            while (instruction != -99999 && index < 100)
            {
                if (instruction > -9999 && instruction < 9999)
                {
                    memory[index] = instruction;
                    index++;
                }
                else {
                    Console.Write("Must be in range -9999 to +9999\n");
                }
                Console.Write($"{index:D2}? ");
                instruction = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("*** Program loading completed ***");
            Console.WriteLine("*** Program execution completed ***");
        }

        public static void Execute()
        {
            try
            {
                while (true)
                {
                    int instructionRegister = memory[instructionCounter];
                    operationCode = instructionRegister / 100;
                    operand = instructionRegister % 100;
                    switch (operationCode)
                    {
                        case 10:
                            Console.WriteLine("Enter an Integer");
                            memory[operand] = int.Parse(Console.ReadLine());
                            break;
                        case 11:
                            Console.WriteLine($"Word from location {operand.ToString()}: {memory[operand].ToString()}");
                            break;
                        case 20:
                            accumulator = memory[operand];
                            break;
                        case 21:
                            memory[operand] = accumulator;
                            break;
                        case 30:
                            accumulator += memory[operand];
                            break;
                        case 31:
                            accumulator -= memory[operand];
                            break;
                        case 32:
                            if (memory[operand] == 0)
                            {
                                Console.WriteLine("*** attempt to divide by 0 ***");
                                Dump();
                                return;
                            }
                            accumulator /= memory[operand];
                            break;
                        case 33:
                            accumulator *= memory[operand];
                            break;
                        case 40:
                            instructionCounter = operand;
                            instructionCounter--;
                            break;
                        case 41:
                            if (accumulator < 0)
                            {
                                instructionCounter = operand;
                                instructionCounter--;
                            }
                            break;
                        case 42:
                            if (accumulator == 0)
                            {
                                instructionCounter = operand;
                                instructionCounter--;
                            }
                            break;
                        case 43:
                            Console.WriteLine("*** Simpletron execution terminated ***\n");
                            return;
                    }
                    instructionCounter++;
                    instructionRegister = memory[index];
                }
            }
            catch {
                Console.WriteLine("*** Simpletron execution abnormally terminated ***\n");
                Dump();
                return;
            }
        }

        public static void Dump()
        {
            string fmt = "0000";
            Console.WriteLine("REGISTERS");
            if (accumulator >= 0)
            {
                Console.WriteLine("accumulator \t\t" + "+" + accumulator.ToString(fmt));
            }
            else
            {
                Console.WriteLine("accumulator \t\t" + accumulator.ToString(fmt));
            }
            Console.WriteLine("instructionCounter \t" + instructionCounter.ToString("00"));
            if (instructionRegister >= 0)
            {
                Console.WriteLine("instructionRegister \t" + "+" + instructionRegister.ToString(fmt));
            }
            else
            {
                Console.WriteLine("instructionRegister \t" + instructionRegister.ToString(fmt));
            }
            Console.WriteLine("operationCode \t\t" + operationCode.ToString("00"));
            Console.WriteLine("operand \t\t" + operand.ToString("00") + "\n\n" + "Memory: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write("          " + i);
            }
            int lineNum = 10;
            Console.WriteLine();
            for (int i = 0; i < memory.Length; i++)
            {
                if (i % 10 == 0 && i != 0)
                {
                    Console.Write(string.Format("\n" + "{0,2:##}", lineNum) + "       ");
                    lineNum += 10;
                }
                else if (i == 0)
                {
                    Console.Write("\n 0" + "       ");
                }
                if (memory[i] >= 0)
                {
                    Console.Write("+" + memory[i].ToString(fmt) + "      ");
                }
                else
                {
                    Console.Write(memory[i].ToString(fmt) + "      ");
                }
            }
            Console.WriteLine();
        }
    }
}
