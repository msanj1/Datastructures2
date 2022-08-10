using System;
using System.Collections.Generic;

namespace Phonebook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numberOfInput = int.Parse(Console.ReadLine());
            var phonebook = new Phonebook();
            var messages = new List<string>();

            for(int i= 0; i < numberOfInput; i++)
            {
                var input = Console.ReadLine().Split(' ');
                var query = input[0];
                var number = int.Parse(input[1]);

                if(query == "add")
                {
                    var name = input[2];
                    phonebook.Add(new Contact()
                    {
                        Name= name,
                        Number= number
                    });
                }
                if(query == "find")
                {
                    var contact = phonebook.Find(number);

                    if(contact == null)
                    {
                        messages.Add("not found");
                    }
                    else
                    {
                        messages.Add(contact.Name);
                    }
                }
                if (query == "del")
                {
                    phonebook.Delete(number);
                }
            }

            for(int i = 0; i < messages.Count; i++)
            {
                Console.WriteLine(messages[i]);
            }
        }
    }
}
