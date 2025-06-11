using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Malshinon
{
    internal class IntelReports
    {
        public int Id;
        public int Reporter_id;
        public int Target_id;
        public string Text;
        public DateTime Timestamp;

        public IntelReports(int reporter_id, int target_id, string text)
        {
            Reporter_id = reporter_id;
            Target_id = target_id;
            Text = text;
            //DateTime timestamp = DateTime.Now;
            //Timestamp = timestamp;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Reporter_id: {Reporter_id}, Target_id: {Target_id}, Text: {Text}, Timestamp: {Timestamp}";
        }

    }
}
