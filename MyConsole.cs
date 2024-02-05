/*
 * Pavel Dev
 * GitHub: PavelDev91
 * E-mail: PavelDev1991@gmail.com
 */

using System;
using System.Globalization;

namespace _MyConsole
{
    class MyConsole
    {
        //-----------------------------------------------------
        private struct _MyConsole_Position
        {
            public int L;
            public int T;

            public int Width;
            public int Height;
        }
        //-----------------------------------------------------
        private _MyConsole_Position MyConsole_Position;

        private char?[,] WorkArray;
        private char?[,] BufferArray;
        //-----------------------------------------------------
        public MyConsole(int L, int T, int Width, int Height)
        {
            MyConsole_Position = new _MyConsole_Position();

            MyConsole_Position.L = L;
            MyConsole_Position.T = T;

            MyConsole_Position.Width = Width;
            MyConsole_Position.Height = Height;

            WorkArray = new char?[Width, Height];
            BufferArray = new char?[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    WorkArray[x, y] = ' ';
                    BufferArray[x, y] = ' ';
                }
            }
        }
        //-----------------------------------------------------
        public void Draw()
        {
            for (int y = 0; y < MyConsole_Position.Height; y++)
            {
                for (int x = 0; x < MyConsole_Position.Width - 1; x++)
                {
                    if (WorkArray[x, y] == ' ' && WorkArray[x, y] != BufferArray[x, y])
                    {
                        Console.SetCursorPosition(MyConsole_Position.L + x, MyConsole_Position.T + y);
                        Console.Write(' ');

                        BufferArray[x, y] = ' ';

                        continue;
                    }

                    if (WorkArray[x, y] == BufferArray[x, y])
                    {
                        continue;
                    }

                    BufferArray[x, y] = WorkArray[x, y];

                    Console.SetCursorPosition(MyConsole_Position.L + x, MyConsole_Position.T + y);
                    Console.Write(WorkArray[x, y]);
                }
            }
        }
        //-----------------------------------------------------
        //-----------------------------------------------------
        public void Write(int L, int T, string Value)
        {
            for (int i = 0; i < Value.Length; i++)
            {
                if (L + i > MyConsole_Position.Width - 1 | T > MyConsole_Position.Height - 1)
                {
                    break;
                }

                WorkArray[L + i, T] = Value[i];
            }
        }
        //-----------------------------------------------------
        public string WriteRead(int L, int T, string Mask, int MaxLength, string DefaultValue = "")
        {
            string res = DefaultValue;

            ConsoleKeyInfo PressKey;

            Console.CursorVisible = false;

            string buf;

            while (true)
            {
                //---------------------------------------------
                if (res.Length <= MaxLength && res.Length > MyConsole_Position.Width - (L + 4))
                {
                    ClearLine(T);

                    buf = String_CopyLastNChars(res, res.Length % (MyConsole_Position.Width - (L + 4)));
                }
                else
                {
                    buf = String_CopyLastNChars(res, res.Length);
                }

                LineSame(L, T, buf);

                Draw();

                Console.SetCursorPosition(L + buf.Length, T);
                Console.CursorVisible = true;
                //---------------------------------------------

                PressKey = Console.ReadKey(true);

                for (int i = 0; i < Mask.Length; i++)
                {
                    if (res.Length == MaxLength)
                    {
                        break;
                    }

                    if (CharUnicodeInfo.GetUnicodeCategory(PressKey.KeyChar) == CharUnicodeInfo.GetUnicodeCategory(Mask[i]))
                    {
                        res += PressKey.KeyChar;

                        break;
                    }
                }

                if (PressKey.Key == ConsoleKey.Backspace)
                {
                    if (res.Length > 0)
                    {
                        res = res.Substring(0, res.Length - 1);
                    }
                }

                if (PressKey.Key == ConsoleKey.Enter)
                {
                    break;
                }

                if (PressKey.Key == ConsoleKey.Escape)
                {
                    return "";
                }
            }

            return res;
        }
        //-----------------------------------------------------
        public string String_CopyLastNChars(string Value, int NChars)
        {
            string res = "";

            if (NChars > Value.Length)
            {
                NChars = Value.Length;
            }

            for (int i = Value.Length - NChars; i < Value.Length; i++)
            {
                res += Value[i];
            }

            return res;
        }
        //-----------------------------------------------------
        public void LineSame(int L, int LineIndex, string Value)
        {
            if (LineIndex >= MyConsole_Position.Height - 1)
            {
                return;
            }

            for (int i = 0; i < MyConsole_Position.Width; i++)
            {
                WorkArray[i, LineIndex] = ' ';
            }

            for (int i = 0; i < Value.Length; i++)
            {
                WorkArray[L + i, LineIndex] = Value[i];
            }
        }
        //-----------------------------------------------------
        public int GetWidth()
        {
            return MyConsole_Position.Width;
        }
        //-----------------------------------------------------
        public int GetHeight()
        {
            return MyConsole_Position.Height;
        }
        //-----------------------------------------------------
        public int GetLineCount()
        {
            int res = 0;

            for (int y = 0; y < MyConsole_Position.Height; y++)
            {
                for (int x = 0; x < MyConsole_Position.Width; x++)
                {
                    if (WorkArray[x, y] != ' ')
                    {
                        res = y + 1;

                        break;
                    }
                }
            }

            return res;
        }
        //-----------------------------------------------------
        public void ClearLine(int LineIndex)
        {
            for (int x = 0; x < MyConsole_Position.Width; x++)
            {
                WorkArray[x, LineIndex] = ' ';
            }
        }
        //-----------------------------------------------------
        public void Clear()
        {
            for (int x = 0; x < MyConsole_Position.Width; x++)
            {
                for (int y = 0; y < MyConsole_Position.Height; y++)
                {
                    WorkArray[x, y] = ' ';
                }
            }
        }
        //-----------------------------------------------------
    }
}
