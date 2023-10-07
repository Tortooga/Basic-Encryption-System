using System;
using System.Runtime.CompilerServices;

namespace encryptionSys // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static char[] alpha = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890- ;,.='@#~/?".ToCharArray();
        static Random random = new Random();

        static void Main(string[] args)
        {
            string input = "";
            char[] key = new char[alpha.Length];
            while (input != "/"){
                Console.WriteLine("For encryption input 'e'. And for decryption input 'd'");
                input = Console.ReadLine();
                if (input == "d"){
                    Console.WriteLine("input the key");
                    key = Console.ReadLine().ToCharArray();
                    while (input != "/"){
                        Console.WriteLine("input a string to be decrypted");
                        input = Console.ReadLine();
                        Console.WriteLine(decrypt(input.ToCharArray(), key));
                    }
                }
            
                else if (input == "e"){
                    Console.WriteLine("Would you like for a session key to be generated: 'y', or do you have your own key: 'n'");
                    input = Console.ReadLine();
                    if (input == "y"){
                        key = keyGen();
                        Console.WriteLine("Your key is:");
                        Console.Write(key);
                    }
                    else{
                        Console.WriteLine("Input your key");
                        key = Console.ReadLine().ToCharArray();
                    }
                    while (input != "/"){
                        Console.ReadLine();
                        Console.WriteLine("Input a string to be encrypted");
                        input = Console.ReadLine();
                        Console.WriteLine(encrypt(input,key));
                    }
                }
            }
        }


        static char[] keyGen(){
            char[] tempAlpha = new char[alpha.Length];
            char[] key = new char[alpha.Length];
            int choice;

            for (int i = 0; i < alpha.Length; i++)
            {
                tempAlpha[i] = alpha[i];
            }

            for (int i = 0; i < tempAlpha.Length; i++)
            {
                choice = choose(tempAlpha);
                key[i] = tempAlpha[choice]; 
                tempAlpha[choice] = '_';
            }
        
        return key;
        }

        static int choose(char[] construct){
            int choice = random.Next(construct.Length);

            while (construct[choice] == '_'){
                choice = random.Next(construct.Length);
            }
            
            return choice;
        }

        static char[] encrypt(string original, char[] key){
            original.ToCharArray();

            int jumpConstant = Array.IndexOf(alpha,key[0]);
            if (jumpConstant == 0){
                jumpConstant = 1;
            }

            int saltJump = Array.IndexOf(alpha,key[0]) + 1; //added 1 such that it is never 0
            if (saltJump < 2){
                saltJump = 2;
            }
            else if (saltJump > 4){
                saltJump = 2;
            }
            char[] encrypted = new char[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                encrypted[i] = key[Array.IndexOf(alpha,original[i])];
                key = cycle(key, jumpConstant);
            }

            return salt(encrypted, saltJump);
        }

        static char[] cycle(char[] inSeq, int jumpConstant){
            char[] fiSeq = new char[inSeq.Length];
            fiSeq[jumpConstant] = inSeq[0];

            for (int i = 1; i < inSeq.Length; i++)
            {
                fiSeq[cycleReset(i + jumpConstant)] = inSeq[i];
            }
            return fiSeq;
        }

        static char[] decrypt(char[] encrypted, char[] key){
            int jumpConstant = Array.IndexOf(alpha,key[0]);
            if (jumpConstant == 0){
                jumpConstant = 1;
            }

            int saltJump = Array.IndexOf(alpha,key[0]) + 1; //added 1 such that it is never 0
            if (saltJump < 2){
                saltJump = 2;
            }
            else if (saltJump > 4){
                saltJump = 2;
            }

            encrypted = unsalt(encrypted, saltJump);

            char[] decrypted = new char[encrypted.Length];
            for (int i = 0; i < encrypted.Length; i++)
            {
                decrypted[i] = alpha[Array.IndexOf(key,encrypted[i])];
                key = cycle(key,jumpConstant);
            }

            return decrypted;
        }

        static char[] salt(char[] unsalted,int saltJump){
            char[] salted = new char[unsalted.Length + floorDiv(unsalted.Length,saltJump - 1) + 1];

            int k = 0;
            for (int i = 0; i < salted.Length; i++)
            {
                if (i % saltJump == 0){
                    salted[i] = alpha[choose(alpha)];
                }
                else{
                    salted[i] = unsalted[k];
                    k++;
                }  
            }

            return salted;
        }

        static char[] unsalt(char[] salted, int saltJump){
            char[] unsalted = new char[salted.Length - floorDiv(salted.Length,saltJump) - 1];
            int k = 0;
            for (int i = 0; i < salted.Length; i++)
            {
                if (i % saltJump == 0){
                    continue;
                }
                else{
                    unsalted[k] = salted[i];
                    k++;
                }
            }

            return unsalted;
        }

        static int cycleReset(int num){
            if (num >= alpha.Length){
                return num - alpha.Length;
            }
            else{
                return num;
            }
        }

        static int floorDiv(int dividend, int divisor){
            return (dividend - (dividend % divisor)) / divisor;
        }

    }
}




