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
        public string Timestamp;

        public IntelReports(int id, int reporter_id, int target_id, string text, string timestamp)
        {
            Id = id;
            Reporter_id = reporter_id;
            Target_id = target_id;
            Text = text;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Reporter_id: {Reporter_id}, Target_id: {Target_id}, Text: {Text}, Timestamp: {Timestamp}";
        }

    }
}
