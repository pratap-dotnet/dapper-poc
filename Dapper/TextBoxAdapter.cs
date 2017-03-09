using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DapperPoc
{
    public interface ILogger
    {
        /// <summary>
        /// Writes the separator.
        /// </summary>
        void WriteSeparator();

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="line">The line.</param>
        void WriteLine(string line);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
    }

    class TextBoxAdapter : ILogger
    {
        private readonly TextBox textBox;
        private const string separator = "*********************************************************************************************************************************************";

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxAdapter"/> class.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        public TextBoxAdapter(TextBox textBox)
        {
            this.textBox = textBox;
        }

        /// <summary>
        /// Writes the separator.
        /// </summary>
        public void WriteSeparator()
        {
            WriteLine(separator);
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="line">The line.</param>
        public void WriteLine(string line)
        {
            textBox.Text += line;
            textBox.Text += Environment.NewLine;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            textBox.Text = string.Empty;
        }
    }
}
