using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast_Algorithms
{
    class StringAndArrayAlogrithms
    {
        const int NUMBER_ASCII_CHARS = 256;
        const int NUMBER_ASCII_NUMERALS = 10;
        const int FIRST_ASCII_NUMBER = 48;

        static void Main(string[] args)
        {
            bool result = IsAnagram("my first test", "first my test");
            result = IsAnagram("bbaaccddeeffgg", "bbaaccddeeffhh");

            StringBuilder outputStr = new StringBuilder(); 
            result = ReturnUniqueSymbols("bbaaccddeeffgg",  ref outputStr);
            int[] inputArray = new int[] { 2, 2,7,6,5,4,8,4,4 };

            outputStr.Clear();
            result = ReturnUniqueNumericalSymbols(inputArray, ref outputStr);
            int length = 0;
            result = ReturnUniqueNumericalSymbols(inputArray, ref length);
            string in_str = "bbagaccdgdeeffgg";
            char maxChar = '0';
            int num = ReturnMaxCharOccurance(in_str, ref maxChar);
        }

        // O(nlogn) - O[1] memory space - sort both arrays and compare if strings are equal
        // TODO

        // this algorithm is O[n} actually 2 loops of O[n} each - note for memory space we need 2 *4 *256 bytes for achii chars 
        // when n is large we can view the space as beingO[1] as its fixed at 1KB independent of the length of the input strings
        // the lookup via arrays is faster than a hash though each are considered as O[1]
        // this algorithm can also be easily developed into a parallel algorithm

        static bool IsAnagram(string str1, string str2)
        {
            // assuming we only have ascii characters and we are limiting the number of any one character to 2^32-1
            int[] lookUp1 = new int[NUMBER_ASCII_CHARS];
            int[] lookUp2 = new int[NUMBER_ASCII_CHARS];

            int len1 = str1.Length;
            int len2 = str2.Length;

            // lengths have to be equal
            if (len1 != len2)
                return false;

            // tabulate our lookup tables for each string
            for (int i = 0; i < len1; i++)
            {
                lookUp1[(byte)str1[i]]++;
                lookUp2[(byte)str2[i]]++;
            }

            // if the strings are anagrams than the char counts are equal
            for (int i = 0; i < NUMBER_ASCII_CHARS; i++)
            {
                if (lookUp1[i] != lookUp2[i])
                    return false;
            }

            // it passes the test
            return true;
        }

        // in c# JAVA we can use a StringBuilder data structure, for C++ we use an unordered_set
        // O[n] function rountine
        static bool ReturnUniqueSymbols(string in_str, ref StringBuilder outputStr)
        {
            // assuming we only have ascii characters and we are limiting the number of any one character to 2^32-1
            // if we assume only ascii numeric cahracters, we can limit the size of our lookup tables to 4* 10 40 Bytes!
            int[] lookUp1 = new int[NUMBER_ASCII_CHARS];
            int[] lookUp2 = new int[NUMBER_ASCII_CHARS];

            // we want to minimize the amount of times we resize
            // NUMBER_ASCII_CHARS is an upperbounds for the capacity
            outputStr = new StringBuilder(NUMBER_ASCII_CHARS);

            // we peroform one pass of the string O[n]
            for (int i = 0; i < in_str.Length; i++)
            {
                if (lookUp1[(int)in_str[i]] == 0) // first occurance of the symbol
                {
                    // add the char to the output
                    outputStr.Append(in_str[i]);
                    // don't use this character again
                    ++(lookUp1[(int)in_str[i]]);
                }
            }

            if (outputStr.Length == 0)
                return false;

            // this copy is O[n] but we do it once at the end
            return true;
        }

        // in c# JAVA we can use a StringBuilder data structure, for C++ we use an unordered_set
        static bool ReturnUniqueNumericalSymbols(int[] in_str, ref StringBuilder outputStr)
        {
            // assuming we only have ascii characters and we are limiting the number of any one character to 2^32-1
            int[] lookUp1 = new int[NUMBER_ASCII_NUMERALS];

            // we want to minimize the amount of times we resize
            // NUMBER_ASCII_NUMERALS is an upperbounds for the capacity
            outputStr = new StringBuilder(in_str.Length);

            // we peroform one pass of the string O[n]
            for (int i = 0; i < in_str.Length; i++)
            {
                if (lookUp1[(int)in_str[i]] == 0) // first occurance of the symbol
                {
                    // add the char to the output
                    outputStr.Append(in_str[i]);
                    // don't use this character again
                    ++(lookUp1[(int)in_str[i]]);
                }
            }

            if (outputStr.Length == 0)
                return false;

            // this copy is O[n] but we do it once at the end
            return true;
        }

        // this algorithm does not use an output StringBuilder
        // if we can overwrite the orignal input array
        static bool ReturnUniqueNumericalSymbols(int[] in_str, ref int outputLength)
        {
            // assuming we only have ascii characters and we are limiting the number of any one character to 2^32-1
            int[] lookUp1 = new int[NUMBER_ASCII_NUMERALS];
            outputLength = 0;

            // we peroform one pass of the string O[n]
            for (int i = 0; i < in_str.Length; i++)
            {
                if (lookUp1[in_str[i]] == 0) // first occurance of the symbol
                {
                    // add the char to the output
                    in_str[outputLength++] = in_str[i];
                    // don't use this character again
                    ++(lookUp1[(int)in_str[i]]);
                }
            }

            if (outputLength == 0)
                return false;

            // this copy is O[n] but we do it once at the end
            return true;
        }

        // returns the character which occurs the maximum times
        // returns the number of times it appears
        // O[n] - 1 scan to build lookup table, 1 scan to find max occurance
        static int ReturnMaxCharOccurance(string in_str, ref char maxChar)
        {
            int length = in_str.Length;
            if (length == 0)
                return 0;

            int[] lookUp1 = new int[NUMBER_ASCII_CHARS];

            // build lookup table
            for (int i = 0; i < length; i++)
                lookUp1[in_str[i]]++;

            // find character with max occurance
            int max = 1;

            for (int i = 0; i < NUMBER_ASCII_CHARS; i++)
            {
                if (lookUp1[i] > max)
                {
                    max = lookUp1[i];
                    maxChar = (char)i;
                }
            }
            return max;
        }
    }
 }
