using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write(">");
            string choice = Console.ReadLine();
            switch(choice)
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
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine(post);
            }
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.WriteLine("Title: ");
            post.Title = Console.ReadLine();

            Console.WriteLine("Url: ");
            post.Url = Console.ReadLine();

            Console.WriteLine("Date:(DD/MM/YYYY)");
            DateTime publishDate = DateTime.Parse(Console.ReadLine());
            post.PublishDateTime = publishDate;

            Console.Write("Author:");
            ListAuthors();

            int selectedAuthor = int.Parse(Console.ReadLine());
            post.Author = _authorRepository.Get(selectedAuthor);

            Console.Write("Blog:");
            ListBlogs();

            int selectedBlog = int.Parse(Console.ReadLine());
            post.Blog = _blogRepository.Get(selectedBlog);

        }

        private void ListAuthors()
        {
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors)
            {
                Console.WriteLine($"{author.Id} {author}");
            }
        }

        private void ListBlogs()
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Id} {blog}");
            }
        }
        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title(blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }

            Console.Write("New Url (blank to leave unchanged): ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }

            Console.Write("New Publication Date (DD/MM/YYYY) (blank to leave unchanged): ");
            var date = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(date))
            {
                postToEdit.PublishDateTime = DateTime.Parse(date);
            }

            Console.WriteLine("Update Author: ");
            List<Author> authors = _authorRepository.GetAll();
            for (int i = 0; i < authors.Count; i++)
            {
                Author updateAuthor = authors[i];
                Console.WriteLine($" {i + 1}) {updateAuthor.FullName}");
            }
            Console.Write("> ");
            var authorIndex = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(authorIndex))
            {
                postToEdit.Author = authors[int.Parse(authorIndex) - 1];
            }

            Console.WriteLine("Update Blog: ");
            List<Blog> blogs = _blogRepository.GetAll();
            for (int i = 0; i < blogs.Count; i++)
            {
                Blog updateBlog = blogs[i];
                Console.WriteLine($" {i + 1}) {updateBlog.ToString()}");
            }
            Console.Write("> ");
            var blogIndex = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(blogIndex))
            {
                postToEdit.Blog = blogs[int.Parse(blogIndex) - 1];
            }

            _postRepository.Update(postToEdit);
        }

        private void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
