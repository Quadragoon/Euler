using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using System.Numerics;

namespace Euler
{
    class Program
    {
        static void Main(string[] args)
        {
            Decimal answer = Euler50();
            WriteAnswer(answer);
        }

        #region Useful functions
        public static void WriteAnswer(Decimal answer)
        {
            Console.WriteLine("!!!");

            Thread thread = new Thread(() => Clipboard.SetText(answer.ToString()));
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start();
            thread.Join();

            Console.WriteLine(answer);
            Console.WriteLine("!!!");
            Console.ReadKey();
        }

        public static void GetSievePrimes(out List<int> primes, int exclusiveUpperCap = 1000000)
        {
            bool[] primeArray = new bool[exclusiveUpperCap];

            for (int i = 2; i < Math.Sqrt(exclusiveUpperCap); i++)
            {
                if (!primeArray[i])
                {
                    int iSquared = (int)Math.Pow(i, 2);
                    for (int multiplier = 0; iSquared + i * multiplier < exclusiveUpperCap; multiplier++)
                    {
                        primeArray[iSquared + i * multiplier] = true;
                    }
                }
            }

            primes = new List<int> { 2 };
            for (int i = 3; i < exclusiveUpperCap; i+=2)
            {
                if (!primeArray[i])
                    primes.Add(i);
            }
        }

        public static void GetHighestSievePrimes(out List<int> primes, int exclusiveUpperCap = 1000000, int numberOfPrimesToGet = 1000)
        {
            bool[] primeArray = new bool[exclusiveUpperCap];

            for (int i = 2; i < Math.Sqrt(exclusiveUpperCap); i++)
            {
                if (!primeArray[i])
                {
                    int iSquared = (int)Math.Pow(i, 2);
                    for (int multiplier = 0; iSquared + i * multiplier < exclusiveUpperCap; multiplier++)
                    {
                        primeArray[iSquared + i * multiplier] = true;
                    }
                }
            }

            int primesGotten = 0;
            primes = new List<int>();
            for (int i = exclusiveUpperCap - 1; primesGotten < numberOfPrimesToGet; i--)
            {
                if (!primeArray[i])
                {
                    primesGotten++;
                    primes.Add(i);
                }
            }
        }
        #endregion

        #region Euler 50-59
        public static long Euler50()
        {


            return 0;
        }
        #endregion

        #region Euler 40-49
        /*
        public static long Euler49()
        {
            GetSievePrimes(out List<int> primes, 10000);

            for (int increment = 1; increment < 4999; increment++)
            {
                for (int start = 1001; start + increment * 2 <= 9999; start += 2)
                {
                    if (!primes.Contains(start))
                        continue;

                    if (!primes.Contains(start + increment))
                        continue;
                    else
                    {
                        bool firstStepIsPermutation = true;
                        string startString = start.ToString();
                        string firstStepString = (start + increment).ToString();
                        foreach (char digit in firstStepString)
                        {
                            if (!startString.Contains(digit))
                            {
                                firstStepIsPermutation = false;
                                break;
                            }
                            else
                                startString = startString.Remove(startString.IndexOf(digit), 1);
                        }
                        if (!firstStepIsPermutation)
                            continue;
                        else
                        {
                            if (!primes.Contains(start + increment + increment))
                                continue;
                            else
                            {
                                bool secondStepIsPermutation = true;
                                startString = start.ToString();
                                string secondStepString = (start + increment + increment).ToString();
                                foreach (char digit in secondStepString)
                                {
                                    if (!startString.Contains(digit))
                                    {
                                        secondStepIsPermutation = false;
                                        break;
                                    }
                                    else
                                        startString = startString.Remove(startString.IndexOf(digit), 1);
                                }
                                startString = start.ToString();
                                if (!secondStepIsPermutation)
                                    continue;
                                else if (start == 1487 && increment == 3330)
                                    continue;
                                else
                                    return long.Parse(startString + firstStepString + secondStepString);
                            }
                        }
                    }
                }
            }

            return 0;
        }

        public static long Euler48()
        {
            BigInteger sum = 0;

            for (int i = 1; i <= 1000; i++)
                sum += BigInteger.Pow(i, i);

            string last10Digits = sum.ToString().Substring(sum.ToString().Length - 10);

            return long.Parse(last10Digits);
        }

        public static long Euler47()
        {
            GetSievePrimes(out List<int> primes);

            int consecutive = 0;
            
            for (int i = 1; i < int.MaxValue; i++)
            {
                int number = i;
                int primeFactors = 0;
                foreach (int prime in primes)
                {
                    if (prime > number)
                        break;
                    if (i % prime == 0)
                    {
                        primeFactors++;
                        do
                        {
                            number /= prime;
                        } while (number % prime == 0);
                    }
                    if (number == 1)
                        break;
                }
                if (primeFactors == 4)
                    consecutive++;
                else
                    consecutive = 0;
                if (consecutive == 4)
                    return i - 3;
            }

            return 0;
        }

        public static long Euler46()
        {
            GetSievePrimes(out List<int> primes);
            List<int> doubleSquares = new List<int>();

            for (int i = 1; i < 100000; i++)
                doubleSquares.Add((int)Math.Pow(i, 2) * 2);

            for (int i = 9; i < 100000; i+=2)
            {
                bool isPrime = false;
                for (int primeIndex = 1; primes[primeIndex] <= i; primeIndex++ )
                    if (primes[primeIndex] == i)
                        isPrime = true;
                if (isPrime)
                    continue;
                bool summable = false;
                foreach (int prime in primes)
                {
                    if (prime >= i)
                        break;
                    foreach (int doubleSquare in doubleSquares)
                    {
                        if (prime + doubleSquare > i)
                            break;
                        if (prime + doubleSquare == i)
                        {
                            summable = true;
                            break;
                        }
                    }
                }
                if (!summable)
                    return i;
            }

            return 0;
        }

        public static long Euler45()
        {
            const int POLYGON_ARRAY_SIZE = int.MaxValue / 1000;

            long[] triangles = new long[POLYGON_ARRAY_SIZE];
            long[] pentagons = new long[POLYGON_ARRAY_SIZE];
            long[] hexagons = new long[POLYGON_ARRAY_SIZE];

            for (int i = 1; i < POLYGON_ARRAY_SIZE; i++)
            {
                triangles[i] = (long)(i * (i + 1)) / 2;
                pentagons[i] = (long)(i * ((3 * i) - 1)) / 2;
                hexagons[i] = (long)(i * ((i * 2) - 1));
            }

            int triangle = 285; // this is where the first tri-pent-hexagonal number is found
            int pentagon = 165; // it's like 40000 or so
            int hexagon = 143;  // so let's start looking beyond that one

            for (hexagon = 144; hexagon < POLYGON_ARRAY_SIZE; hexagon++)
            {
                for (pentagon = hexagon; pentagon < POLYGON_ARRAY_SIZE; pentagon++)
                { 
                    if (pentagons[pentagon] > hexagons[hexagon])
                        break;
                    if (pentagons[pentagon] < hexagons[hexagon])
                        continue;
                    for (triangle = hexagon; triangle < POLYGON_ARRAY_SIZE; triangle++)
                    {
                        if (triangles[triangle] > pentagons[pentagon])
                            break;
                        if (triangles[triangle] == pentagons[pentagon] && triangles[triangle] == hexagons[hexagon])
                        {
                            return triangles[triangle];
                        }
                    }
                }
            }
            return 0;
        }

        public static long Euler44()
        {
            const int PENTAGON_ARRAY_SIZE = 1000000;

            int[] pentagons = new int[PENTAGON_ARRAY_SIZE];
            int firstPentagonWithLowestDifference;
            int secondPentagonWithLowestDifference;
            int lowestDifference = int.MaxValue;

            for (int i = 1; i < PENTAGON_ARRAY_SIZE; i++)
                pentagons[i - 1] = ((int)i * (3 * i - 1) / 2);

            int firstPentagon = 0;
            int secondPentagon = 0;
            int pentagonSum = 0;
            int pentagonDifference = 0;

            for (int firstPentagonIndex = 1; firstPentagonIndex < PENTAGON_ARRAY_SIZE; firstPentagonIndex++)
            {
                firstPentagon = pentagons[firstPentagonIndex];
                for (int secondPentagonIndex = 0; secondPentagonIndex < firstPentagonIndex; secondPentagonIndex++)
                {
                    secondPentagon = pentagons[secondPentagonIndex];

                    pentagonSum = firstPentagon + secondPentagon;
                    bool willSkipToNextSecondPentagonIndex = true;
                    for (int pentagonToCheck = firstPentagonIndex + 1; pentagons[pentagonToCheck] <= pentagonSum; pentagonToCheck++)
                    {
                        if (pentagons[pentagonToCheck] == pentagonSum)
                        {
                            willSkipToNextSecondPentagonIndex = false;
                            break;
                        }
                    }

                    if (willSkipToNextSecondPentagonIndex)
                        continue;

                    pentagonDifference = firstPentagon - secondPentagon;
                    willSkipToNextSecondPentagonIndex = true;
                    for (int pentagonToCheck = firstPentagonIndex; pentagons[pentagonToCheck] >= pentagonDifference; pentagonToCheck--)
                    {
                        if (pentagons[pentagonToCheck] == pentagonDifference)
                        {
                            willSkipToNextSecondPentagonIndex = false;
                            break;
                        }
                    }

                    if (willSkipToNextSecondPentagonIndex)
                        continue;

                    if (pentagonDifference < lowestDifference)
                    {
                        firstPentagonWithLowestDifference = firstPentagon;
                        secondPentagonWithLowestDifference = secondPentagon;
                        lowestDifference = firstPentagon - secondPentagon;
                        Console.WriteLine(firstPentagon + "-" + secondPentagon + "=" + lowestDifference);
                    }
                }
            }

            return lowestDifference;
        }

        public static long Euler43()
        {
            long answer = 0;

            for (long i = 1000000000; i < 9999999999; i++)
            {
                bool isPandigital = true;
                string iString = i.ToString();
                for (char digit = '0'; digit <= '9'; digit++)
                    if (!iString.Contains(digit))
                    {
                        isPandigital = false;
                        break;
                    }
                if (!isPandigital)
                {
                    int seventhSubstring2 = int.Parse(iString.Substring(7, 3));
                    if (seventhSubstring2 % 17 == 0 && seventhSubstring2 < 980)
                        i += 16;
                    continue;
                }

                int firstSubstring = int.Parse(iString.Substring(1, 3));
                int secondSubstring = int.Parse(iString.Substring(2, 3));
                int thirdSubstring = int.Parse(iString.Substring(3, 3));
                int fourthSubstring = int.Parse(iString.Substring(4, 3));
                int fifthSubstring = int.Parse(iString.Substring(5, 3));
                int sixthSubstring = int.Parse(iString.Substring(6, 3));
                int seventhSubstring = int.Parse(iString.Substring(7, 3));

                if (firstSubstring % 2 == 0 && secondSubstring % 3 == 0 && thirdSubstring % 5 == 0 &&
                    fourthSubstring % 7 == 0 && fifthSubstring % 11 == 0 && sixthSubstring % 13 == 0 && seventhSubstring % 17 == 0)
                {
                    Console.WriteLine(i);
                    answer += i;
                }
                if (seventhSubstring % 17 == 0 && seventhSubstring < 980)
                    i += 16;
            }
            return answer;
        }

        public static long Euler42()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB"); // this sumbitch right here is VERY important!!!

            List<string> words = new List<string>();
            try
            {
                words.AddRange(File.ReadAllText("words.txt").Split(','));
            }
            catch (Exception e)
            { throw e; }

            Dictionary<int, int> scoresAndHowManyTimesTheyOccur = new Dictionary<int, int>();

            for (int i = 0; i < words.Count; i++)
            {
                int score = 0;
                words[i] = words[i].Trim('"');
                foreach (char character in words[i])
                {
                    score += character - 'A' + 1;
                }
                if (scoresAndHowManyTimesTheyOccur.ContainsKey(score))
                    scoresAndHowManyTimesTheyOccur[score]++;
                else
                    scoresAndHowManyTimesTheyOccur.Add(score, 1);
            }

            int count = 0;

            for (int i = 1; i < 10000; i++)
            {
                int triangle = (int)(i / 2f * (i + 1));
                if (scoresAndHowManyTimesTheyOccur.ContainsKey(triangle))
                    count += scoresAndHowManyTimesTheyOccur[triangle];
            }

            return count;
        }

        public static long Euler41()
        {
            List<int> primes = new List<int>();
            GetSievePrimes(out primes, 987654321);
            long answer = 0;

            foreach (int prime in primes)
            {
                bool isPandigital = true;
                string primeString = prime.ToString();
                for (char i = '1'; i - '1' < primeString.Length; i++)
                {
                    if (!primeString.Contains(i))
                    {
                        isPandigital = false;
                        break;
                    }
                }
                if (isPandigital)
                {
                    answer = prime;
                    Console.WriteLine(answer);
                }
            }

            return answer;
        }

        public static long Euler40()
        {
            List<int> digits = new List<int> { 0 };

            for (int i = 1; digits.Count < 1000050; i++)
            {
                string iString = i.ToString();
                foreach (char iDigitChar in iString)
                    digits.Add(int.Parse(iDigitChar.ToString()));
            }

            long answer = digits[1];
            for (int i = 1; i < 7; i++)
            {
                int digit = digits[1 * (int)Math.Pow(10, i)];
                answer *= digit;
            }

            return answer;
        }*/
        #endregion
        
        #region Euler 30-39
        /*
        public static long Euler39()
        {
            int maximumNumberOfSolutions = 0;
            int solutionsAreMaximizedAt = 0;

            for (int i = 2; i <= 1000; i++)
            {
                int solutions = 0;
                for (int a = 1; a < i; a++)
                {
                    for (int b = 1; b < i - a; b++)
                    {
                        if (Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)) == i - a - b)
                            solutions++;
                    }
                }
                if (solutions > maximumNumberOfSolutions)
                {
                    solutionsAreMaximizedAt = i;
                    maximumNumberOfSolutions = solutions;
                }
            }
            return solutionsAreMaximizedAt;
        }

        public static long Euler38()
        {
            int largestConcatenatedValue = 0;

            for (int i = 1; i < 1000000; i++)
            {
                bool isPandigital = true;
                string concatenatedString = "";
                for (int multiplier = 1; multiplier < 10; multiplier++)
                {
                    concatenatedString += (i * multiplier).ToString();
                    if (concatenatedString.Length >= 9)
                        break;
                }
                if (concatenatedString.Length != 9)
                    continue;
                else
                {
                    for (int digit = 1; digit < 10; digit++)
                        if (!concatenatedString.Contains(digit.ToString()))
                        {
                            isPandigital = false;
                            break;
                        }
                    if (isPandigital && int.Parse(concatenatedString) > largestConcatenatedValue)
                        largestConcatenatedValue = int.Parse(concatenatedString);
                }
            }

            return largestConcatenatedValue;
        }

        public static long Euler37()
        {
            List<int> primes = new List<int>();
            EstabilishPrimes(out primes);

            int sum = 0;

            for (int i = 11; i < 1000000; i += 2)
            {
                bool isTruncatable = true;
                string numberString = i.ToString();
                if (numberString.IndexOfAny("012468".ToCharArray()) >= 0)
                    continue;
                do
                {
                    if (!primes.Contains(int.Parse(numberString)))
                    {
                        isTruncatable = false;
                        break;
                    }
                    numberString = numberString.Remove(0, 1);
                } while (numberString.Length > 0);

                if (isTruncatable)
                {
                    numberString = i.ToString();
                    do
                    {
                        if (!primes.Contains(int.Parse(numberString)))
                        {
                            isTruncatable = false;
                            break;
                        }
                        numberString = numberString.Remove(numberString.Length - 1, 1);
                    } while (numberString.Length > 0);
                }

                if (isTruncatable)
                {
                    sum += i;
                }
            }

            return sum;
        }

        public static long Euler36()
        {
            int sum = 0;

            for (int i = 1; i < 1000000; i++)
            {
                bool isDoublePalindromic = true;

                string decimalString = i.ToString();
                string binaryString = Convert.ToString(i, 2);

                for (int symbolIndex = 0; symbolIndex < decimalString.Length / 2; symbolIndex++)
                {
                    if (decimalString[symbolIndex] != decimalString[decimalString.Length - symbolIndex - 1])
                    {
                        isDoublePalindromic = false;
                        break;
                    }
                }
                if (isDoublePalindromic)
                    for (int symbolIndex = 0; symbolIndex < binaryString.Length; symbolIndex++)
                    {
                        if (binaryString[symbolIndex] != binaryString[binaryString.Length - symbolIndex - 1])
                        {
                            isDoublePalindromic = false;
                            break;
                        }
                    }

                if (isDoublePalindromic)
                {
                    sum += i;
                }
            }

            return sum;
        }

        public static long Euler35()
        {
            List<int> primes = new List<int> { 2 };
            for (int i = 3; i < 1000000; i += 2)
            {
                bool isPrime = true;
                foreach (int prime in primes)
                {
                    if (i % prime == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                    primes.Add(i);
            }

            long circleCount = 0;

            for (int i = 2; i < 1000000; i++)
            {
                bool isCircular = true;
                string iString = i.ToString();
                for (int rotation = 0; rotation < iString.Length; rotation++)
                {
                    if (!primes.Contains(int.Parse(iString)))
                    {
                        isCircular = false;
                        break;
                    }
                    iString = iString.Insert(0, iString[iString.Length - 1].ToString());
                    iString = iString.Remove(iString.Length - 1, 1);
                }
                if (isCircular)
                    circleCount++;
            }

            return circleCount;
        }

        public static long Euler34()
        {
            //Dictionary<int, int> factorials = new Dictionary<int, int>();
            int[] factorials = new int[10] { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 };

            List<int> matchingNumbers = new List<int>();
            for (int i = 3; i < 3000000; i++)
            {
                string numberString = i.ToString();
                int sum = 0;
                foreach (char digitChar in numberString)
                {
                    int digit = int.Parse(digitChar.ToString());
                    sum += factorials[digit];
                    if (sum > i)
                        break;
                }
                if (sum == i)
                {
                    matchingNumbers.Add(i);
                    Console.WriteLine(i);
                }
            }

            return matchingNumbers.Sum();
        }

        public static long Euler33()
        {
            Dictionary<string, double> singleDigitFractions = new Dictionary<string, double>();

            for (double a = 1; a < 10; a++)
                for (double b = 1; b < 10; b++)
                    singleDigitFractions.Add(a.ToString() + b.ToString(), a / b);

            for (double a = 10; a < 100; a++)
                for (double b = 10; b < 100; b++)
                {
                    if (a.ToString()[1] == '0' && b.ToString()[1] == '0')
                        continue;

                    double result = a / b;

                    if (result < 1)
                    {
                        string combinedString = a.ToString() + b.ToString();
                        string trimmedString = combinedString;
                        if (combinedString[0] == combinedString[2])
                        {
                            trimmedString = trimmedString.Remove(2, 1);
                            trimmedString = trimmedString.Remove(0, 1);
                        }
                        else if (combinedString[0] == combinedString[3])
                        {
                            trimmedString = trimmedString.Remove(3, 1);
                            trimmedString = trimmedString.Remove(0, 1);
                        }
                        else if (combinedString[1] == combinedString[2])
                        {
                            trimmedString = trimmedString.Remove(2, 1);
                            trimmedString = trimmedString.Remove(1, 1);
                        }
                        else if (combinedString[1] == combinedString[3])
                        {
                            trimmedString = trimmedString.Remove(3, 1);
                            trimmedString = trimmedString.Remove(1, 1);
                        }

                        if (trimmedString.Length == 2 && !trimmedString.Contains('0') && singleDigitFractions[trimmedString] == result)
                        {
                            Console.WriteLine(a + " - " + b + " - " + trimmedString + " - " + result);
                        }
                    }
                }

            // The answers are: 16 / 64, 19 / 95, 26 / 65, and 49 / 98.
            // These shorten out to 1/4, 1/5, 2/5, and 4/8.
            // Taking the product of all these equals out to 0.25 * 0.2 * 0.4 * 0.5 = 0.01
            // The lowest terms of this fraction are 1/100, making "100" the answer to the question (being the denominator)

            return 100;
        }

        public static long Euler32()
        {
            List<int> products = new List<int>();

            for (int a = 1; a < 10000; a++)
                for (int b = 1; b < 10000; b++)
                {
                    int product = a * b;
                    if (a.ToString().Length + b.ToString().Length + product.ToString().Length == 9)
                    {
                        string textRepresentation = a.ToString() + b.ToString() + product.ToString();
                        for (int symbol = 1; symbol < 10; symbol++)
                        {
                            if (!textRepresentation.Contains(symbol.ToString()))
                                break;
                            if (symbol == 9 && !products.Contains(product))
                                products.Add(product);
                        }
                    }
                }
            return products.Sum();
        }
        
        #region Euler31
        public static List<int> possibleCoins = new List<int> { 1, 2, 5, 10, 20, 50, 100, 200 };

        public static long HowManyWaysToMakeChange(int valueIndex, int targetValue)
        {
            if (valueIndex == 0) 
                return 1;// we can always make change for N with pennies exactly one way: by using N pennies
            else if (targetValue == 0) 
                return 1;// there is one way to make change for 0: with no coins at all
            else if (targetValue < 0) 
                return 0;// there is no way to make negative change
            else
            {
                long waysToMake = 0;
                waysToMake = (HowManyWaysToMakeChange(valueIndex - 1, targetValue)
                            + HowManyWaysToMakeChange(valueIndex, targetValue - possibleCoins[valueIndex]));
                return waysToMake;
            }
        }

        public static long Euler31()
        {
            // Du har oändligt många av alla svenska mynt och sedlar.
            // Hur många olika sätt kan du göra 200kr?

            // Bara ettor: ett sätt
            // Ettor och tvåor: 100
            // Femmor och ettor: 40
            // Femmor och tvåor: 20

            long combinations = 0;

            combinations = HowManyWaysToMakeChange(possibleCoins.Count - 1, 200);

            return combinations;
        }
        #endregion
        public static long Euler30()
        {
            List<int> numbersWhoseDigitsToTheFifthPowerSummedEqualTheNumber = new List<int>();

            for (int i = 2; i < 10000000; i++)
            {
                string numberString = i.ToString();
                int sum = 0;
                for (int digitIndex = 0; digitIndex < numberString.Length; digitIndex++)
                {
                    sum += (int)Math.Pow(int.Parse(numberString[digitIndex].ToString()), 5);
                    if (sum > i)
                        break;
                }
                if (sum == i)
                    numbersWhoseDigitsToTheFifthPowerSummedEqualTheNumber.Add(i);
            }

            return numbersWhoseDigitsToTheFifthPowerSummedEqualTheNumber.Sum();
        }
        */
        #endregion

        #region Euler 20-29
        /*
        public static long Euler29()
        {
            List<BigInteger> allOfTheNumbers = new List<BigInteger>();

            for (BigInteger a = 2; a <= 100; a++)
                for (int b = 2; b <= 100; b++)
                {
                    BigInteger wowLookAtThatBigNumber = BigInteger.Pow(a, b);
                    if (allOfTheNumbers.Contains(wowLookAtThatBigNumber))
                        continue;
                    else
                        allOfTheNumbers.Add(wowLookAtThatBigNumber);
                }
            return allOfTheNumbers.Count;
        }

        public static long Euler28()
        {
            // 73 74 75 76 77 78 79 80 81
            // 72 43 44 45 46 47 48 49 50
            // 71 42 21 22 23 24 25 26 51
            // 70 41 20  7  8  9 10 27 52
            // 69 40 19  6  1  2 11 28 53
            // 68 39 18  5  4  3 12 29 54
            // 67 38 17 16 15 14 13 30 55
            // 66 37 36 35 34 33 32 31 56
            // 65 64 63 62 61 60 59 58 57

            long answer = 1;
            int currentNumber = 1;
            for (int i = 3; i <= 1001; i += 2)
            {
                for (int corner = 1; corner <= 4; corner++)
                {
                    currentNumber += i - 1;
                    answer += currentNumber;
                }
            }


            return answer;
        }

        public static long Euler27()
        {
            long answer = 0;
            int longestSequence = 0;

            List<int> lowPrimes = new List<int> { 2 };
            for (int i = 3; i < 1000000; i += 2)
            {
                bool isPrime = true;
                foreach (int prime in lowPrimes)
                {
                    if (i % prime == 0)
                    {
                        isPrime = false;
                        break;
                    }
                    if (prime > i / 2)
                        break;
                }
                if (isPrime)
                    lowPrimes.Add(i);
            }

            for (int a = -999; a < 1000; a++)
            {
                for (int b = -1000; b <= 1000; b++)
                {
                    bool sequenceHolding = true;
                    List<int> primes = new List<int>();

                    for (int n = 0; n < 100000; n++)
                    {
                        int number = (int)Math.Pow(n, 2) + a * n + b;
                        if (number <= 0)
                        {
                            sequenceHolding = false;
                            break;
                        }
                        foreach (int prime in lowPrimes)
                        {
                            if (prime > Math.Abs(number) / 2)
                                break;
                            if (number != prime && number % prime == 0)
                            {
                                sequenceHolding = false;
                                break;
                            }
                        }
                        if (sequenceHolding)
                        {
                            foreach (int prime in primes)
                            {
                                if (number != prime && number % prime == 0)
                                {
                                    sequenceHolding = false;
                                    break;
                                }
                            }
                            if (sequenceHolding)
                                primes.Add(number);
                            else
                                break;
                        }
                        else
                            break;
                    }
                    if (primes.Count > longestSequence)
                    {
                        answer = a * b;
                        longestSequence = primes.Count;
                        Console.WriteLine(a);
                        Console.WriteLine(b);
                    }
                }
            }
            return answer;
        }

        public static long Euler26()
        {
            int longestRecurringPatternLength = 0;
            long longestRecurringPatternIndex = 0;

            BigInteger wowThatIsABigNumber = 10;
            for (int i = 0; i < 10000; i++)
            {
                wowThatIsABigNumber *= 10;
            }

            for (BigInteger i = 7; i < 1000; i++)
            {
                BigInteger extremelyBigFraction = wowThatIsABigNumber / i;
                string fractionString = extremelyBigFraction.ToString();

                for (int sequenceLength = 2; sequenceLength < (fractionString.Length / 3) - 5; sequenceLength++)
                {
                    bool sequenceFound = false;
                    for (int startOffset = 0; startOffset < 6; startOffset++)
                    {
                        string firstString = fractionString.Substring(startOffset, sequenceLength);
                        string secondString = fractionString.Substring(startOffset + sequenceLength, sequenceLength);
                        string thirdString = fractionString.Substring(startOffset + sequenceLength + sequenceLength, sequenceLength);
                        if (firstString == secondString && secondString == thirdString)
                        {
                            if (sequenceLength > longestRecurringPatternLength)
                            {
                                longestRecurringPatternLength = sequenceLength;
                                longestRecurringPatternIndex = (long)i;
                                Console.WriteLine(longestRecurringPatternIndex.ToString() + " - " + longestRecurringPatternLength.ToString() + " - " + firstString);
                                Console.WriteLine();
                            }
                            sequenceFound = true;
                            break;
                        }
                    }
                    if (sequenceFound)
                        break;
                }
            }

            return longestRecurringPatternIndex;
        }

        public static long Euler25()
        {
            uint[] currentFibonacci = new uint[10000];
            uint[] previousFibonacci = new uint[10000];
            uint[] previousPreviousFibonacci = new uint[10000];

            int fibonacciIndex = 1;
            int numberOfDigits = 1;

            currentFibonacci[0] = 1;

            for (int i = 2; i <= 100000; i++)
            {
                for (int digit = numberOfDigits - 1; digit >= 0; digit--)
                {
                    previousPreviousFibonacci[digit] = previousFibonacci[digit];
                    previousFibonacci[digit] = currentFibonacci[digit];

                    currentFibonacci[digit] = previousFibonacci[digit] + previousPreviousFibonacci[digit];
                    int numberOfTensMovedUp = 0;
                    while (currentFibonacci[digit] >= 10)
                    {
                        if (digit == numberOfDigits - 1)
                            numberOfDigits++;
                        currentFibonacci[digit + 1] += 1;
                        currentFibonacci[digit] -= 10;
                        digit++;
                        numberOfTensMovedUp++;
                    }
                    digit -= numberOfTensMovedUp;
                }

                Console.WriteLine(numberOfDigits);
                if (numberOfDigits == 1000)
                {
                    fibonacciIndex = i;
                    break;
                }
            }

            return fibonacciIndex;
        }

        public static long Euler24()
        {
            //The number of lexicographic permutations starting with each number (if using 0-9) is 9!, which is 362880
            //So we'll just skip 0 and 1 by starting at 362880 * 2 + 1.

            // <insert time trying to figure out how to write a function for lexicographic permutations>

            //Alright so writing a program for this is difficult and also boring. Therefore: MATH!
            //The number of permutations for each STARTING SYMBOL when using n total symbols is (n-1)!
            //So if we're using 10 symbols (0 to 9) then when we've stepped through 9! permutations, the first symbol will shift once.
            //Then we just check how many times we can subtract each factorial from the target number.
            //We can subtract 9! twice, shifting the first symbol twice (0 -> 1 -> 2)
            //Now we just look at the rest of the symbols, ignoring the first one. How many times can we subtract 8! ? (it's 6 times)
            //Then the same for 7!, 6!, 5!, 4! and so on until we can no longer subtract anything from the target number (because it's 0)
            //Shift each symbol by the amount of times we can subtract the factorials and et voila, the answer!

            //0123456789
            //9! * 2
            //2013456789
            //8! * 6
            //2701345689
            //7! * 6
            //2780134569
            //6!*2
            //2783014569
            //5!*5
            //2783901456
            //4!*1
            //2783910456
            //3!*2
            //2783915046
            //2!*1
            //2783915406
            //1!*1
            //2783915460

            return 2783915460;
        }

        public static long Euler23()
        {
            List<int> abundantNumbers = new List<int>();

            for (int i = 0; i < 28123; i++)
            {
                int factorSum = 0;
                for (int factor = 1; factor < i; factor++)
                {
                    if (i % factor == 0)
                        factorSum += factor;
                }
                if (factorSum > i)
                    abundantNumbers.Add(i);
            }

            long abundantSum = 0;

            for (int i = 0; i < 28124; i++)
            {
                bool summable = false;
                for (int firstIterator = 0; firstIterator < abundantNumbers.Count; firstIterator++)
                {
                    if (abundantNumbers[firstIterator] > i / 2)
                        break;
                    for (int secondIterator = firstIterator; secondIterator < abundantNumbers.Count; secondIterator++)
                    {
                        if (abundantNumbers[firstIterator] + abundantNumbers[secondIterator] > i)
                            break;
                        if (abundantNumbers[firstIterator] + abundantNumbers[secondIterator] == i)
                        {
                            summable = true;
                            break;
                        }
                    }
                    if (summable)
                        break;
                }

                if (!summable)
                    abundantSum += i;
            }

            return abundantSum;
        }

        public static long Euler22()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB"); // this sumbitch right here is VERY important!!!

            List<string> names = new List<string>();
            try
            {
                names.AddRange(File.ReadAllText("names.txt").Split(','));
            }
            catch (Exception e)
            { throw e; }

            for (int i = 0; i < names.Count; i++)
                names[i] = names[i].Trim('"');
            names.Sort();

            long nameScoreTotal = 0;

            for (int i = 0; i < names.Count; i++)
            {
                int nameScore = 0;
                for (int letter = 0; letter < names[i].Length; letter++)
                    nameScore += (names[i][letter] - 64);
                nameScore *= (i + 1);
                nameScoreTotal += nameScore;
            }

            return nameScoreTotal;
        }

        public static long Euler21()
        {
            Dictionary<int, int> factorSums = new Dictionary<int, int>();

            for (int i = 0; i < 10000; i++)
            {
                int factorSum = 0;
                for (int divisor = 1; divisor < i; divisor++)
                {
                    if (i % divisor == 0)
                        factorSum += divisor;
                }
                factorSums.Add(i, factorSum);
            }

            long amicableSum = 0;
            List<int> checkedSums = new List<int>();

            for (int i = 1; i < 10000; i++)
            {
                if (checkedSums.Contains(i))
                    continue;

                if (!factorSums.ContainsKey(factorSums[i]))
                    continue;

                if (factorSums[factorSums[i]] == i && factorSums[i] != i)
                {
                    amicableSum += factorSums[i];
                    amicableSum += i;
                    checkedSums.Add(i);
                    checkedSums.Add(factorSums[i]);
                }
            }

            return amicableSum;
        }

        #region Euler20
        public static void Euler20_MoveTensToNextDigit(uint[] digits, int indexToMove, int digitCount)
        {
            while (digits[indexToMove] >= 10)
            {
                if (indexToMove == digitCount - 1)
                    digitCount++;
                digits[indexToMove + 1] += 1;
                digits[indexToMove] -= 10;
                if (digits[indexToMove + 1] >= 10)
                    Euler20_MoveTensToNextDigit(digits, indexToMove + 1, digitCount);
            }
        }

        public static long Euler20()
        {
            uint[] digits = new uint[10000];
            digits[0] = 1;
            int digitCount = 1;

            for (int i = 1; i <= 100; i++)
            {
                for (int digit = digitCount - 1; digit >= 0; digit--)
                {
                    digits[digit] *= (uint)i;
                    if (digits[digit] >= 10)
                        Euler20_MoveTensToNextDigit(digits, digit, digitCount);
                }
            }

            long answer = 0;
            foreach (byte digit in digits)
                answer += digit;

            return answer;
        }
        #endregion
        */
        #endregion
            
        #region Euler 10-19
        /*
        public static int Euler19()
        {
            int day = 1;
            int weekday = 2;
            int month = 1;
            int year = 1901;
            int sundays = 0;

            for (day = 1; year < 2001; day++)
            {
                weekday++;
                if (weekday == 8)
                    weekday = 1;

                if ((day > 30 && (month == 4 || month == 6 || month == 9 || month == 11)) ||
                    (day > 31 && (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)) ||
                    (day > 28 && month == 2 && (year % 4 != 0 || (year % 100 == 0 && year % 400 != 0))) ||
                    (day > 29 && month == 2 && (year % 4 == 0 || year % 400 == 0)))
                {
                    month++;
                    if (month == 13)
                    {
                        month = 1;
                        year++;
                    }
                    day = 1;
                    if (weekday == 7)
                        sundays++;
                }
            }

            return sundays;
        }

        public static int Euler18()
        {
            List<int[]> rows = new List<int[]>();
            rows.Add(new int[] { 75 });
            rows.Add(new int[] { 95, 64 });
            rows.Add(new int[] { 17, 47, 82 });
            rows.Add(new int[] { 18, 35, 87, 10 });
            rows.Add(new int[] { 20, 04, 82, 47, 65 });
            rows.Add(new int[] { 19, 01, 23, 75, 03, 34 });
            rows.Add(new int[] { 88, 02, 77, 73, 07, 63, 67 });
            rows.Add(new int[] { 99, 65, 04, 28, 06, 16, 70, 92 });
            rows.Add(new int[] { 41, 41, 26, 56, 83, 40, 80, 70, 33 });
            rows.Add(new int[] { 41, 48, 72, 33, 47, 32, 37, 16, 94, 29 });
            rows.Add(new int[] { 53, 71, 44, 65, 25, 43, 91, 52, 97, 51, 14 });
            rows.Add(new int[] { 70, 11, 33, 28, 77, 73, 17, 78, 39, 68, 17, 57 });
            rows.Add(new int[] { 91, 71, 52, 38, 17, 14, 91, 43, 58, 50, 27, 29, 48 });
            rows.Add(new int[] { 63, 66, 04, 68, 89, 53, 67, 30, 73, 16, 69, 87, 40, 31 });
            rows.Add(new int[] { 04, 62, 98, 27, 23, 09, 70, 98, 73, 93, 38, 53, 60, 04, 23 });

            for (int y = rows.Count - 2; y >= 0; y--)
            {
                for (int x = 0; x < rows[y].Length; x++)
                {
                    rows[y][x] += (rows[y + 1][x] > rows[y + 1][x + 1]) ? rows[y + 1][x] : rows[y + 1][x + 1];
                }
            }

            return rows[0][0];
        }

        #region Euler17
        public static int Euler17_CountLettersInWords(int number)
        {
            int count = 0;
            int hundred = 0;
            int ten = 0;
            int one = 0;

            if (number >= 100)
                hundred = number / 100;
            if (number >= 10)
                ten = (number % 100) / 10;
            one = number % 10;

            if (hundred == 1 || hundred == 2 || hundred == 6)
                count += 10;
            else if (hundred == 4 || hundred == 5 || hundred == 9)
                count += 11;
            else if (hundred == 3 || hundred == 7 || hundred == 8)
                count += 12;
            else if (hundred == 10)
                count += 11;

            if (hundred > 0 && (ten + one) > 0)
                count += 3;

            if (ten == 4 || ten == 5 || ten == 6)
                count += 5;
            else if (ten == 2 || ten == 3 || ten == 8 || ten == 9)
                count += 6;
            else if (ten == 7)
                count += 7;

            if (ten == 1)
            {
                if (one == 0)
                    count += 3;
                else if (one == 1 || one == 2)
                    count += 6;
                else if (one == 5 || one == 6)
                    count += 7;
                else if (one == 3 || one == 4 || one == 8 || one == 9)
                    count += 8;
                else if (one == 7)
                    count += 9;
            }
            else if (one > 0)
            {
                if (one == 1 || one == 2 || one == 6)
                    count += 3;
                else if (one == 4 || one == 5 || one == 9)
                    count += 4;
                else if (one == 3 || one == 7 || one == 8)
                    count += 5;
            }

            return count;
        }

        private static long Euler17()
        {
            long answer = 0;
            for (int i = 1; i <= 1000; i++)
                answer += Euler17_CountLettersInWords(i);

            return answer;
        }
        #endregion

        private static long Euler16()
        {
            byte[] digits = new byte[1000];
            int digitCount = 1;
            digits[0] = 2;
            for (int i = 2; i <= 1000; i++)
            {
                for (int digit = digitCount - 1; digit >= 0; digit--)
                {
                    digits[digit] *= 2;
                    if (digits[digit] >= 10)
                    {
                        if (digit == digitCount - 1)
                            digitCount++;
                        digits[digit + 1] += 1;
                        digits[digit] -= 10;
                    }
                }
            }

            long answer = 0;
            foreach (byte digit in digits)
                answer += digit;

            return answer;
        }

        private static long Euler15()
        {
            int GRIDSIZE_VERTICAL = 21;
            int GRIDSIZE_HORIZONTAL = 21;

            long[,] pathsToLocation = new long[GRIDSIZE_HORIZONTAL, GRIDSIZE_VERTICAL];
            pathsToLocation[0, 0] = 1;

            for (int x = 0; x < GRIDSIZE_HORIZONTAL; x++)
            {
                for (int y = 0; y < GRIDSIZE_VERTICAL; y++)
                {
                    if (x < GRIDSIZE_HORIZONTAL - 1)
                        pathsToLocation[x + 1, y] += pathsToLocation[x, y];
                    if (y < GRIDSIZE_VERTICAL - 1)
                        pathsToLocation[x, y + 1] += pathsToLocation[x, y];
                }
            }

            return pathsToLocation[GRIDSIZE_HORIZONTAL - 1, GRIDSIZE_VERTICAL - 1];
        }

        private static long Euler14()
        {
            ulong number = 0;
            int longestChainCount = 0;
            ulong longestChainingStartValue = 0;
            Dictionary<ulong, int> chains = new Dictionary<ulong, int>();

            for (ulong i = 1; i < 1000000; i++)
            {
                number = i;
                int chainCount = 0;

                while (number != 1)
                {
                    chainCount++;
                    number = ((number % 2 == 0) ? (number / 2) : (number * 3) + 1);

                    if (chains.ContainsKey(number))
                    {
                        chainCount += chains[number];
                        break;
                    }
                }

                chains.Add(i, chainCount);

                if (chainCount > longestChainCount)
                {
                    longestChainCount = chainCount;
                    longestChainingStartValue = i;
                    Console.WriteLine(i + " - " + longestChainCount);
                }
            }

            return (long)longestChainingStartValue;
        }

        private static long Euler13()
        {
            string[] extremelyLongNumbers = {"37107287533902102798797998220837590246510135740250",
                                            "46376937677490009712648124896970078050417018260538",
                                            "74324986199524741059474233309513058123726617309629",
                                            "91942213363574161572522430563301811072406154908250",
                                            "23067588207539346171171980310421047513778063246676",
                                            "89261670696623633820136378418383684178734361726757",
                                            "28112879812849979408065481931592621691275889832738",
                                            "44274228917432520321923589422876796487670272189318",
                                            "47451445736001306439091167216856844588711603153276",
                                            "70386486105843025439939619828917593665686757934951",
                                            "62176457141856560629502157223196586755079324193331",
                                            "64906352462741904929101432445813822663347944758178",
                                            "92575867718337217661963751590579239728245598838407",
                                            "58203565325359399008402633568948830189458628227828",
                                            "80181199384826282014278194139940567587151170094390",
                                            "35398664372827112653829987240784473053190104293586",
                                            "86515506006295864861532075273371959191420517255829",
                                            "71693888707715466499115593487603532921714970056938",
                                            "54370070576826684624621495650076471787294438377604",
                                            "53282654108756828443191190634694037855217779295145",
                                            "36123272525000296071075082563815656710885258350721",
                                            "45876576172410976447339110607218265236877223636045",
                                            "17423706905851860660448207621209813287860733969412",
                                            "81142660418086830619328460811191061556940512689692",
                                            "51934325451728388641918047049293215058642563049483",
                                            "62467221648435076201727918039944693004732956340691",
                                            "15732444386908125794514089057706229429197107928209",
                                            "55037687525678773091862540744969844508330393682126",
                                            "18336384825330154686196124348767681297534375946515",
                                            "80386287592878490201521685554828717201219257766954",
                                            "78182833757993103614740356856449095527097864797581",
                                            "16726320100436897842553539920931837441497806860984",
                                            "48403098129077791799088218795327364475675590848030",
                                            "87086987551392711854517078544161852424320693150332",
                                            "59959406895756536782107074926966537676326235447210",
                                            "69793950679652694742597709739166693763042633987085",
                                            "41052684708299085211399427365734116182760315001271",
                                            "65378607361501080857009149939512557028198746004375",
                                            "35829035317434717326932123578154982629742552737307",
                                            "94953759765105305946966067683156574377167401875275",
                                            "88902802571733229619176668713819931811048770190271",
                                            "25267680276078003013678680992525463401061632866526",
                                            "36270218540497705585629946580636237993140746255962",
                                            "24074486908231174977792365466257246923322810917141",
                                            "91430288197103288597806669760892938638285025333403",
                                            "34413065578016127815921815005561868836468420090470",
                                            "23053081172816430487623791969842487255036638784583",
                                            "11487696932154902810424020138335124462181441773470",
                                            "63783299490636259666498587618221225225512486764533",
                                            "67720186971698544312419572409913959008952310058822",
                                            "95548255300263520781532296796249481641953868218774",
                                            "76085327132285723110424803456124867697064507995236",
                                            "37774242535411291684276865538926205024910326572967",
                                            "23701913275725675285653248258265463092207058596522",
                                            "29798860272258331913126375147341994889534765745501",
                                            "18495701454879288984856827726077713721403798879715",
                                            "38298203783031473527721580348144513491373226651381",
                                            "34829543829199918180278916522431027392251122869539",
                                            "40957953066405232632538044100059654939159879593635",
                                            "29746152185502371307642255121183693803580388584903",
                                            "41698116222072977186158236678424689157993532961922",
                                            "62467957194401269043877107275048102390895523597457",
                                            "23189706772547915061505504953922979530901129967519",
                                            "86188088225875314529584099251203829009407770775672",
                                            "11306739708304724483816533873502340845647058077308",
                                            "82959174767140363198008187129011875491310547126581",
                                            "97623331044818386269515456334926366572897563400500",
                                            "42846280183517070527831839425882145521227251250327",
                                            "55121603546981200581762165212827652751691296897789",
                                            "32238195734329339946437501907836945765883352399886",
                                            "75506164965184775180738168837861091527357929701337",
                                            "62177842752192623401942399639168044983993173312731",
                                            "32924185707147349566916674687634660915035914677504",
                                            "99518671430235219628894890102423325116913619626622",
                                            "73267460800591547471830798392868535206946944540724",
                                            "76841822524674417161514036427982273348055556214818",
                                            "97142617910342598647204516893989422179826088076852",
                                            "87783646182799346313767754307809363333018982642090",
                                            "10848802521674670883215120185883543223812876952786",
                                            "71329612474782464538636993009049310363619763878039",
                                            "62184073572399794223406235393808339651327408011116",
                                            "66627891981488087797941876876144230030984490851411",
                                            "60661826293682836764744779239180335110989069790714",
                                            "85786944089552990653640447425576083659976645795096",
                                            "66024396409905389607120198219976047599490197230297",
                                            "64913982680032973156037120041377903785566085089252",
                                            "16730939319872750275468906903707539413042652315011",
                                            "94809377245048795150954100921645863754710598436791",
                                            "78639167021187492431995700641917969777599028300699",
                                            "15368713711936614952811305876380278410754449733078",
                                            "40789923115535562561142322423255033685442488917353",
                                            "44889911501440648020369068063960672322193204149535",
                                            "41503128880339536053299340368006977710650566631954",
                                            "81234880673210146739058568557934581403627822703280",
                                            "82616570773948327592232845941706525094512325230608",
                                            "22918802058777319719839450180888072429661980811197",
                                            "77158542502016545090413245809786882778948721859617",
                                            "72107838435069186155435662884062257473692284509516",
                                            "20849603980134001723930671666823555245252804609722",
                                            "53503534226472524250874054075591789781264330331690"};

            ulong[] sumOfDigits = new ulong[extremelyLongNumbers[0].Length];

            for (int i = 0; i < extremelyLongNumbers[0].Length; i++)
            {
                foreach (string extremelyLongNumber in extremelyLongNumbers)
                {
                    sumOfDigits[i] += (ulong)int.Parse(extremelyLongNumber[i].ToString());
                }
            }

            for (int i = 1; i < 12; i++)
            {
                sumOfDigits[0] *= 10;
                sumOfDigits[0] += sumOfDigits[i];
            }

            ulong answer = 0;
            for (int i = 0; i < 10; i++)
            {
                answer *= 10;
                answer += (ulong)int.Parse(sumOfDigits[0].ToString()[i].ToString());
            }

            return (long)answer;
        }

        private static long Euler12()
        {
            long triangle = 0;
            int numberOfFactorsMax = 0;

            for (int i = 1; i < int.MaxValue; i++)
            {
                triangle += i;
                int numberOfFactors = 0;
                if (triangle % 2 == 1)// || triangle < 66176760)
                    continue;

                List<int> primeFactors = new List<int>();
                long dividedTriangle = triangle;

                for (int divisor = 2; divisor <= dividedTriangle; divisor++)
                {
                    while (dividedTriangle % divisor == 0)
                    {
                        primeFactors.Add(divisor);
                        dividedTriangle /= divisor;
                    }
                }

                List<int> primeExponents = new List<int>();
                numberOfFactors = 1;

                for (int currentPrime = 0; true; currentPrime = primeFactors[0])
                {
                    primeExponents.Add(primeFactors.FindAll(delegate (int prime) { return prime == currentPrime; }).Count + 1);
                    primeFactors.RemoveAll(delegate (int prime) { return prime == currentPrime; });
                    if (primeFactors.Count == 0)
                        break;
                }

                foreach (int exponent in primeExponents)
                    numberOfFactors *= exponent;

                // The below function takes FOREVER to get 500 factors. North of half an hour running on LAPTOP-2000.
                // I had enough time while it was running to find a better solution by googling some prime factorization math.
                
                if (numberOfFactors > numberOfFactorsMax)
                {
                    numberOfFactorsMax = numberOfFactors;
                    Console.WriteLine(triangle + " - " + numberOfFactorsMax);
                }
                if (numberOfFactors > 500)
                    return triangle;
            }
            return 0;
        }

        private static int Euler11()
        {
            int[,] numbers = new int[,]
                        {
                {08,02,22,97,38,15,00,40,00,75,04,05,07,78,52,12,50,77,91,08},
                {49,49,99,40,17,81,18,57,60,87,17,40,98,43,69,48,04,56,62,00},
                {81,49,31,73,55,79,14,29,93,71,40,67,53,88,30,03,49,13,36,65},
                {52,70,95,23,04,60,11,42,69,24,68,56,01,32,56,71,37,02,36,91},
                {22,31,16,71,51,67,63,89,41,92,36,54,22,40,40,28,66,33,13,80},
                {24,47,32,60,99,03,45,02,44,75,33,53,78,36,84,20,35,17,12,50},
                {32,98,81,28,64,23,67,10,26,38,40,67,59,54,70,66,18,38,64,70},
                {67,26,20,68,02,62,12,20,95,63,94,39,63,08,40,91,66,49,94,21},
                {24,55,58,05,66,73,99,26,97,17,78,78,96,83,14,88,34,89,63,72},
                {21,36,23,09,75,00,76,44,20,45,35,14,00,61,33,97,34,31,33,95},
                {78,17,53,28,22,75,31,67,15,94,03,80,04,62,16,14,09,53,56,92},
                {16,39,05,42,96,35,31,47,55,58,88,24,00,17,54,24,36,29,85,57},
                {86,56,00,48,35,71,89,07,05,44,44,37,44,60,21,58,51,54,17,58},
                {19,80,81,68,05,94,47,69,28,73,92,13,86,52,17,77,04,89,55,40},
                {04,52,08,83,97,35,99,16,07,97,57,32,16,26,26,79,33,27,98,66},
                {88,36,68,87,57,62,20,72,03,46,33,67,46,55,12,32,63,93,53,69},
                {04,42,16,73,38,25,39,11,24,94,72,18,08,46,29,32,40,62,76,36},
                {20,69,36,41,72,30,23,88,34,62,99,69,82,67,59,85,74,04,36,16},
                {20,73,35,29,78,31,90,01,74,31,49,71,48,86,81,16,23,57,05,54},
                {01,70,54,71,83,51,54,69,16,92,33,48,61,43,52,01,89,19,67,48}
                        };

            int largestProduct = 0;

            // vertical sums
            for (int y = 0; y < 17; y++) // loop through columns
            {
                for (int x = 0; x < 20; x++) // loop through rows
                {
                    int product = (numbers[y, x] * numbers[y + 1, x] * numbers[y + 2, x] * numbers[y + 3, x]);
                    largestProduct = (product > largestProduct) ? product : largestProduct;
                }
            }

            // horizontal sums
            for (int x = 0; x < 17; x++) // loop through rows
            {
                for (int y = 0; y < 20; y++) // loop through columns
                {
                    int product = (numbers[y, x] * numbers[y, x + 1] * numbers[y, x + 2] * numbers[y, x + 3]);
                    largestProduct = (product > largestProduct) ? product : largestProduct;
                }
            }

            // diagonal sums, NW to SE
            for (int x = 0; x < 17; x++) // loop through rows
            {
                for (int y = 0; y < 17; y++) // loop through columns
                {
                    int product = (numbers[y, x] * numbers[y + 1, x + 1] * numbers[y + 1, x + 2] * numbers[y + 1, x + 3]);
                    largestProduct = (product > largestProduct) ? product : largestProduct;
                }
            }

            // diagonal sums, NE to SW
            for (int x = 3; x < 20; x++) // loop through rows
            {
                for (int y = 0; y < 17; y++) // loop through columns
                {
                    int product = (numbers[y, x] * numbers[y + 1, x - 1] * numbers[y + 2, x - 2] * numbers[y + 3, x - 3]);
                    largestProduct = (product > largestProduct) ? product : largestProduct;
                }
            }

            return largestProduct;
        }
        */
        #endregion
    }
}
