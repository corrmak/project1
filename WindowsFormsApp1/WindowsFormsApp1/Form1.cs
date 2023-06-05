using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Contact> contacts;
        private const string connectionString = "Data Source=DESKTOP-ELT7709;Initial Catalog=Contacts;Integrated Security=True";
        private DatabaseManager dataBase;

        public Form1()
        {
            InitializeComponent();
            contacts = new List<Contact>();
            dataBase = new DatabaseManager(connectionString);

            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;

            Contact newcontact = new Contact();
            contacts.Add(newcontact);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Код обработки щелчка на ячейке (по вашей логике)
        }

        private void AddContactButton_Click(object sender, EventArgs e)
        {
            Contact newcontact = new Contact();
            contacts.Add(newcontact);
            LoadContactsToDataGridView(contacts);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            if (rowIndex >= 0 && rowIndex < contacts.Count)
            {
                DataGridViewCell cell = dataGridView1.Rows[rowIndex].Cells[columnIndex];

                if (cell.Value != null)
                {
                    string newValue = cell.Value.ToString();

                    if (contacts[rowIndex] != null)
                    {
                        switch (columnIndex)
                        {
                            case 0:
                                contacts[rowIndex].FirstName = newValue;
                                break;
                            case 1:
                                contacts[rowIndex].LastName = newValue;
                                break;
                            case 2:
                                int PhoneNumber;
                                if (int.TryParse(newValue, out PhoneNumber))
                                {
                                    contacts[rowIndex].PhoneNumber = PhoneNumber;
                                }
                                break;
                            case 3:
                                contacts[rowIndex].Email = newValue;
                                break;
                            case 4:
                                contacts[rowIndex].City = newValue;
                                break;
                            case 5:
                                int contactId;
                                if (int.TryParse(newValue, out contactId))
                                {
                                    contacts[rowIndex].ContactId = contactId;
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void DeleteContactButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;
                contacts.RemoveAt(rowIndex);
                LoadContactsToDataGridView(contacts);
            }
        }

        private void ClearListButton_Click(object sender, EventArgs e)
        {
            contacts.Clear();
            LoadContactsToDataGridView(contacts);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataBase.SaveContacts(contacts);
        }

        public void LoadContactsToDataGridView(List<Contact> contacts)
        {
            dataGridView1.Rows.Clear();

            foreach (Contact contact in contacts)
            {
                dataGridView1.Rows.Add(
                    contact.FirstName,
                    contact.LastName,
                    contact.PhoneNumber,
                    contact.Email,
                    contact.City,
                    contact.ContactId
                );
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    contacts = dataBase.LoadContacts();
            //    LoadContactsToDataGridView(contacts);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Ошибка при загрузке контактов из базы данных: " + ex.Message);
            //}
        }
    }
}
