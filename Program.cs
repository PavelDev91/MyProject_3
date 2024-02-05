/*
 * Pavel Dev
 * GitHub: PavelDev91
 * E-mail: PavelDev1991@gmail.com
 */

using System;
using System.Diagnostics;
using System.Globalization;
using _MyConsole;

namespace MyProject_3
{
    internal class Program
    {
        struct _FindResult
        {
            public string Value;
            public int Count;

            public int XData;
        }
        static void Main(string[] args)
        {
            //-------------------------------------------------
            Console.SetWindowSize(128, 44);
            Console.SetBufferSize(128, 44);

            Console.SetWindowPosition(0, 0);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.CursorVisible = false;

            Console.Title = "GitHub: PavelDev91";
            //-------------------------------------------------

            #region Задание 1

            MyConsole WorkConsole = new MyConsole(0, 0, Console.WindowWidth, Console.WindowHeight);

            ConsoleKey PressKey;

            char MenuPos = 'M';

            string WorkString = "";

            string[] WorkString_Array = null;
            //-----------------------------------------------------------------------------------------------------------------
            void Menu()
            {
                WorkConsole.Clear();

                WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));
                WorkConsole.Write((WorkConsole.GetWidth() - "Работа с текстом".Length) / 2, 1, "Работа с текстом");
                WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));

                WorkConsole.Draw();
            }
            //-----------------------------------------------------------------------------------------------------------------
            void FindResultArray_AddLen(ref _FindResult[] FindResult)
            {
                if (FindResult == null)
                {
                    FindResult = new _FindResult[1];
                }
                else
                {
                    _FindResult[] res = new _FindResult[FindResult.Length + 1];

                    for (int i = 0; i < FindResult.Length; i++)
                    {
                        res[i].Value = FindResult[i].Value;
                        res[i].Count = FindResult[i].Count;
                        res[i].XData = FindResult[i].XData;
                    }

                    FindResult = res;
                }
            }
            //-------------------------------------------------
            void StringSame(ref string WString, string Value1, string Value2, bool OneStep = false)
            {
                string buf;

                int x = 0;

                string mask = " .,!?()<>'\":";

                int f_i;
                int l_i;
                bool f_status;
                bool l_status;

                while(true)
                {
                    if (x + Value1.Length > WString.Length)
                    {
                        break;
                    }

                    //---------------------------------------------------------------------------------------------------------
                    buf = WString.Substring(x, Value1.Length);

                    f_status = false;
                    l_status = false;

                    f_i = 0;
                    l_i = 0;

                    if (buf == Value1)
                    {
                        if (x == 0)
                        {
                            f_i = 0;
                            f_status = true;
                        }

                        for (int i = 0; i < mask.Length; i++)
                        {
                            if (f_status == true)
                            {
                                break;
                            }    

                            if (CharUnicodeInfo.GetUnicodeCategory(WString[x - 1]) == CharUnicodeInfo.GetUnicodeCategory(mask[i]))
                            {
                                f_i = x;
                                f_status = true;

                                break;
                            }
                        }

                        if (x + Value1.Length == WString.Length)
                        {
                            l_i = x + Value1.Length;
                            l_status = true;
                        }

                        for (int i = 0; i < mask.Length; i++)
                        {
                            if (l_status == true)
                            {
                                break;
                            }

                            if (CharUnicodeInfo.GetUnicodeCategory(WString[f_i + Value1.Length]) == CharUnicodeInfo.GetUnicodeCategory(mask[i]))
                            {
                                l_i = f_i + Value1.Length;
                                l_status = true;

                                break;
                            }
                        }
                    }

                    //-----------------------------------------
                    if (f_status == true && l_status == true)
                    {
                        buf = WString.Substring(0, f_i) + Value2;
                        buf += WString.Substring(l_i);

                        WString = buf;

                        if (OneStep == true)
                        {
                            return;
                        }
                    }

                    x++;
                }
            }
            //-------------------------------------------------
            void Find_StrMaxD(string WString, ref _FindResult[] FindRes)
            {
                _FindResult[] FindResult = null;

                _FindResult[] buf = null;

                while(true)
                {
                    Find_LongStr(WString, ref buf);

                    if (buf == null)
                    {
                        break;
                    }

                    for (int i = 0; i < buf.GetLength(0); i++)
                    {
                        FindResultArray_AddLen(ref FindResult);

                        FindResult[FindResult.GetLength(0) - 1].Value = buf[i].Value;
                        FindResult[FindResult.GetLength(0) - 1].Count = buf[i].Count;
                    }

                    for (int i = 0; i < FindResult.GetLength(0); i++)
                    {
                        StringSame(ref WString, FindResult[i].Value, " ");
                    }
                }
                //---------------------------------------------
                int dc;

                for (int f = 0; f < FindResult.GetLength(0); f++)
                {
                    dc = 0;

                    for (int i = 0; i < FindResult[f].Value.Length; i++)
                    {
                        if (char.IsDigit(FindResult[f].Value[i]) == true)
                        {
                            dc++;
                        }
                    }

                    FindResult[f].XData = dc;
                }
                //---------------------------------------------

                buf = FindResult;

                FindResult = null;

                for (int f = 0; f < buf.GetLength(0); f++)
                {
                    if (buf[f].XData == 0)
                    {
                        continue;
                    }

                    FindResultArray_AddLen(ref FindResult);
                    FindResult[FindResult.GetLength(0) - 1].Value = buf[f].Value;
                    FindResult[FindResult.GetLength(0) - 1].Count = buf[f].Count;
                    FindResult[FindResult.GetLength(0) - 1].XData = buf[f].XData;
                }

                if (FindResult == null)
                {
                    FindRes = null;

                    return;
                }
                //---------------------------------------------
                dc = 0;
                for (int f = 0; f < FindResult.GetLength(0); f++)
                {
                    if (FindResult[f].XData > FindResult[dc].XData)
                    {
                        dc = f;
                    }
                }
                //---------------------------------------------

                buf = FindResult;

                FindResult = null;

                FindResultArray_AddLen(ref FindResult);
                FindResult[FindResult.GetLength(0) - 1].Value = buf[dc].Value;
                FindResult[FindResult.GetLength(0) - 1].Count = buf[dc].Count;
                FindResult[FindResult.GetLength(0) - 1].XData = buf[dc].XData;

                for (int f = 0; f < buf.GetLength(0); f++)
                {
                    if (buf[f].Value != FindResult[0].Value)
                    {
                        if (buf[f].XData == FindResult[0].XData)
                        {
                            FindResultArray_AddLen(ref FindResult);
                            FindResult[FindResult.GetLength(0) - 1].Value = buf[f].Value;
                            FindResult[FindResult.GetLength(0) - 1].Count = buf[f].Count;
                            FindResult[FindResult.GetLength(0) - 1].XData = buf[f].XData;
                        }
                    }
                }
                //---------------------------------------------

                FindRes = FindResult;
            }
            //-------------------------------------------------
            void Find_LongStr(string WString, ref _FindResult[] FindRes)
            {
                string mask = " .,!?()<>'\":";

                string[] s = new string[WString.Length];

                for (int ii = 0; ii < WString.Length; ii++)
                {
                    for (int m = 0; m < mask.Length; m++)
                    {
                        s[ii] = WString[ii].ToString();
                        if (CharUnicodeInfo.GetUnicodeCategory(WString[ii]) == CharUnicodeInfo.GetUnicodeCategory(mask[m]))
                        {
                            s[ii] = " ";

                            break;
                        }
                    }
                }

                WString = "";
                for (int ii = 0; ii < s.GetLength(0); ii++)
                {
                    WString += s[ii];
                }

                string[] array = null;

                int i = 0;

                int x = 0;
                int xc = -1;
                while(true)
                {
                    if (i >= WString.Length)
                    {
                        break;
                    }

                    if (i == WString.Length - 1 && x > -1)
                    {
                        xc = i + 1;
                    }

                    if (WString[i] == ' ')
                    {
                        if (x == -1)
                        {
                            x = i + 1;
                        }
                        else
                        {
                            xc = i;
                        }
                    }

                    if (x != -1 && xc != -1)
                    {
                        Array_AddString(ref array, WString.Substring(x, xc - x));

                        WString = WString.Substring(xc, WString.Length - xc);

                        i = -1;

                        x = -1;
                        xc = -1;
                    }

                    i++;
                }

                //---------------------------------------------
                _FindResult[] FindResult = null;
                //---------------------------------------------
                x = 0;
                for (int z = 0; z < array.GetLength(0); z++)
                {
                    if (array[z].Length > array[x].Length)
                    {
                        x = z;
                    }
                }

                if (array[x].Length == 0)
                {

                    FindRes = null;

                    return;
                }

                FindResultArray_AddLen(ref FindResult);

                FindResult[FindResult.Length - 1].Value = array[x];
                FindResult[FindResult.Length - 1].Count = 0;

                for (int z = 0; z < array.GetLength(0); z++)
                {
                    if (array[z].Length == array[x].Length)
                    {
                        i = z;
                        for (int f = 0; f < FindResult.GetLength(0); f++)
                        {
                            if (FindResult[f].Value == array[z])
                            {
                                i = -1;

                                break;
                            }
                        }

                        if (i == z)
                        {
                            FindResultArray_AddLen(ref FindResult);

                            FindResult[FindResult.Length - 1].Value = array[i];
                            FindResult[FindResult.Length - 1].Count = 0;
                        }
                    }
                }
                //---------------------------------------------
                for (int f = 0; f < FindResult.GetLength(0); f++)
                {
                    for (int z = 0; z < array.Length; z++)
                    {
                        if (array[z] == FindResult[f].Value)
                        {
                            FindResult[f].Count++;
                        }
                    }
                }
                //---------------------------------------------

                FindRes = FindResult;
            }
            //-------------------------------------------------
            void Array_AddString(ref string[] Array, string Value)
            {
                string[] res;

                if (Array == null)
                {
                    res = new string[1];

                    res[0] = Value;
                }
                else
                {
                    res = new string[Array.GetLength(0) + 1];

                    for (int i = 0; i < Array.GetLength(0); i++)
                    {
                        res[i] = Array[i];
                    }

                    res[res.GetLength(0) - 1] = Value;
                }

                Array = res;
            }
            //-------------------------------------------------
            void Create_WorkArray()
            {
                int x = 0;
                int xc;

                string buf;
                while (true)
                {
                    if (x >= WorkString.Length)
                    {
                        break;
                    }

                    if (WorkString.Length - x > WorkConsole.GetWidth() - 4)
                    {
                        xc = WorkConsole.GetWidth() - 4;
                    }
                    else
                    {
                        xc = WorkString.Length - x;
                    }

                    buf = WorkString.Substring(x, xc);

                    Array_AddString(ref WorkString_Array, buf);

                    x += xc;
                }
            }
            //-----------------------------------------------------------------------------------------------------------------

            Menu();

            while (true)
            {
                bool ws = false;
                //---------------------------------------------
                if (MenuPos == 'M')
                {
                    Menu();

                    WorkConsole.Write(2, WorkConsole.GetLineCount(), "| Введите текст: (Max Length: " + ((WorkConsole.GetWidth() - 4) * 10) + ")");
                    WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));
                    WorkConsole.Write(2, WorkConsole.GetLineCount(), "|");
                    WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));
                    WorkConsole.Write(2, WorkConsole.GetLineCount(), "| Выход | Press Key: '" + ConsoleKey.Escape.ToString() + "'");
                    WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));

                    WorkConsole.Draw();

                    while (true)
                    {
                        WorkString = WorkConsole.WriteRead(4, WorkConsole.GetLineCount() - 4, "Az 09 =+-*/ `~_ !? ;: @#$%& []{}<>^ /\\|()",
                                                           (WorkConsole.GetWidth() - 4) * 10);

                        Console.CursorVisible = false;
                        if (WorkString.Length > 0)
                        {
                            Create_WorkArray();

                            MenuPos = 'E';

                            Menu();

                            break;
                        }

                        if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                        {
                            return;
                        }
                    }
                }
                //---------------------------------------------
                if (MenuPos == 'E')
                {
                    Console.CursorVisible = false;

                    WorkConsole.Write(2, (int)WorkConsole.GetLineCount(), "| Введенный текст:");
                    WorkConsole.Write(2, (int)WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));


                    for (int i = 0; i < WorkString_Array.GetLength(0); i++)
                    {
                        WorkConsole.Write(2, WorkConsole.GetLineCount(), WorkString_Array[i]);
                    }

                    WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));
                    WorkConsole.Write(2, WorkConsole.GetLineCount(), "| * Заменить цифры от 0 до 9 на слова «ноль» .... «девять».                       | Press Key: '" +
                                                                      ConsoleKey.S.ToString() + "'");
                    WorkConsole.Write(2, WorkConsole.GetLineCount(), "| * Найти слова, содержащие максимальное количество цифр.                         | Press Key: '" +
                                                                      ConsoleKey.M.ToString() + "'");
                    WorkConsole.Write(2, WorkConsole.GetLineCount(), "| * Найти самое длинное слово и определить, сколько раз оно встретилось в тексте. | Press Key: '" +
                                                                      ConsoleKey.L.ToString() + "'");

                    WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));
                    WorkConsole.Write(2, WorkConsole.GetLineCount(), "| Ввести текст | Press Key: '" + ConsoleKey.W.ToString() + "'");
                    WorkConsole.Write(2, WorkConsole.GetLineCount(), "| Выход        | Press Key: '" + ConsoleKey.Escape.ToString() + "'");
                    WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));

                    WorkConsole.Draw();

                    while (true)
                    {
                        PressKey = Console.ReadKey(true).Key;

                        //-------------------------------------
                        if (PressKey == ConsoleKey.S)
                        {
                            StringSame(ref WorkString, "0", "ноль");
                            StringSame(ref WorkString, "1", "один");
                            StringSame(ref WorkString, "2", "два");
                            StringSame(ref WorkString, "3", "три");
                            StringSame(ref WorkString, "4", "четыре");
                            StringSame(ref WorkString, "5", "пять");
                            StringSame(ref WorkString, "6", "шесть");
                            StringSame(ref WorkString, "7", "семь");
                            StringSame(ref WorkString, "8", "восемь");
                            StringSame(ref WorkString, "9", "девять");

                            WorkString_Array = null;

                            Create_WorkArray();

                            Menu();

                            MenuPos = 'E';

                            ws = true;

                            break;
                        }
                        //-------------------------------------
                        if (PressKey == ConsoleKey.M)
                        {
                            int C;

                            _FindResult[] FindResult = null;

                            Find_StrMaxD(WorkString, ref FindResult);

                            if (FindResult == null)
                            {
                                WorkConsole.Write(2, WorkConsole.GetLineCount(), "| Ошибка: Слова содержащие цифры НЕ НАЙДЕНЫ!");
                                WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));
                                WorkConsole.Write(2, WorkConsole.GetLineCount(), "| Press any Key!");
                                WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));

                                WorkConsole.Draw();

                                Console.ReadKey(true);

                                int x1 = WorkConsole.GetLineCount();
                                for (int i = 0; i < 5; i++)
                                {
                                    WorkConsole.ClearLine(x1 - i);
                                }

                                WorkConsole.Draw();

                                continue;
                            }

                            WorkConsole.Write(2, WorkConsole.GetLineCount(), "| * Слова, содержащие максимальное количество цифр:");
                            WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));

                            for (int i = 0; i < FindResult.GetLength(0); i++)
                            {
                                WorkConsole.Write(2, WorkConsole.GetLineCount(), "| - Слово: " + FindResult[i].Value);
                                WorkConsole.Write(2, WorkConsole.GetLineCount(), "| - Встретилось (кол-во раз): " + FindResult[i].Count);
                                WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));
                            }

                            WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));
                            WorkConsole.Write(2, WorkConsole.GetLineCount(), "| Press any Key!");
                            WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));

                            WorkConsole.Draw();

                            Console.ReadKey(true);

                            C = 5 + (FindResult.GetLength(0) * 3) + 1;
                            int x = WorkConsole.GetLineCount();
                            for (int i = 0; i < C; i++)
                            {
                                WorkConsole.ClearLine(x - i);
                            }

                            WorkConsole.Draw();
                        }
                        //-------------------------------------
                        if (PressKey == ConsoleKey.L)
                        {
                            int C;

                            _FindResult[] FindResult = null;

                            Find_LongStr(WorkString, ref FindResult);

                            WorkConsole.Write(2, WorkConsole.GetLineCount(), "| * Самые длинные слова:");
                            WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));

                            for (int i = 0; i < FindResult.GetLength(0); i++)
                            {
                                WorkConsole.Write(2, WorkConsole.GetLineCount(), "| - Слово: " + FindResult[i].Value);
                                WorkConsole.Write(2, WorkConsole.GetLineCount(), "| - Встретилось (кол-во раз): " + FindResult[i].Count);
                                WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));
                            }

                            WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));
                            WorkConsole.Write(2, WorkConsole.GetLineCount(), "| Press any Key!");
                            WorkConsole.Write(2, WorkConsole.GetLineCount(), new string('-', WorkConsole.GetWidth() - 4));

                            WorkConsole.Draw();

                            Console.ReadKey(true);

                            C = 5 + (FindResult.GetLength(0) * 3) + 1;
                            int x = WorkConsole.GetLineCount();
                            for (int i = 0; i < C; i++)
                            {
                                WorkConsole.ClearLine(x - i);
                            }

                            WorkConsole.Draw();
                        }
                        //-------------------------------------
                        if (PressKey == ConsoleKey.W)
                        {
                            WorkString_Array = null;

                            MenuPos = 'M';
                            Menu();

                            ws = true;

                            break;
                        }
                        //-------------------------------------
                        if (PressKey == ConsoleKey.Escape)
                        {
                            return;
                        }
                        //-------------------------------------
                    }
                }
                //--------------------------------------------
                if (ws == true)
                {
                    continue;
                }
                //---------------------------------------------
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    return;
                }
                //---------------------------------------------
            }

            #endregion

            //-------------------------------------------------
        }
    }
}
