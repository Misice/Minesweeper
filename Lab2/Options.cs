using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Klasa_Atributi;

namespace Lab2
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Provera())
            {
                PoljeMatrix.Instance.NapraviMatricu(Convert.ToInt32(txbRed.Text), Convert.ToInt32(txbKolona.Text), Convert.ToInt32(txbMine.Text));

                this.DialogResult = DialogResult.OK;
            }

        }



        private bool Provera()
        {
            int value;

            if(!(int.TryParse(txbRed.Text, out value)) )
            {
                MessageBox.Show("TextBox ROWS CAN ONLY HOLD INT NUMBERS From 9 to 30");
                return false;
            }

            if (!(int.TryParse(txbKolona.Text, out value)))
            {
                MessageBox.Show("TextBox COLUMNS CAN ONLY HOLD INT NUMBERS From 9 to 30");
                return false;
            }
            if (!(int.TryParse(txbMine.Text, out value)))
            {
                MessageBox.Show("TextBox MINES CAN ONLY HOLD INT NUMBERS From 9 to 30");
                return false;
            }

            int pom = Convert.ToInt32(txbRed.Text);
            int pom2 = Convert.ToInt32(txbKolona.Text);
            int pom3 = Convert.ToInt32(txbMine.Text);

            if (pom < 9 || pom2 < 9 || pom>30 || pom2>30)
            {
                MessageBox.Show("RED I KOLONA MINIMUM 9 , a MAXIMUM 30  ");
                return false;
            }

            if (pom3 < 10 || pom3 >= (pom * pom2) - 15)
            {
                MessageBox.Show($"Najmanje 10 mina, a najvise {(pom * pom2) - 16}");
                return false;
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
