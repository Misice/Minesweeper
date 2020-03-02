using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Klasa_Atributi
{

    public class PoljeMatrix
    {
        private Polje[,] _matrica;
        private int _brojMina = 10;
        private int _red = 9, _kolona = 9;
        private Polje[] nova;
        public PoljeMatrix()
        {
        }




        private static PoljeMatrix _instance = null;



        #region proprety


        public static PoljeMatrix Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PoljeMatrix();

                }
                return _instance;
            }
        }

        
        public int Red
        {
            get
            {
                return _red;
            }

            set
            {
                _red = value;
            }
        }

        
        public int Kolona
        {
            get
            {
                return _kolona;
            }

            set
            {
                _kolona = value;
            }
        }

        
        public int BrojMina
        {
            get
            {
                return _brojMina;
            }

            set
            {
                _brojMina = value;
            }
        }
        
        public Polje[,] Matrica
        {
            get
            {
                return _matrica;
            }

            set
            {
                _matrica = value;
            }
        }

       

        #endregion

        #region Methods

        public void PokaziGame()
        {

            for (int i = 0; i < _red; i++)
            {
                for (int j = 0; j < _kolona; j++)
                {
                    if (_matrica[i, j].StatusPolja == Status.GameOver)
                    {
                        _matrica[i, j].BackColor = Color.Red;
                    }
                    else if (_matrica[i, j].Vrednost == 'X')
                    {
                        _matrica[i, j].BackColor = Color.DarkRed;
                    }
                    else
                    {
                        _matrica[i, j].BackColor = Color.LightGray;
                    }



                    _matrica[i, j].StatusPolja = Status.Otkriven;
                    _matrica[i, j].Text = Convert.ToString(_matrica[i, j].Vrednost);

                }
            }
        }


        public void NapraviMatricu(int red, int kolona, int mine)
        {
            this._red = red;
            this._kolona = kolona;
            this._brojMina = mine;

            this._matrica = new Polje[_red, _kolona];

            for (int i = 0; i < _red; i++)
            {
                for (int j = 0; j < _kolona; j++)
                {
                    _matrica[i, j] = new Polje();
                    _matrica[i, j].RedPolja = i;
                    _matrica[i, j].KolonaPolja = j;
                }
            }

            Random novi = new Random();
            int pom = _brojMina;

            while (pom > 0)
            {
                int a = novi.Next(0, _red);
                int b = novi.Next(0, _kolona);

                if (_matrica[a, b].Vrednost != 'X')
                {
                    _matrica[a, b].Vrednost = 'X';
                    pom--;

                }
            }


            int count;

            for (int i = 0; i < _red; i++)
            {
                for (int j = 0; j < _kolona; j++)
                {

                    count = 0;

                    if (_matrica[i, j].Vrednost == 'X')
                    {
                        continue;
                    }


                    for (int k = i - 1; k <= i + 1; k++)
                    {
                        for (int l = j - 1; l <= j + 1; l++)
                        {

                            if (k >= 0 && l >= 0 && k < PoljeMatrix.Instance.Red && l < PoljeMatrix.Instance.Kolona)
                            {
                                if (_matrica[k, l].Vrednost == 'X')
                                {
                                    count++;
                                }
                            }

                        }

                    }

                    _matrica[i, j].Vrednost = Convert.ToChar(count + 48);

                }
            }

            nova = new Polje[_red * _kolona];

            count = 0;

            for (int i = 0; i < _red; i++)
            {
                for (int j = 0; j < _kolona; j++)
                {

                    nova[count] = _matrica[i, j];
                    count++;
                }
            }



        }



        public void Clear()
        {

            for (int i = 0; i < _red; i++)
            {
                for (int j = 0; j < _kolona; j++)
                {
                    _matrica[i, j] = null;

                }
            }

            _instance = new PoljeMatrix();
        }

        public Polje findPolje(Polje b)
        {
            for (int i = 0; i < _red; i++)
            {
                for (int j = 0; j < _kolona; j++)
                {
                    if (_matrica[i, j] == b)
                        return _matrica[i, j];
                }
            }

            return null;
        }




        public void Save(/*string path*/ StreamWriter wr)
        {

            /*
                XmlTextWriter wr = null;
                try
                {
                    wr = new XmlTextWriter(path, Encoding.Unicode);
                    XmlSerializer sr = new XmlSerializer(typeof(PoljeMatrix));

                    sr.Serialize(wr, Instance);

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.StackTrace);
                }
                finally
                {

                    wr.Close();
                }

    */



         wr.WriteLine(this._red);
            wr.WriteLine(this._kolona);
            wr.WriteLine(this._brojMina);

            for (int i = 0; i<_red; i++)
            {
                for (int j = 0; j<_kolona; j++)
                {
                    _matrica[i, j].SavePolje(wr);
                  }
              wr.WriteLine();
            }

        }




        public bool Pobeda()
        {
            int count = 0;

            for (int i = 0; i < _red; i++)
            {
                for (int j = 0; j < _kolona; j++)
                {

                    if (_matrica[i, j].StatusPolja==Status.Neotkriven)
                    {
                        count++;
                    }

                }
            }

            if (count == _brojMina)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        #endregion


    }
}

