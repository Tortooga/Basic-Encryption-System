using System;

namespace encryptionSys // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static char[] alpha = "qwertyuiopasdfghjklzxcvbnm1234567890-='@#~/?".ToCharArray();
        static Random random = new Random();

        static void Main(string[] args)
        {
            while (true){
                Console.WriteLine(encrypt(Console.ReadLine(),keyGen()));
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

            char[] encrypted = new char[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
               encrypted[i] = key[Array.IndexOf(alpha,original[i])]; 
            }

            return encrypted;
        }

        static char[] decrypt(string encrypted, char[] key){
            encrypted.ToCharArray(); 

            char[] decrypted = new char[encrypted.Length];
            for (int i = 0; i < encrypted.Length; i++)
            {
                decrypted[i] = alpha[Array.IndexOf(key,encrypted[i])];
            }

            return decrypted;
        }
    }
}





//C:\Users\Umar\Desktop\Projects\Basic-Encryption-System