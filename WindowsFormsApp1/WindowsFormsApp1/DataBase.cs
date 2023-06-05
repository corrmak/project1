using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }

    public class DatabaseManager
    {
        private string connectionString;

        public DatabaseManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void SaveContacts(List<Contact> contacts)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (Contact contact in contacts)
                {
                    string query = "INSERT INTO Person (Contact_id, FirstName, LastName, Email, City, PhoneNumber) " +
                                   "VALUES (@Contact_id, @FirstName, @LastName, @Email, @City, @PhoneNumber)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Contact_id", contact.ContactId);
                        command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                        command.Parameters.AddWithValue("@LastName", contact.LastName);
                        command.Parameters.AddWithValue("@Email", contact.Email);
                        command.Parameters.AddWithValue("@City", contact.City);
                        command.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<Contact> LoadContacts()
        {
            List<Contact> contacts = new List<Contact>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Contact_id, FirstName, LastName, PhoneNumber, Email, City FROM Person";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contact contact = new Contact
                            {
                                ContactId = Convert.ToInt32(reader["Contact_id"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                PhoneNumber = Convert.ToInt32(reader["PhoneNumber"]),
                                Email = reader["Email"].ToString(),
                                City = reader["City"].ToString()
                            };

                            contacts.Add(contact);
                        }
                    }
                }
            }

            return contacts;
        }
    }
}
