using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockCounter.Data
{
    [Bindable(true)]
    public class Location
    {
        public string Code { get; set; }
        public string Description { get; set; }


        public override bool Equals(object obj)
        {
            if (obj is Location)
            {
                Location other = obj as Location;
                if (!this.Code.Equals(other.Code))
                    return false;
                if (!this.Description.Equals(other.Description))
                    return false;

                return true;
            }


            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} :: {1}", Code, Description);
        }


    }
}
