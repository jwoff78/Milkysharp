﻿using System;
using System.IO;
using Milkysharp.Core;

namespace Milkysharp.Applications
{
    class MIVMain
    {
        public static void printMIVStartScreen()
        {
            System.Console.Clear();
            System.Console.WriteLine("~");
            System.Console.WriteLine("~");
            System.Console.WriteLine("~");
            System.Console.WriteLine("~");
            System.Console.WriteLine("~");
            System.Console.WriteLine("~");
            System.Console.WriteLine("~");
            System.Console.WriteLine("~                               MIV - MInimalistic Vi");
            System.Console.WriteLine("~");
            System.Console.WriteLine("~                                  version 1.2");
            System.Console.WriteLine("~                             by Denis Bartashevich");
            System.Console.WriteLine("~                            Minor additions by CaveSponge");
            System.Console.WriteLine("~                    MIV is open source and freely distributable");
            System.Console.WriteLine("~");
            System.Console.WriteLine("~                     type :help<Enter>          for information");
            System.Console.WriteLine("~                     type :q<Enter>             to exit");
            System.Console.WriteLine("~                     type :wq<Enter>            save to file and exit");
            System.Console.WriteLine("~                     press i                    to write");
            System.Console.WriteLine("~");
            System.Console.WriteLine("~");
            System.Console.WriteLine("~");
            System.Console.WriteLine("~");
            System.Console.WriteLine("~");
            System.Console.WriteLine("~");
            System.Console.Write("~");
        }

        public static String stringCopy(String value)
        {
            String newString = String.Empty;

            for (int i = 0; i < value.Length - 1; i++)
            {
                newString += value[i];
            }

            return newString;
        }

        public static void printMIVScreen(char[] chars, int pos, String infoBar, Boolean editMode)
        {
            int countNewLine = 0;
            int countChars = 0;
            delay(10000000);
            System.Console.Clear();

            for (int i = 0; i < pos; i++)
            {
                if (chars[i] == '\n')
                {
                    System.Console.WriteLine("");
                    countNewLine++;
                    countChars = 0;
                }
                else
                {
                    System.Console.Write(chars[i]);
                    countChars++;
                    if (countChars % 80 == 79)
                    {
                        countNewLine++;
                    }
                }
            }

            System.Console.Write("/");

            for (int i = 0; i < 23 - countNewLine; i++)
            {
                System.Console.WriteLine("");
                System.Console.Write("~");
            }

            //PRINT INSTRUCTION
            System.Console.WriteLine();
            for (int i = 0; i < 72; i++)
            {
                if (i < infoBar.Length)
                {
                    System.Console.Write(infoBar[i]);
                }
                else
                {
                    System.Console.Write(" ");
                }
            }

            if (editMode)
            {
                System.Console.Write(countNewLine + 1 + "," + countChars);
            }

        }

        public static String miv(String start)
        {
            Boolean editMode = false;
            int pos = 0;
            char[] chars = new char[2000];
            String infoBar = String.Empty;

            if (start == null)
            {
                printMIVStartScreen();
            }
            else
            {
                pos = start.Length;

                for (int i = 0; i < start.Length; i++)
                {
                    chars[i] = start[i];
                }
                printMIVScreen(chars, pos, infoBar, editMode);
            }

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = System.Console.ReadKey(true);

                if (isForbiddenKey(keyInfo.Key)) continue;

                else if (!editMode && keyInfo.KeyChar == ':')
                {
                    infoBar = ":";
                    printMIVScreen(chars, pos, infoBar, editMode);
                    do
                    {
                        keyInfo = System.Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            if (infoBar == ":wq")
                            {
                                String returnString = String.Empty;
                                for (int i = 0; i < pos; i++)
                                {
                                    returnString += chars[i];
                                }
                                return returnString;
                            }
                            else if (infoBar == ":q")
                            {
                                return null;

                            }
                            else if (infoBar == ":help")
                            {
                                printMIVStartScreen();
                                break;
                            }
                            else
                            {
                                infoBar = "ERROR: No such command";
                                printMIVScreen(chars, pos, infoBar, editMode);
                                break;
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.Backspace)
                        {
                            infoBar = stringCopy(infoBar);
                            printMIVScreen(chars, pos, infoBar, editMode);
                        }
                        else if (keyInfo.KeyChar == 'q')
                        {
                            infoBar += "q";
                        }
                        else if (keyInfo.KeyChar == ':')
                        {
                            infoBar += ":";
                        }
                        else if (keyInfo.KeyChar == 'w')
                        {
                            infoBar += "w";
                        }
                        else if (keyInfo.KeyChar == 'h')
                        {
                            infoBar += "h";
                        }
                        else if (keyInfo.KeyChar == 'e')
                        {
                            infoBar += "e";
                        }
                        else if (keyInfo.KeyChar == 'l')
                        {
                            infoBar += "l";
                        }
                        else if (keyInfo.KeyChar == 'p')
                        {
                            infoBar += "p";
                        }
                        else
                        {
                            continue;
                        }
                        printMIVScreen(chars, pos, infoBar, editMode);



                    } while (keyInfo.Key != ConsoleKey.Escape);
                }

                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    editMode = false;
                    infoBar = String.Empty;
                    printMIVScreen(chars, pos, infoBar, editMode);
                    continue;
                }

                else if (keyInfo.Key == ConsoleKey.I && !editMode)
                {
                    editMode = true;
                    infoBar = "-- INSERT --";
                    printMIVScreen(chars, pos, infoBar, editMode);
                    continue;
                }

                else if (keyInfo.Key == ConsoleKey.Enter && editMode && pos >= 0)
                {
                    chars[pos++] = '\n';
                    printMIVScreen(chars, pos, infoBar, editMode);
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && editMode && pos >= 0)
                {
                    if (pos > 0) pos--;

                    chars[pos] = '\0';

                    printMIVScreen(chars, pos, infoBar, editMode);
                    continue;
                }

                if (editMode && pos >= 0)
                {
                    chars[pos++] = keyInfo.KeyChar;
                    printMIVScreen(chars, pos, infoBar, editMode);
                }

            } while (true);
        }

        public static bool isForbiddenKey(ConsoleKey key)
        {
            ConsoleKey[] forbiddenKeys = { ConsoleKey.Print, ConsoleKey.PrintScreen, ConsoleKey.Pause, ConsoleKey.Home, ConsoleKey.PageUp, ConsoleKey.PageDown, ConsoleKey.End, ConsoleKey.NumPad0, ConsoleKey.NumPad1, ConsoleKey.NumPad2, ConsoleKey.NumPad3, ConsoleKey.NumPad4, ConsoleKey.NumPad5, ConsoleKey.NumPad6, ConsoleKey.NumPad7, ConsoleKey.NumPad8, ConsoleKey.NumPad9, ConsoleKey.Insert, ConsoleKey.F1, ConsoleKey.F2, ConsoleKey.F3, ConsoleKey.F4, ConsoleKey.F5, ConsoleKey.F6, ConsoleKey.F7, ConsoleKey.F8, ConsoleKey.F9, ConsoleKey.F10, ConsoleKey.F11, ConsoleKey.F12, ConsoleKey.Add, ConsoleKey.Divide, ConsoleKey.Multiply, ConsoleKey.Subtract, ConsoleKey.LeftWindows, ConsoleKey.RightWindows };
            for (int i = 0; i < forbiddenKeys.Length; i++)
            {
                if (key == forbiddenKeys[i]) return true;
            }
            return false;
        }

        public static void delay(int time)
        {
            for (int i = 0; i < time; i++) ;
        }
        public static void StartMIV(string specifiedFile)
        {
            try
            {
                if (File.Exists(Kernel.CurrentDirectory + specifiedFile))
                {
                    System.Console.WriteLine("Found file!");
                }
                else if (!File.Exists(Kernel.CurrentDirectory + specifiedFile))
                {
                    System.Console.WriteLine("Creating file!");
                    File.Create(Kernel.CurrentDirectory + specifiedFile);
                }
                System.Console.Clear();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            String text = String.Empty;
            System.Console.WriteLine("Do you want to open " + specifiedFile + " content? (Yes/No)");
            if (System.Console.ReadLine().ToLower() == "yes" || System.Console.ReadLine().ToLower() == "y")
            {
                text = miv(File.ReadAllText(Kernel.CurrentDirectory + specifiedFile));
            }
            else
            {
                text = miv(null);
            }

            System.Console.Clear();

            if (text != null)
            {
                File.WriteAllText(Kernel.CurrentDirectory + specifiedFile, text);
                System.Console.WriteLine("Content has been saved to " + specifiedFile);
            }
            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey(true);
        }
    }
}
