using DapperPoc.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DapperPoc
{
    public partial class NewUserForm : Form
    {
        public Person PersonData { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewUserForm"/> class.
        /// </summary>
        public NewUserForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the NewUserForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void NewUserForm_Load(object sender, EventArgs e)
        {
            LoadPersonType();
            LoadSuffix();
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            lblValidationMessage.Text = string.Empty;
            if(ValidateInput() == false)
                return;

            PersonData = GetPersonData();
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Loads the type of the person.
        /// </summary>
        void LoadPersonType()
        {
            comboPersonType.Items.AddRange(new string[]{ "EM", "IN", "SC", "VC"});
        }

        /// <summary>
        /// Loads the suffix.
        /// </summary>
        void LoadSuffix()
        {
            comboSuffix.Items.AddRange(new string[] { "Dr", "Mr", "Mrs", "Miss"});
        }

        /// <summary>
        /// Validates the input.
        /// </summary>
        /// <returns></returns>
        bool ValidateInput()
        {
            if (comboPersonType.SelectedIndex == -1)
            {
                lblValidationMessage.Text = "Please select Person Type.";
                return false;
            }

            if (comboSuffix.SelectedIndex == -1)
            {
                lblValidationMessage.Text = "Please select Suffix.";
                return false;
            }

            if (txtFirstName.Text.Length == 0)
            {
                lblValidationMessage.Text = "Please enter First name.";
                return false;
            }

            if (ValidateName(txtFirstName.Text) == false)
            {
                lblValidationMessage.Text = "First name may contain only text.";
                return false;
            }

            if (txtMiddleName.TextLength > 0 && ValidateName(txtMiddleName.Text) == false)
            {
                lblValidationMessage.Text = "Middle name may contain only text.";
                return false;
            }

            if (txtLastName.Text.Length == 0)
            {
                lblValidationMessage.Text = "Please enter Last name.";
                return false;
            }

            if (ValidateName(txtLastName.Text) == false)
            {
                lblValidationMessage.Text = "Last name may contain only text.";
                return false;
            }

            if (txtTitle.Text.Length == 0)
            {
                lblValidationMessage.Text = "Please enter Title.";
                return false;
            }

            if (ValidateTitle(txtTitle.Text) == false)
            {
                lblValidationMessage.Text = "Title may contain only text.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        bool ValidateName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z]+$");
        }

        /// <summary>
        /// Validates the title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        bool ValidateTitle(string title)
        {
            return Regex.IsMatch(title, @"^[a-zA-Z]+$");
        }

        /// <summary>
        /// Validates the e mail.
        /// </summary>
        /// <param name="eMail">The e mail.</param>
        /// <returns></returns>
        bool ValidateEMail(string eMail)
        {
            return Regex.IsMatch(eMail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Gets the person data.
        /// </summary>
        /// <returns></returns>
        Person GetPersonData()
        {
            Person person = new Person();
            {
                person.PersonType = comboPersonType.SelectedItem.ToString();
                person.Suffix = comboSuffix.SelectedItem.ToString();
                person.FirstName = txtFirstName.Text;
                person.MiddleName = txtMiddleName.Text;
                person.LastName = txtLastName.Text;
                person.Title = txtTitle.Text;
                person.AdditionalContactInfo = txtAdditionalContactInfo.Text;
            }
            return person;
        }
    }
}
