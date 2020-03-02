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
using System.IO;

namespace Lab2
{
    public partial class Form1 : Form
    {

        int sec = 0, min = 0, hour = 0;
        int pomsec = 0, najduze = 0,najkrace=1000;
        bool stopped = true;
        bool dalije = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PoljeMatrix.Instance.NapraviMatricu(9, 9, 15);

            this.PravljenjeMatrice();

        }

        private void newCustomGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

            PoljeMatrix.Instance.Clear();
            var nova = new Options();
            nova.ShowDialog();

            if (nova.DialogResult == DialogResult.OK)
            {
                txbScore.Text = "0";
                tableLayoutPanel1.Visible = false;
                tableLayoutPanel1.Controls.Clear();
                lblInformacije.Visible = false;
                saveGameToolStripMenuItem.Enabled = false;



                this.PravljenjeMatrice();

                this.REFRESH_FORM();
            }

        }

        private void endGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.ENDGAME();

        }

        private void newEasyGameToolStripMenuItem_Click(object sender, EventArgs e)
        {


            this.NovaIgra();

            PoljeMatrix.Instance.NapraviMatricu(9, 9, 15);

            this.PravljenjeMatrice();

            this.REFRESH_FORM();
        }

        private void newMediumGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.NovaIgra();

            PoljeMatrix.Instance.NapraviMatricu(13, 13, 35);

            this.PravljenjeMatrice();


            this.REFRESH_FORM();
        }

        private void newHardGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.NovaIgra();

            PoljeMatrix.Instance.NapraviMatricu(17, 17, 70);
            this.PravljenjeMatrice();


            this.REFRESH_FORM();
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog novi = new SaveFileDialog();
            //novi.Filter= "xml files (*.xml)|*.xml";

           novi.Filter = "(*.txt)|*.txt";

            if (novi.ShowDialog()==DialogResult.OK)
            {



                //    PoljeMatrix.Save(novi.FileName);



                using (StreamWriter wr = new StreamWriter(new FileStream(novi.FileName, FileMode.Create)))
                {

                    wr.Write("hours: ");
                    wr.Write(hour);
                    wr.Write("  minutes: ");
                    wr.Write(min);
                    wr.Write("  seconds: ");
                    wr.Write(sec);
                    wr.WriteLine();
                    wr.Write("SCORE: ");
                    wr.WriteLine(txbScore.Text);


                    PoljeMatrix.Instance.Save(wr);

                    MessageBox.Show("Uspesno sacuvano");





                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (label3.Text == Convert.ToString(60))
            {
                sec = 0;

                label3.Text = Convert.ToString(sec);
                label4.Text = Convert.ToString(++min);
            }
            else if (label4.Text == Convert.ToString(5))
            {
                sec = 0;
                min = 0;
                label3.Text = Convert.ToString(sec);
                label4.Text = Convert.ToString(min);


                label5.Text = Convert.ToString(++hour);
            }
            else
            {
                label3.Text = Convert.ToString(++sec);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (stopped == true)
            {
                timer.Start();
                stopped = false;
                btnStopStart.Text = "Stop";
                tableLayoutPanel1.Enabled = true;


            }
            else
            {
                timer.Stop();
                stopped = true;
                btnStopStart.Text = "Start";
                tableLayoutPanel1.Enabled = false;

            }


        }

        private void PravljenjeMatrice()
        {
            for (int i = 0; i < PoljeMatrix.Instance.Red; i++)
            {
                for (int j = 0; j < PoljeMatrix.Instance.Kolona; j++)
                {
                    tableLayoutPanel1.Controls.Add(PoljeMatrix.Instance.Matrica[i, j], j, i);

                    PoljeMatrix.Instance.Matrica[i, j].MouseDown += new MouseEventHandler(Button_Click);
                }
            }

            tableLayoutPanel1.ColumnCount = PoljeMatrix.Instance.Kolona;
            tableLayoutPanel1.RowCount = PoljeMatrix.Instance.Red;

            this.Height = (PoljeMatrix.Instance.Red * 20) + 230;
            this.Width = (PoljeMatrix.Instance.Kolona * 20) + 500;

            tableLayoutPanel1.Enabled = false;
        }

        private void REFRESH_FORM()
        {
            label3.Text = "0";
            label4.Text = "0";
            label5.Text = "0";

            sec = min = hour = 0;
            stopped = true;
            btnStopStart.Text = "Start";
            btnStopStart.Enabled = true;

            timer.Stop();

            tableLayoutPanel1.Enabled = false;

            endGameToolStripMenuItem.Enabled = true;
            tableLayoutPanel1.Visible = true;
            btnRestart.Visible = false;


        }

        public void Button_Click(object sender, MouseEventArgs arg)
        {

            Polje pom = PoljeMatrix.Instance.findPolje(sender as Polje);

            switch (arg.Button)
            {
                case MouseButtons.Left:

                    if (pom.StatusPolja == Status.Neotkriven)
                    {
                        if (pom.Vrednost == 'X')
                        {
                            pom.BackColor = Color.Red;
                            pom.StatusPolja = Status.GameOver;
                            this.ENDGAME();
                        }
                        else
                        {


                            if (dalije == true)
                            {
                                if (pomsec > najduze)
                                {
                                    lbNajduze.Text = Convert.ToString(pomsec+" ms ");
                                    najduze = pomsec;
                                    pomsec = 0;

                                }
                                else if (pomsec < najkrace)
                                {
                                    lbNajkrace.Text = Convert.ToString(pomsec + " ms ");
                                    najkrace = pomsec;
                                    pomsec = 0;
 
                                }

                            }
                            if (dalije == false)
                            {
                                timer1.Start();
                                dalije = true;
                            }

                            pom.BackColor = Color.LightGray;
                            pom.StatusPolja = Status.Otkriven;
                            pom.Text = Convert.ToString(pom.Vrednost);

                            int Broj = int.Parse(txbScore.Text);
                            Broj += pom.Vrednost - 48;
                            txbScore.Text = Convert.ToString(Broj);



                            if (pom.Vrednost == '0')
                            {
                                this.Otkrivanje_Buttona_Sa_Vrednost_0(pom);
                            }

                            if (PoljeMatrix.Instance.Pobeda())
                            {
                                MessageBox.Show("Pobedili ste");
                                this.ENDGAME();
                            }
                            
                        }
                    }
    
                        
                    break;

                case MouseButtons.Right:

                    if (pom.StatusPolja == Status.Zastavica)
                    {
                        pom.BackColor = Color.Gray;
                        pom.StatusPolja = Status.Neotkriven;
                    }
                    else if (pom.StatusPolja == Status.Neotkriven)
                    {

                        pom.BackColor = Color.Red;
                        pom.StatusPolja = Status.Zastavica;
                    }
                    timer1.Stop();
                    break;

                    
            }

        }

        private void ENDGAME()
        {
            PoljeMatrix.Instance.PokaziGame();
            stopped = true;
            timer.Stop();

            btnStopStart.Text = "End Game";
            btnStopStart.Enabled = false;
            tableLayoutPanel1.Enabled = false;
            // btnRestart.Visible = true;
            endGameToolStripMenuItem.Enabled = false;
            lblInformacije.Visible = true;
            saveGameToolStripMenuItem.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            pomsec++;
        }

        private void NovaIgra()
        {
            txbScore.Text = "0";
            tableLayoutPanel1.Visible = false;
            PoljeMatrix.Instance.Clear();
            tableLayoutPanel1.Controls.Clear();
            saveGameToolStripMenuItem.Enabled = false;
            lblInformacije.Visible = false;
            pomsec = 0;
            najkrace = 1000;
            najduze = 0;
            lbNajduze.Text = "0";
            lbNajkrace.Text = "0";
            dalije = false;
        }

        private void Otkrivanje_Buttona_Sa_Vrednost_0(Polje polje)
            {


                for (int k = polje.RedPolja - 1; k <= polje.RedPolja + 1; k++)
                {
                    for (int l = polje.KolonaPolja - 1; l <= polje.KolonaPolja + 1; l++)
                    {

                        if (k >= 0 && l >= 0 && k < PoljeMatrix.Instance.Red && l < PoljeMatrix.Instance.Kolona)
                        {
                                if (PoljeMatrix.Instance.Matrica[k, l].StatusPolja == Status.Neotkriven)
                                {


                                    polje.BackColor = Color.LightGray;
                                    polje.StatusPolja = Status.Otkriven;
                                    polje.Text = Convert.ToString(polje.Vrednost);



                                            if (PoljeMatrix.Instance.Matrica[k, l].Vrednost == '0')
                                                this.Otkrivanje_Buttona_Sa_Vrednost_0(PoljeMatrix.Instance.Matrica[k, l]);    
                                        

                                }
                         }

                      }
                }
                return;

            }
    }
}
