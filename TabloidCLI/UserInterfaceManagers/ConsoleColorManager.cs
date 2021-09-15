using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class ConsoleColorManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;

        public ConsoleColorManager(IUserInterfaceManager parentUI)
        {
            _parentUI = parentUI;

        }
        public IUserInterfaceManager Execute()
        {

            Console.WriteLine("Choose a New Console Color");
            Console.WriteLine(" 1) White");
            Console.WriteLine(" 2) Yellow");
            Console.WriteLine(" 3) Magenta");
            Console.WriteLine(" 4) Cyan");
            Console.WriteLine(" 5) Gray");
            Console.WriteLine(" 6) Original Console Color");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ToWhite();
                    return this;
                case "2":
                    ToYellow();
                    return this;
                case "3":
                    ToMagenta();
                    return this;
                case "4":
                    ToCyan();
                    return this;
                case "5":
                    ToGray();
                    return this;
                case "6":
                    ToBlack();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
            private void ToWhite()
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
            }

            private void ToYellow()
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
            }

            private void ToMagenta()
            {
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
            }

            private void ToCyan()
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
            }

            private void ToGray()
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Clear();
            }

        private void ToBlack()
            {
            //throw new NotImplementedException();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }
    }
}
