using System;

namespace encryptionSys // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static char[] alpha = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890- ,.='@#~/?".ToCharArray();
        static Random random = new Random();

        static void Main(string[] args)
        {
            while (true){
                string input = "";
                char[] key = new char[alpha.Length];
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
            
                else{
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
            /*else{
                fiSeq[inSeq.Length - 1] = inSeq[0];

                for (int i = 1; i < inSeq.Length; i++)
                {
                    fiSeq[i-1] = inSeq[i];
                }
            } 
            */
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