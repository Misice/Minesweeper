using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Klasa_Atributi
{
   
    public enum Status
    {
        Otkriven,
        Neotkriven,
        Zastavica,
        GameOver
    }


    public class Polje : Button
    {
        private Status _statusPolja;
        private char _vrednost;
        private int _redPolja, _kolonaPolja;

        #region Constructors
        public Polje():base()
        {
            StatusPolja = Status.Neotkriven;
            this.BackColor = Color.Gray;
            this.Width = 20;
            this.Height = 20;
            this._vrednost = ' ';

         
            
        }


        #endregion


        #region proprety

        
        public char Vrednost
        {
            get
            {
                return _vrednost;
            }

            set
            {
                _vrednost = value;
            }
        }
       
        public Status StatusPolja
        {
            get
            {
                return _statusPolja;
            }

            set
            {
                _statusPolja = value;
            }
        }

        
        public int RedPolja
        {
            get
            {
                return _redPolja;
            }

            set
            {
                _redPolja = value;
            }
        }
        
        public int KolonaPolja
        {
            get
            {
                return _kolonaPolja;
            }

            set
            {
                _kolonaPolja = value;
            }
        }



        #endregion


        public void SavePolje(  StreamWriter putanja)
        {


            putanja.Write(_vrednost);
            putanja.Write(" ");


        }
    }
}
