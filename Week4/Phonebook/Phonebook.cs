using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook
{
    public class Phonebook
    {
        public LinkedList<Contact>[] contacts;
        private int numberOfKeys = 0;
        private int cardinality = 1;
        private long a = 23;
        private long b = 1;
        private long p = 1000000007;
        public Phonebook()
        {
            contacts = InitialiseContacts(cardinality);
        }

        private LinkedList<Contact>[] InitialiseContacts(int cardinality)
        {
            var contacts = new LinkedList<Contact>[cardinality];
            for(int i=0; i< contacts.Length; i++)
            {
                contacts[i] = new LinkedList<Contact>();
            }

            return contacts;
        }

        public void Add(Contact newContact)
        {
            if (numberOfKeys / (double)contacts.Length > 0.9)
            {
                ReHash();
            }

            var existingContact = Find(newContact.Number);
            if (existingContact != null)
            {
                existingContact.Name = newContact.Name;
                return;
            }

            var newContactHash = Hash(newContact.Number);
            var chain = contacts[newContactHash];
            chain.AddLast(newContact);
            numberOfKeys++;
        }

        public void Delete(int number)
        {
            var contact = Find(number);
            if (contact == null)
                return;

            var hash = Hash(number);

            var chain = contacts[hash];
            chain.Remove(contact);
            numberOfKeys--;
        }

        public Contact Find(long number)
        {
            var hash = Hash(number);
            var chain = contacts[hash];

            var currentNode = chain.First;
            while(currentNode != null)
            {
                if(currentNode.Value.Number == number)
                {
                    break;
                }
                currentNode = currentNode.Next;
            }

            return currentNode?.Value;
        }

        private void ReHash()
        {
            //Random rnd = new Random();

            var newContacts = InitialiseContacts(contacts.Length * 2);
            cardinality = contacts.Length * 2;
            foreach (var contact in contacts)
            {
                var chain = contact;
                var currentNode = chain.First;
                while (currentNode != null)
                {
                    var hash = Hash(currentNode.Value.Number);

                    var newChain = newContacts[hash];
                    newChain.AddLast(currentNode.Value);

                    currentNode = currentNode.Next;
                }
            }

            contacts = newContacts;
        }

        private long Hash(long number)
        {
            return ((a * number + b) % p) % cardinality;
        }
    }
}
