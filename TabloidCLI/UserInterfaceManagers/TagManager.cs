using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;


namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;
        private string _connectionString;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
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
            List<Tag> tags = _tagRepository.GetAll();
            foreach (Tag tag in tags)
            { 
                Console.WriteLine(tag);
            }
        }

        private void Add()
        {
            Console.WriteLine("New Tag");
            Tag tag = new Tag();


            Console.Write("Add a tag name: ");
            tag.Name = Console.ReadLine();


            _tagRepository.Insert(tag);
            Console.WriteLine();
            Console.WriteLine($"{tag.Name} is added!");
            Console.WriteLine();

        }


        private Tag Select(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Choose a tag please.";
            }

            Console.WriteLine(prompt);

            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++) 
                {
                Tag tag = tags[i];
                Console.WriteLine($"{i + 1} {tag.Name}");

                }
            Console.Write("> ");

            string userInput = Console.ReadLine();
            try
            {
                int selection = int.Parse(userInput);
                return tags[selection - 1];
            }
            catch (Exception)
            {
                Console.WriteLine("Not a valid selection!");
                return null;
            }

        }


        private void Edit()
        {
            Tag tagToEdit = Select("Which tag do you want to edit?");
            if (tagToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Enter new tag name: ");
            string tagName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(tagName))
            {
                tagToEdit.Name = tagName;
            }

            _tagRepository.Update(tagToEdit);
            Console.WriteLine();
            Console.WriteLine($"Tag has been updated to {tagToEdit.Name}");
            Console.WriteLine();
        }

        private void Remove()
        {
            Tag tagToDelete = Select("Which tag do you want to remove?");
            if (tagToDelete != null)
            {
                _tagRepository.Delete(tagToDelete.Id);
                Console.WriteLine();
                Console.WriteLine($"{tagToDelete.Name} has been removed.");
                Console.WriteLine();
            }
        }
    }
}
