using System;

namespace encryptionSys // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static char[] alpha = "qwertyuiopasdfghjklzxcvbnm".ToCharArray();
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine(encrypt("hello",keyGen()));
            Console.ReadLine();
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

            char[] encrypted = new char[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
               encrypted[i] = key[Array.IndexOf(alpha,original[i])]; 
            }

            return encrypted;
        }
    }
}