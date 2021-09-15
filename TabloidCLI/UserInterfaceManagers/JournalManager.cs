using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journals Entry");
            Console.WriteLine(" 2) Add A Journal Entry");
            Console.WriteLine(" 3) Edit A Journal Entry");
            Console.WriteLine(" 4) Remove A Journal Entry");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Journal> journals = _journalRepository.GetAll();
            foreach (Journal j in journals)
            {
                Console.WriteLine(j);
            }    
        }

        private void Add()
        {
            Console.WriteLine("New Journal Entry");
            Journal journal = new Journal();


            Console.WriteLine("Entry Title: ");
            journal.Title = Console.ReadLine();


            Console.Write("Body: ");
            journal.Content = Console.ReadLine();


            journal.CreateDateTime = DateTime.Now;

            _journalRepository.Insert(journal);
;        }


        private Journal Select(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please select a journal entry!";
            }

            Console.WriteLine(prompt);

            List<Journal> journals = _journalRepository.GetAll();

            for (int i = 0; i < journals.Count; i++)
            {
                Journal journal = journals[i];
                Console.WriteLine($"{i + 1} {journal.Title}");
            }

            Console.Write("> ");

            string userInput = Console.ReadLine();
            try
            {
                int selection = int.Parse(userInput);
                return journals[selection - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Not a valid selection!");
                return null;
            }
        }


        private void Edit()
        {
            Journal journal = Select("Which journal entry do you want to edit?");
            if (journal == null)
            {
                return;
            }
            Console.WriteLine();
            Console.Write("What do you want to change the title to?");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                journal.Title = title;
            }

            Console.Write("What would you like to change the content to?");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                journal.Content = content;
            }

            journal.CreateDateTime = DateTime.Now;

            _journalRepository.Update(journal);


        }





        private void Remove()
        {
            Journal journalToDelete = Select("Which journal entry do you want to delete?");
            if (journalToDelete != null)
            {
                _journalRepository.Delete(journalToDelete.Id);
                Console.WriteLine("Journal entry has been removed!");
            }
        }
    }
}
