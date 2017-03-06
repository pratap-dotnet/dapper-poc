using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using DapperPoc.Samples;
using DapperPoc.Common;

namespace DapperPoc
{
    public partial class Form1 : Form
    {
        private readonly SamplesFactory samplesFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            samplesFactory = new SamplesFactory(new TextBoxAdapter(textBox1));
        }

        /// <summary>
        /// Called when [select statement button click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnSelectStatementButtonClick(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.SimpleSelectStatement);
        }

        /// <summary>
        /// Called when [select returning dynamic objects click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnSelectReturningDynamicObjectsClick(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.SimpleSelectStatementWithDynamicEntities);
        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.MultiMappingSingleEntity);
        }

        /// <summary>
        /// Handles the Click event of the button3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button3_Click(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.MultiMappingChildEntities);
        }

        /// <summary>
        /// Handles the Click event of the button4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button4_Click(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.ParameterizedSelectStatement);
        }

        /// <summary>
        /// Handles the Click event of the button5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button5_Click(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.MultipleQueries);
        }

        /// <summary>
        /// Handles the Click event of the button6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button6_Click(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.SimpleStoredProcedure);
        }

        /// <summary>
        /// Handles the Click event of the button7 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button7_Click(object sender, System.EventArgs e)
        {
            using (NewUserForm form = new NewUserForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Person person = form.PersonData;
                    samplesFactory.Execute(SampleTypes.SimpleInsertStatement, new object[] { person });
                }
            }

        }
    }
}
