using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace ProjectionChants
{
    public enum Theme
    {
        Aucun, Louange, Adoration, Esprit_Saint, Méditation, Marie 
    }
    
    public class UserDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private bool _modeEdition;
        public bool modeEdition
        {
            get { return _modeEdition; }
            set
            {
                if (_modeEdition != value)
                {
                    _modeEdition = value;
                    NotifyPropertyChanged("modeEdition");
                }
            }
        }

        private bool _modeNouveau;
        public bool modeNouveau
        {
            get { return _modeNouveau; }
            set
            {
                if (_modeNouveau != value)
                {
                    _modeNouveau = value;
                    NotifyPropertyChanged("modeNouveau");
                }
            }
        }

        private bool _isFiltering;
        public bool isFiltering
        {
            get { return _isFiltering; }
            set
            {
                if (_isFiltering != value)
                {
                    _isFiltering = value;
                    NotifyPropertyChanged("isFiltering");
                }
            }
        }

        private bool _isTabVisible;
        public bool isTabVisible
        {
            get { return _isTabVisible; }
            set
            {
                if (_isTabVisible != value)
                {
                    _isTabVisible = value;
                    NotifyPropertyChanged("isTabVisible");
                }
            }
        }

        private RichTextBox _SelectedRtb;
        public RichTextBox SelectedRtb
        {
            get { return _SelectedRtb; }
            set
            {
                if (_SelectedRtb != value)
                {
                    _SelectedRtb = value;
                    NotifyPropertyChanged("SelectedRtb");
                }
            }
        }

        private int _Id = 0;
        public int Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }
        private string _Titre = "";
        public string Titre
        {
            get { return _Titre; }
            set
            {
                if (!_Titre.Equals(value))
                {
                    _Titre = value;
                    NotifyPropertyChanged("Titre");
                }
            }
        }
        private string _Theme = "";
        public string Theme
        {
            get { return _Theme; }
            set
            {
                if (!_Theme.Equals(value))
                {
                    _Theme = value;
                    NotifyPropertyChanged("Theme");
                }
            }
        }
        private string _Cle = "";
        public string Cle
        {
            get { return _Cle; }
            set
            {
                if (!_Cle.Equals(value))
                {
                    _Cle = value;
                    NotifyPropertyChanged("Cle");
                }
            }
        }
        private string _Refrain = "";
        public string Refrain
        {
            get { return _Refrain; }
            set
            {
                if (!_Refrain.Equals(value))
                {
                    _Refrain = value;
                    NotifyPropertyChanged("Refrain");
                }
            }
        }
        private string _RefrainTab = "";
        public string RefrainTab
        {
            get { return _RefrainTab; }
            set
            {
                if (!_RefrainTab.Equals(value))
                {
                    _RefrainTab = value;
                    NotifyPropertyChanged("RefrainTab");
                }
            }
        }
        private string _Couplet = "";
        public string Couplet
        {
            get { return _Couplet; }
            set
            {
                if (!_Couplet.Equals(value))
                {
                    _Couplet = value;
                    NotifyPropertyChanged("Couplet");
                }
            }
        }
        private string _CoupletTab = "";
        public string CoupletTab
        {
            get { return _CoupletTab; }
            set
            {
                if (!_CoupletTab.Equals(value))
                {
                    _CoupletTab = value;
                    NotifyPropertyChanged("CoupletTab");
                }
            }
        }

        private int _NbCouplet = 0;
        public int NbCouplet
        {
            get { return _NbCouplet; }
            set
            {
                if (_NbCouplet != value)
                {
                    _NbCouplet = value;
                    NotifyPropertyChanged("_NbCouplet");
                }
            }
        }

        private SortedList<int, Couplet> _Couplets = new SortedList<int, Couplet>();
        public SortedList<int, Couplet> Couplets { get { return _Couplets; } set { _Couplets = value; } }
        


        private string _Numero = "";
        public string Numero
        {
            get { return _Numero; }
            set
            {
                if (_Numero != value)
                {
                    _Numero = value;
                    NotifyPropertyChanged("Numero");
                }
            }
        }

        private GridLength _RefrainLength = new GridLength(200);
        public GridLength RefrainLength
        {
            get { return _RefrainLength; }
            set
            {
                if (_RefrainLength != value)
                {
                    _RefrainLength = value;
                    NotifyPropertyChanged("RefrainLength");
                }
            }
        }
        public int RefrainSize
        {
            get { return Convert.ToInt32(RefrainLength.Value); }
            set { RefrainLength = new GridLength(value); }
        }
        
        private GridLength _RefrainTabLength = new GridLength(100);
        public GridLength RefrainTabLength
        {
            get { return _RefrainTabLength; }
            set
            {
                if (_RefrainTabLength != value)
                {
                    _RefrainTabLength = value;
                    NotifyPropertyChanged("RefrainTabLength");
                }
            }
        }
        public int RefrainTabSize
        {
            get { return Convert.ToInt32(RefrainTabLength.Value); }
            set { RefrainTabLength = new GridLength(value); }
        }

        private GridLength _CoupletTabLength = new GridLength(100);
        public GridLength CoupletTabLength
        {
            get { return _CoupletTabLength; }
            set
            {
                if (_CoupletTabLength != value)
                {
                    _CoupletTabLength = value;
                    NotifyPropertyChanged("CoupletTabLength");
                }
            }
        }
        public int CoupletTabSize
        {
            get { return Convert.ToInt32(CoupletTabLength.Value); }
            set { CoupletTabLength = new GridLength(value); }
        }

        
    }

    public class Couplet
    {
        private int _id;
        private int _numero;
        private string _text;
        private int _tabSize;
        private string _tabText;
        public int Id { get { return _id; } }
        public int Numero { get { return _numero; } set { _numero = value; } }
        public string Text { get { return _text; } set { _text = value; } }
        public int TabSise { get { return _tabSize; } set { _tabSize = value; } }
        public string Tab { get { return _tabText; } set { _tabText = value; } }

        public Couplet(int numero)
        {
            _id = 0;
            _numero = numero;
            _text = "";
            _tabText = "";
            _tabSize = 150;
        }

        public Couplet(int numero, int id)
        {
            _id = id;
            _numero = numero;
            _text = "";
            _tabText = "";
            _tabSize = 150;
        }
    }


}
