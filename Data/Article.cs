using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace StockCounter.Data
{
    [Bindable(true)]
    public class Article : INotifyPropertyChanged
    {
        public string Itmref { get; set; }
        public string Itmdes { get; set; }
        public int Itmsta { get; set; }
        public string Tsicod0 { get; set; }
        public string Tsicod1 { get; set; }
        public string Tsicod2 { get; set; }
        public string Tsicod3 { get; set; }
        public string Tsicod4 { get; set; }
        public string Tclcod { get; set; }
        public string Stu { get; set; }
        public DateTime Lascundat { get; set; }
        public double Physto { get; set; }
        public double Mfmqty { get; set; }

        public bool Mvt { get; set; }
        public bool Cun { get; set; }
        public bool Mfm { get; set; }
        public bool Sto { get; set; }

        public bool IsActive { get { return Itmsta == 1; } }

        private double mCountedQuantity;
        public double CountedQuantity { get { return mCountedQuantity; } set { mCountedQuantity = value; OnPropertyChanged("CountedQuantity"); } }

        private int mLabelCount;
        public int LabelCount { get { return mLabelCount; } set { mLabelCount = value; OnPropertyChanged("LabelCount"); } }

        public bool Cunflg { get; internal set; }
        public string Cunlisnum { get; internal set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override bool Equals(object obj)
        {
            if(obj is Article)
            {
                Article other = obj as Article;
                if (!this.Itmref.Equals(other.Itmref))
                    return false;
                if (!this.Itmdes.Equals(other.Itmdes))
                    return false;
                if (!this.Itmsta.Equals(other.Itmsta))
                    return false;
                if (!this.Tsicod0.Equals(other.Tsicod0))
                    return false;
                if (!this.Tsicod1.Equals(other.Tsicod1))
                    return false;
                if (!this.Tsicod2.Equals(other.Tsicod2))
                    return false;
                if (!this.Tsicod3.Equals(other.Tsicod3))
                    return false;
                if (!this.Tsicod4.Equals(other.Tsicod4))
                    return false;
                if (!this.Tclcod.Equals(other.Tclcod))
                    return false;
                if (!this.Stu.Equals(other.Stu))
                    return false;
                if (!this.Lascundat.Equals(other.Lascundat))
                    return false;
                if (!this.Physto.Equals(other.Physto))
                    return false;
                if (!this.Mfmqty.Equals(other.Mfmqty))
                    return false;

                if (!this.Mvt.Equals(other.Mvt))
                    return false;
                if (!this.Cun.Equals(other.Cun))
                    return false;
                if (!this.Mfm.Equals(other.Mfm))
                    return false;
                if (!this.Sto.Equals(other.Sto))
                    return false;

                if (!this.Cunflg.Equals(other.Cunflg))
                    return false;
                if (!this.Cunlisnum.Equals(other.Cunlisnum))
                    return false;

                return true;
            }


            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Itmref.GetHashCode();
        }


        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
