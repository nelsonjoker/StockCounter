using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockCounter.Data
{
    public class CountEntry
    {

        public enum SyncStatus : int
        {
            NEW = 0,
            POSTED = 1
        }


        public string Itmref { get; set; }
        public int LabelNumber { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
        public DateTime Time { get; set; }
        public bool IsDeleted { get; set; }
        public string Hash { get; set; }
        public SyncStatus Sync { get; set; }
        public int SyncCode { get; set; }
        public string Location { get; set; }
        public long DeleteKey { get; set; }

        public CountEntry()
        {
            Itmref = string.Empty;
            LabelNumber = 1;
            Value = 0;
            Unit = string.Empty;
            Time = DateTime.UtcNow;
            IsDeleted = false;
            Hash = Guid.NewGuid().ToString();
            Sync = SyncStatus.NEW;
            SyncCode = 0;
            Location = string.Empty;
            DeleteKey = DateTime.UtcNow.ToFileTimeUtc();
        }

        public override bool Equals(object obj)
        {
            CountEntry other = obj as CountEntry;
            if(other != null)
            {
                if (!Itmref.Equals(other.Itmref))
                    return false;
                if (!LabelNumber.Equals(other.LabelNumber))
                    return false;
                if (!Value.Equals(other.Value))
                    return false;
                if (!Unit.Equals(other.Unit))
                    return false;
                if (!Time.Equals(other.Time))
                    return false;
                if (!IsDeleted.Equals(other.IsDeleted))
                    return false;
                if (!Hash.Equals(other.Hash))
                    return false;
                if (!Sync.Equals(other.Sync))
                    return false;
                if (SyncCode != other.SyncCode)
                    return false;
                if (!Location.Equals(other.Location))
                    return false;
                return true;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
