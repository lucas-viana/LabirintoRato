using System;
using System.Collections;
using System.Collections.Generic;

namespace LabirintoCSharp
{
    internal class Program
    {
        private const int limit = 16;


        static void mostrarLabirinto(char[,] array, int l, int c)
        {
            for (int i = 0; i < l; i++)
            {
                Console.WriteLine();
                if (i < 10)
                {
                    Console.Write(i + " ");
                }
                else
                {
                    Console.Write(i);
                }
                for (int j = 0; j < c; j++)
                {
                    Console.Write($" {array[i, j]} ");
                }
                if (i == l - 1)
                {
                    int contador = 0;
                    Console.WriteLine();
                    while (contador < limit)
                    {
                        if (contador == 0)
                        {
                            Console.Write("   " + contador + " ");
                            contador++;
                        }
                        else if (contador < 10)
                        {
                            Console.Write(" " + contador + " ");
                            contador++;
                        }
                        else
                        {
                            Console.Write(" " + contador + "");
                            contador++;
                        }
                    }
                }
            }
            Console.WriteLine();
        }

        public static bool PodeMover(char[,] labirinto, int l, int c)
        {
            if (labirinto[l, c] is '.' or 'Q')
            {
                return true;
            }
            else
                return false;
        }
        public static bool QueijoEncontrado(char[,] labirinto, int l, int c)
        {
            if (labirinto[l, c] is 'Q')
            {
                return true;
            }
            else 
                return false;
        }

        static void buscarQueijo(char[,] meuLab, Posicao posicao)
        {
            Stack<int> minhaPilha = new Stack<int>();

            int movimentos = 0;
            do
            {
                int i = posicao.Linha;
                int j = posicao.Coluna;
                if (movimentos == minhaPilha.Count)
                {
                    if (QueijoEncontrado(meuLab, i, j))
                    {
                        Console.WriteLine("Queijo encontrado");
                        break;
                    }
                    meuLab[posicao.Linha, posicao.Coluna] = 'R';
                    minhaPilha.Push(i);
                    minhaPilha.Push(j);
                }
                Console.Clear();
                mostrarLabirinto(meuLab, limit, limit);

                
                //direita
                if (PodeMover(meuLab, i, j + 1) && j < 15)
                {
                    if(QueijoEncontrado(meuLab,i,j + 1))
                    {
                        Console.WriteLine("Queijo encontrado");
                        break;
                    }
                    minhaPilha.Push(i);
                    minhaPilha.Push(j);
                    posicao.Coluna++;
                    meuLab[posicao.Linha, posicao.Coluna] = 'R';

                }
                //esquerda
                else if (PodeMover(meuLab, i, j - 1) && j > 0)
                {
                    if (QueijoEncontrado(meuLab, i, j - 1))
                    {
                        Console.WriteLine("Queijo encontrado");
                        break;
                    }
                    minhaPilha.Push(i);
                    minhaPilha.Push(j);
                    posicao.Coluna--;
                    meuLab[posicao.Linha, posicao.Coluna] = 'R';
                }
                //baixo
                else if (PodeMover(meuLab, i + 1, j) && i < 15)
                {
                    if (QueijoEncontrado(meuLab, i + 1, j))
                    {
                        Console.WriteLine("Queijo encontrado");
                        break;
                    }
                    minhaPilha.Push(i);
                    minhaPilha.Push(j);
                    posicao.Linha++;
                    meuLab[posicao.Linha, posicao.Coluna] = 'R';
                }
                //acima
                else if (PodeMover(meuLab, i - 1, j) && i > 0)
                {
                    if (QueijoEncontrado(meuLab, i -1, j))
                    {
                        Console.WriteLine("Queijo encontrado");
                        break;
                    }
                    minhaPilha.Push(i);
                    minhaPilha.Push(j);
                    posicao.Linha--;
                    meuLab[posicao.Linha, posicao.Coluna] = 'R';
                }
                else if (minhaPilha.Count > 0)
                {
                    meuLab[i,j] = 'x';
                    
                    j = minhaPilha.Pop();
                    i = minhaPilha.Pop();
                    posicao.Linha = i;
                    posicao.Coluna = j;
                }
                else
                {
                    Console.WriteLine("Não foi possivel encontrar o queijo");
                    break;
                }
                System.Threading.Thread.Sleep(0);
                Console.Clear();
                mostrarLabirinto(meuLab, limit, limit);
            } while (meuLab[posicao.Linha, posicao.Coluna] != 'Q');
            // encontrou
        }


        static void criaLabirinto(char[,] meuLab)
        {
            Random random = new Random();
            for (int i = 0; i < limit; i++)
            {
                for (int j = 0; j < limit; j++)
                {
                    meuLab[i, j] = random.Next(4) == 1 ? '|' : '.';
                }
            }


            for (int i = 0; i < limit; i++)
            {
                meuLab[0, i] = '*';
                meuLab[limit - 1, i] = '*';
                meuLab[i, 0] = '*';
                meuLab[i, limit - 1] = '*';
            }


            int x = random.Next(limit);
            int y = random.Next(limit);
            meuLab[x, y] = 'Q';
        }


        static void Main(string[] args)
        {
            Posicao position = new Posicao(1, 1);
            char[,] labirinto = new char[limit, limit];
            criaLabirinto(labirinto);
            mostrarLabirinto(labirinto, limit, limit);
            buscarQueijo(labirinto, position);
            Console.ReadKey();
        }
    }
}
