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
        public Form1()
        {
            InitializeComponent();
            samplesFactory = new SamplesFactory(new TextBoxAdapter(textBox1));
        }

        private void OnSelectStatementButtonClick(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.SimpleSelectStatement);
        }

        private void OnSelectReturningDynamicObjectsClick(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.SimpleSelectStatementWithDynamicEntities);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.MultiMappingSingleEntity);
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.MultiMappingChildEntities);
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.ParameterizedSelectStatement);
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.MultipleQueries);
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            samplesFactory.Execute(SampleTypes.SimpleStoredProcedure);
        }

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
