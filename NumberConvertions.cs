using System;
using System.Xml.Serialization;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NumberConversions
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //Clear console for reprinting main menu text
                Console.Clear();
                //Print main menu text
                Console.Write("Number Conversions Main Menu\n" +
                    "Type the number of the conversion to get started\n" +
                    "1. Binary to denary\n" +
                    "2. Denary to binary\n" +
                    "3. Binary to Hexadecimal\n" +
                    "4. Hexadecimal to Denary/Binary\n" +
                    "5. Denary to Hexadecimal\n" +
                    "6. Add binary numbers\n" +
                    "e. Exit the application\n");

                char choice = '.';
                bool choiceaccept = false;

                //while input is not valid
                while (!choiceaccept)
                {
                    Console.Write("> ");

                    //Try and parse the input into a character
                    choiceaccept = char.TryParse(Console.ReadLine(), out choice);

                    //Exit if e is given
                    if (choice == 'e')
                    {
                        return;
                    }
                    //Check if the character is equal to any of the choices
                    if (!"123456".Contains(choice))
                    {
                        choiceaccept = false;
                    }
                }
                //Ask for a number
                Console.Write("Enter a number > ");
                string input = "";

                //Get user input while the input is empty
                while (string.IsNullOrEmpty(input))
                {
                    input = Console.ReadLine();
                }

                //Make a switch statement to decide on appropriate action
                switch (choice)
                {
                    case '1':
                        //Call upon the binary to denary converter
                        BinToDen(input);
                        break;

                    case '2':
                        //Call upon the denary to binary converter
                        DenToBin(input);
                        break;

                    case '3':
                        //Call upon the binary to hexadecimal converter
                        BinToHex(input);
                        break;

                    case '4':
                        //Call upon the hexadecimal to binary and denary converter
                        HexToDenBin(input);
                        break;

                    case '5':
                        //Call upon the denary to hexadecimal converter
                        DenToHex(input);
                        break;

                    case '6':
                        //Take second input and call the add binary function
                        Console.Write("Input a second number > ");

                        string inp2 = "";
                        while (string.IsNullOrEmpty(input))
                        { inp2 = Console.ReadLine(); }

                        AddBinary(input, inp2);
                        break;

                    default:
                        break;
                }
            }
        }
        static private bool isBin(string bin)
        {
            //Subroutine to check if a string is an 8-bit binary

            //checking if the length of the string is equal to 8
            if (bin.Length!=8) { return false; }

            //checking if all the characters are equal to 0 or 1
            return bin.All("01".Contains);
        }
        static private bool isHex(string hex)
        {
            //Subroutine to check if a string is a hexadecimal number

            //checking if the length of the string is equal to 2
            if (hex.Length != 2) { return false; }

            //checking if all the characters are equal to the symbols used in hex
            return hex.All("0123456789ABCDEF".Contains);
        }

        static public void BinToDen(string input)
        {
            //Binary to Denary
            //Check if the input is a binary number
            if (!isBin(input)) 
            {
                Console.WriteLine("Invalid Input");
                Thread.Sleep(1000);
                return;
            }

            //Converting number if the tests are passed
            //Initialising variables for convertion
            int inputInt = int.Parse(input), output = 0, twopower = 1;

            while (inputInt > 0)
            {
                //get the last digit by using MOD 10, either 0 or 1
                int remainder = inputInt % 10;
                //DIV binary figure by 10 for next cycle
                inputInt /= 10;
                //add remainder multiplied by the power defined out of the loop
                output += remainder * twopower;
                //multiply the power by 2 for next loop
                twopower *= 2;
            }

            //outputting number and returning to main menu
            Console.WriteLine("The number in denary is {0}. Press enter to return to the main menu", output);
            Console.Read();
            return;
        }
        static public bool DenToBin(string input)
        {
            //Take user input and check if it is a denary integer

            if (int.TryParse(input, out int inputInt))
            {
                Console.WriteLine("Invalid Input");
                Thread.Sleep(1000);
                Console.Write("Enter another number > ");
                return DenToBin(Console.ReadLine());
            }

            //Check if input is within the range of accepted data
            if (inputInt < 0 || inputInt > 255)
            {
                Console.WriteLine("Invalid Input: Too big or small of a number to fit into 8 bits");
                Thread.Sleep(1000);
                return DenToBin(Console.ReadLine());
            }

            //Converting number if the tests are passed

            int Input2 = inputInt;
            string output = "";

            for (int i = 0; Input2 > 0; i++)
            {
                //add one or zero on the beginning of the string 
                output = Input2 % 2 + output;

                //dividing input by 2
                Input2 /= 2;
            }

            //outputting number and returning to main menu
            Console.WriteLine("The number in binary is {0}. Press enter to return to the main menu", output);
            Console.Read();
            return true;
        }
        static public void BinToHex(string input)
        {
            //Binary to Hexadecimal
            //Checking if input is a binary number
            if (!isBin(input))
            {
                Console.WriteLine("Invalid Input");
                Thread.Sleep(1000);
                return;
            }

            //Converting number if the tests are passed
            int inputInt = int.Parse(input), tempoutput = 0, twopower = 1;

            //Converting to Denary first as its easier
            while (inputInt > 0)
            {
                int remainder = inputInt % 10;
                inputInt /= 10;
                tempoutput += remainder * twopower;
                twopower *= 2;
            }
            
            //Floor dividing by 16 and also grabbing the remainder
            int onehex = tempoutput % 16, tenhex = tempoutput / 16;
            //Converting both values into hex digits
            string output2 = DenToHexChar(tenhex) + DenToHexChar(onehex);


            //outputting number and returning to main menu
            Console.WriteLine("The number in hexadecimal is {0}. Press enter to return to the main menu", output2);
            Console.Read();
            return;
        }
        static public string DenToHexChar(int input)
        {
            //If the input is 10 or over
            if (input >= 10)
            {
                //Return corresponding digit
                switch (input)
                {
                    case 10:
                        return "A";
                    case 11:
                        return "B";
                    case 12:
                        return "C";
                    case 13:
                        return "D";
                    case 14:
                        return "E";
                    case 15:
                        return "F";
                    default:
                        throw new ArgumentOutOfRangeException("Out of range");
                }
            }
            //If negative throw error
            if (input < 0) { throw new ArgumentOutOfRangeException("Out of range"); }
            //Returning number as string
            return input.ToString();
        }
        static public int HexCharToDen(string input)
        {
            //If the input is a number and of length 1
            if (input.All(char.IsDigit) && input.Length == 1)
            {
                return Convert.ToInt32(input);
            }
            else
            {
                //Check if the input is equal to any of the letters
                switch (input)
                {
                    case "A":
                        return 10;
                    case "B":
                        return 11;
                    case "C":
                        return 12;
                    case "D":
                        return 13;
                    case "E":
                        return 14;
                    case "F":
                        return 15;
                    default:
                        //Throw an exception otherwise
                        throw new ArgumentOutOfRangeException("Improper Digit given");
                }
            }
        }
        static public void HexToDenBin(string input)
        {
            //Hexadecimal to Binary
            //Uppercasing the input string
            input = input.ToUpper();

            //Check if input is a hexidecimal number
            if (!isHex(input))
            {
                Console.WriteLine("Invalid Input");
                Thread.Sleep(1000);
                return;
            }

            //Converting number if the tests are passed
            
            string hexd1 = input.Substring(0, 1), hexd2 = input.Substring(1, 1);
            int outputDen = (HexCharToDen(hexd1) * 16) + HexCharToDen(hexd2);

            
            string outputBin = "";
            int Input2 = outputDen;
            for (int i = 0; Input2 > 0; i++)
            {
                //add one or zero on the beginning of the string 
                outputBin = Input2 % 2 + outputBin;

                //dividing input by 2
                Input2 /= 2;
            }

            //outputting number and returning to main menu
            Console.WriteLine(
                "The number in denary is {0}\n" +
                "The number in binary is {1}\n" +
                "Press enter to return to the main menu", outputDen, outputBin);
            Console.Read();
            return;
        }
        static public void DenToHex(string input)
        {
            //Take user input and check if it is a denary integer
            bool ConvSuccess = int.TryParse(input, out int inputInt);

            if (!ConvSuccess)
            {
                Console.WriteLine("Invalid Input");
                Thread.Sleep(1000);
                return;
            }

            //Check if input is within the range of accepted data
            if (inputInt < 0 || inputInt > 255)
            {
                Console.WriteLine("Invalid Input: Too big or small of a number to fit into 8 bits");
                Thread.Sleep(1000);
                return;
            }

            //Converting number if the tests are passed
            string output = inputInt.ToString("X");

            //outputting number and returning to main menu
            Console.WriteLine("The number in hexadecimal is {0}. Press enter to return to the main menu", output);
            Console.Read();
            return;
        }
        static public void AddBinary(string val1, string val2)
        {
            //Add two binary numbers
            //Check if both inputs are string
            if (!isBin(val1))
            {
                Console.WriteLine("Invalid Input");
                Thread.Sleep(1000);
                return;
            }
            if (!isBin(val2))
            {
                Console.WriteLine("Invalid Input");
                Thread.Sleep(1000);
                return;
            }
            //---- Below code works but i dont know what it does lmao ----

            string result = "";
            int s = 0, i = val1.Length - 1, j = val2.Length - 1;

            //If there is no more digits to pass through
            while (i >= 0 || j >= 0 || s == 1)
            {
                //Add digits and carry over  if needed
                s += ((i >= 0) ? val1[i] - '0' : 0);
                s += ((j >= 0) ? val2[j] - '0' : 0);

                //If digit is 1 or 3, add 1 to string, else 0
                result = (char)(s % 2 + '0') + result;

                //Compute carry
                s /= 2;

                //Move onto next digit
                i--; j--;
            }

            //Output results
            Console.WriteLine("The numbers added together equal {0}.", result);

            //If over 8 bits in length, print overflow, else pass results to binary to denary converter
            if (result.Length > 8)
            {
                Console.WriteLine("The result of your operation would cause an overflow\n" +
                    "As a result denary will not be calculated\n" +
                    "Press enter to return to the main menu");
                Console.Read();
            }
            else { BinToDen(result); }
        }
    }
}