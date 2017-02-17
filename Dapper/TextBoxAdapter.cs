using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DapperPoc
{
    interface ILogger
    {
        void WriteSeparator();
        void WriteLine(string line);
        void Clear();
    }

    class TextBoxAdapter : ILogger
    {
        private readonly TextBox textBox;
        private const string separator = "*********************************************************************************************************************************************";
        public TextBoxAdapter(TextBox textBox)
        {
            this.textBox = textBox;
        }

        public void WriteSeparator()
        {
            WriteLine(separator);
        }

        public void WriteLine(string line)
        {
            textBox.Text += line;
            textBox.Text += Environment.NewLine;
        }

        public void Clear()
        {
            textBox.Text = string.Empty;
        }
    }
}
