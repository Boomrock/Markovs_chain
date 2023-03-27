using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Markovs_chain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sizex = 3;
            int sizey = 3;
            string[] first = new string[] { "C", "d" };
            string[] second = new string[] { "t" };
            char[,] mapch = new char[sizey, sizex];
            float[,] map = new float[sizey, sizex];
            float[,] avr = new float[sizey, sizex];

            #region постановка стола 



            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int z = 0; z < map.GetLength(1); z++)
                {
                    map[i, z] = 1;
                }
            }
            map[0, 0] = 0;
            map = generateSector(map, 100);
            map = generateSector(map, 10);



            for (int z = 0; z < map.GetLength(0); z++)
            {
                for (int d = 0; d < map.GetLength(1); d++)
                {
                    avr[z, d] += (float)map[z, d] / 10;

                }


            }
            float max = Max(map);
            for (int i = 0; i < mapch.GetLength(0); i++)
            {
                for (int z = 0; z < mapch.GetLength(1); z++)
                {
                    mapch[i, z] = ' ';
                    if (map[i, z] >= max - max / 30)
                    {
                        mapch[i, z] = first[0][0];
                    }
                }
            }


            printarray(map);
            printarray(mapch);

            Console.WriteLine();
            #endregion


            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int z = 0; z < map.GetLength(1); z++)
                {
                    map[i, z] = 0;
                    if (checkNeighbour(mapch, i, z, 'C'))
                    {
                        map[i, z] = 1;
                    }
                }
            }
            printarray(map);
            map = generateSector(map, 20);
            softArray(map);
            softArray(map);
            softArray(map);
            softArray(map);
            softArray(map);
            Filter(mapch, map,'C');

            printarray(map);
            max = Max(map);
            int countChair = 2;
            for (int i = 0; i < mapch.GetLength(0); i++)
            {
                for (int z = 0; z < mapch.GetLength(1); z++)
                {

                    if (map[i, z] >= (max - max / (2 * countChair)))
                    {
                        mapch[i, z] = second[0][0];
                    }
                }
            }
            printarray(mapch);

            /* int size = 5;
             int[,] avr = new int[size, size];
             for (int i = 0; i < size; i++)
             {
                 for (int z = 0; z < size; z++)
                 {
                     avr[i, z] = 0;
                 }

                 }
             for (int t = 0; t <100; t++)
             {
                 int[,] map= new int[size,size];

             for (int i = 0; i < size; i++)
             {
                 for (int z = 0; z < size; z++)
                 {
                         map[i, z] = Math.Abs(size/2 - i ) + Math.Abs(size / 2 - z) + 1;
                 }
             }
                 map[2, 0] = 0;

                 float sum = 0;
             for (int i = 0; i < size; i++)
             {
                 for (int z = 0; z < size; z++)
                 {
                     sum += map[i, z];
                 }
             }
             List<float> p = new List<float>();
             for (int i = 0; i < size; i++)
             {

                 for (int z = 0; z < size; z++)
                 {
                     p.Add(map[i,z] / sum);
                 }
             }
             Link markivsChain = new Link(
                 p.ToArray()
             );
             for (int i = 0; i < 1; i++)
             {

                     var current = (int)markivsChain.NextElement();
                     map[current / size, current % size] += 3;

             }
             sum = 0;
             for (int i = 0; i < size; i++)
             {
                 for (int z = 0; z < size; z++)
                 {
                     sum += map[i, z];
                 }
             }
             p = new List<float>();
             for (int i = 0; i < size; i++)
             {

                 for (int z = 0; z < size; z++)
                 {
                     p.Add(map[i, z] / sum);
                 }
             }
             markivsChain = new Link(
                 p.ToArray()
             );
             for (int i = 0; i < 30; i++)
             {

                 var current = (int)markivsChain.NextElement();
                 map[current / size, current % size] += 1;

             }
                 for (int i = 0; i < size; i++)
                 {
                     for (int z = 0; z < size; z++)
                     {
                         Console.Write(map[i, z] + " ");
                     }
                     Console.WriteLine();

                 }
                 Console.WriteLine();
                 for (int i = 0; i < size; i++)
                 {
                     for (int z = 0; z < size; z++)
                     {
                         avr[i, z] += map[i, z];
                     }
                 }

             }
             for (int i = 0; i < size; i++)
             {
                 for (int z = 0; z < size; z++)
                 {
                     Console.Write(((float)avr[i, z])/100 + " ");
                 }
                 Console.WriteLine();

             }
             */
        }

        private static void Filter(char[,] mapch, float[,] map, char ch)
        {

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int z = 0; z < map.GetLength(1); z++)
                {
                    if (mapch[i, z] == ch)
                    {
                        map[i, z] = 0;
                    }
                }
            }
        }

        private static void RoundArray(float[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int z = 0; z < map.GetLength(1); z++)
                {
                    map[i, z] = MathF.Round(map[i, z]);
                }
            }
        }

        static bool checkNeighbour(char[,] map,int y,int x, char ch)
        {

            for (int a = -1; a <= 1; a++)
            {
                for (int s = -1; s <= 1; s++)
                {
                    if (y+a >= 0 && y+a< map.GetLength(0) && x + s >= 0 && x + s < map.GetLength(1) &&( a != 0 || s!=0))
                    {
                        if (map[a + y, x + s] == ch)
                            return true;
                    }
                }
            }
            return false;
        }

        private static float Max(float[,] map)
        {
            float max = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int z = 0; z < map.GetLength(1); z++)
                {
                    if (max < map[i, z])
                    {
                        max = map[i, z];
                    }
                }
            }

            return max;
        }

        static void printarray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int z = 0; z < array.GetLength(1); z++)
                {
                    Console.Write(array[i, z] + " ");
                }
                Console.WriteLine();
            }
             
        }
        static void printarray(char[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int z = 0; z < array.GetLength(1); z++)
                {
                    Console.Write(array[i, z] + " ");
                }
                Console.WriteLine();
            }

        }
        static void printarray(float[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int z = 0; z < array.GetLength(1); z++)
                {
                    Console.Write(array[i, z] + " ");
                }
                Console.WriteLine();
            }

        }
        static float[,] generateSector(float[,] map, int countStep)
        {

            float sum = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int z = 0; z < map.GetLength(1); z++)
                {
                    sum += map[i, z];
                }
            }
            List<float> p = new List<float>();
            for (int i = 0; i < map.GetLength(0); i++)
            {

                for (int z = 0; z < map.GetLength(1); z++)
                {
                    p.Add(map[i, z] / sum);
                }
            }
            Link markivsChain = new Link(
                p.ToArray()
            );
            for (int i = 0; i < countStep; i++)
            {

                var current = markivsChain.NextElement();
                map[current / map.GetLength(1), current % map.GetLength(1)] += 1;

            }


            return map;
            /*  for (int r = -1; r < 1; r++)
              {
                  for (int u = -1; u < 1; u++)
                  {
                      if (r + i >= 0 && r + i < map.GetLength(0) && u + z >= 0 && u + z < map.GetLength(1))
                          s += (map[r + i, u + z] / 6);
                  }
              }*/
        }

        private static float[,] softArray(float[,] map)
        {
            float sum = 0;
            float[,] gradient = (float[,])map.Clone();
            for (int i = 0; i < map.GetLength(0); i++)
            {

                for (int z = 0; z < map.GetLength(1); z++)
                {

                    sum = 0;

                    gradient[i, z] = 0;
                    for (int r = -1; r <= 1; r++)
                    {
                        for (int u = -1; u <= 1; u++)
                        {

                            if ((r + i) >= 0 && (r + i) < map.GetLength(0) && u + z >= 0 && u + z < map.GetLength(1))
                            {


                                sum += (map[r + i, u + z])/9;
                                
                             
                            }

                        }
                    }
                     gradient[i, z] = sum;
                }
            }
            return gradient;
        }
    }
    
    enum en
    {
        kkk,
        d,
        c,
        C,

    }
}
