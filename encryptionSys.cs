using System;
using System.Runtime.CompilerServices;

namespace encryptionSys // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static char[] alpha = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890- ,.='@#~/?".ToCharArray();
        static Random random = new Random();

        static void Main(string[] args)
        {
            string input = "";
            char[] key = keyGen();
            char[] encrypted = encrypt("hello this message is a test, alpha is a list containing all the possible charecters recognised by this encryption system.", key);
            Console.WriteLine(encrypted);
            Console.WriteLine(decrypt(encrypted, key));
           /* while (input != "/"){
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
            }*/
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
            char[] preCycleKey = key;

            char[] encrypted = new char[original.Length];
            for (int i = 0; i < original.Length; i++)
            {
                encrypted[i] = key[Array.IndexOf(alpha,original[i])];
                key = cycle(key);
            }

            return salt(encrypted, preCycleKey);
        }

        static char[] cycle(char[] inSeq){
            char[] fiSeq = new char[inSeq.Length];
            int jumpConstant = Array.IndexOf(alpha,inSeq[0]);

            fiSeq[jumpConstant] = inSeq[0];
            if (jumpConstant == 0){
                jumpConstant = 1;
            }

            for (int i = 1; i < inSeq.Length; i++)
            {
                fiSeq[cycleReset(i + jumpConstant)] = inSeq[i];
            }
            return fiSeq;
        }

        static char[] decrypt(char[] encrypted, char[] key){
            char[] decrypted = new char[encrypted.Length];
            encrypted = unsalt(encrypted, key);
            Console.WriteLine(encrypted);
            for (int i = 0; i < encrypted.Length; i++)
            {
                decrypted[i] = alpha[Array.IndexOf(key,encrypted[i])];
                key = cycle(key);
            }

            return decrypted;
        }

        static char[] salt(char[] unsalted, char[] key){
            int?[] scatterMapParams = {unsalted.Length, null};
            int[] scatterMap = scatterMapGen(scatterMapParams, key);
            char[] salted = new char[scatterMap.Length];

            int realValCount = 0;
            for (int i = 0; i < scatterMap.Length; i++)
            {
                if (scatterMap[i] == 1){
                    salted[i] = unsalted[realValCount];
                    realValCount++;
                }
                else{
                    salted[i] = alpha[choose(alpha)];
                }
            }

            return salted;
        }

        static char[] unsalt(char[] salted, char[] key){
            int?[] scatterMapParams = {null, salted.Length};
            int[] scatterMap = scatterMapGen(scatterMapParams, key);
            char[] tempUnsalted = new char[scatterMap.Length];

            int realValCount = 0;
            for (int i = 0; i < salted.Length; i++)
            {
                if (scatterMap[i] == 1){
                    tempUnsalted[realValCount] = salted[i];
                    realValCount++;
                }
            }

            char[] finalUnsalted = new char[realValCount];
            for (int i = 0; i < realValCount; i++)
            {
                finalUnsalted[i] = tempUnsalted[i];
            }
            return finalUnsalted;
        }


        static int cycleReset(int num){
            if (num >= alpha.Length){
                return num - alpha.Length;
            }
            else{
                return num;
            }
        }
        
        static int[] toBin(int num){
            short bittage;

            if (num == 0){
                bittage = 1;
            }
            else{
                bittage = (short)(Math.Floor(Math.Log2(num)) + 1);
            }
            
            int[] binval = new int[bittage];

            for (int i = 0; i < bittage; i++)
            {
            if (num >= Math.Pow(2,bittage-1-i)){
                num = num - (int)Math.Pow(2,bittage-1-i);
                binval[i] = 1;
            }

            else{
                binval[i] = 0;
            }
            }

            return binval;
        }

        static int[] scatterMapGen(int?[] paramArray, char[] key){
            int realValCount = 0;
            int seedCharIndex = 0;
            int bitCount  = 0;
            int subCharBit = 0;
            int[] ScatterMapTemp;
            bool scatterMode;

            if (paramArray[0] is null){
                ScatterMapTemp = new int[(int)paramArray[1]];
                scatterMode = false;
            }

            else{
                ScatterMapTemp = new int[(int)paramArray[0]*5];
                for (int i = 0; i < ScatterMapTemp.Length; i++)
                {
                    ScatterMapTemp[i] = 2;
                }
                scatterMode = true;
            }
            
            bool conditionalDummyVar = true;
            while (conditionalDummyVar){
                if (subCharBit >= toBin(Array.IndexOf(alpha,key[seedCharIndex])).Length){
                    seedCharIndex++;
                    subCharBit = 0;
                }

                ScatterMapTemp[bitCount] = toBin(Array.IndexOf(alpha,key[seedCharIndex]))[subCharBit];

                if (ScatterMapTemp[bitCount] == 1){
                    realValCount++;
                }

                bitCount++;
                subCharBit++;

                if (scatterMode){
                    conditionalDummyVar = realValCount < paramArray[0];
                }
                else{
                    conditionalDummyVar = bitCount < paramArray[1];
                }
            }

            int[] ScatterMap = new int[bitCount];
            for (int i = 0; i < bitCount; i++)
            {
                ScatterMap[i] = ScatterMapTemp[i];
            }

            return ScatterMap;
        }

        
    }
}