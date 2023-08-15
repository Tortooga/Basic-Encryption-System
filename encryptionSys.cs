using System;

namespace encryptionSys // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static char[] alpha = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890- ='@#~/?".ToCharArray();
        static Random random = new Random();

        static void Main(string[] args)
        {
           /* while (true){
                Console.WriteLine("enter a string of text such that it would be encrypted");
                input = Console.ReadLine();
                if (!(input is null)){
                    Console.WriteLine(encrypt(input,keyGen()));
                }
                else {
                    Console.WriteLine("invalid input");
                }
                 
           }*/

            char[] key = keyGen();
            char[] message = encrypt("hello",key);
            Console.WriteLine(message);
            Console.WriteLine(decrypt(message,key));
        
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
                key = cycle(key,true);
            }

            return encrypted;
        }

        static char[] cycle(char[] inSeq, bool forward){
            char[] fiSeq = new char[inSeq.Length];

            if (forward){
                fiSeq[0] = inSeq[inSeq.Length - 1];

                for (int i = 0; i < inSeq.Length - 1; i++)
                {
                    fiSeq[i+1] = inSeq[i];
                }
            }
            else{
                fiSeq[inSeq.Length - 1] = inSeq[0];

                for (int i = 1; i < inSeq.Length; i++)
                {
                    fiSeq[i-1] = inSeq[i];
                }
            } 

            return fiSeq;
        }

        static char[] decrypt(char[] encrypted, char[] key){
            
            char[] decrypted = new char[encrypted.Length];
            for (int i = 0; i < encrypted.Length; i++)
            {
                decrypted[i] = alpha[Array.IndexOf(key,encrypted[i])];
                key = cycle(key,true);
            }

            return decrypted;
        }
    }
}





//C:\Users\Umar\Desktop\Projects\Basic-Encryption-System