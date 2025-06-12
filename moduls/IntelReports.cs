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
        public int Id { get; private set; }
        public int Reporter_id { get; private set; }
        public int Target_id { get; private set; }
        public string Text { get; private set; }
        public DateTime Timestamp { get; private set; }

        public IntelReports(int reporter_id, int target_id, string text)
        {
            Reporter_id = reporter_id;
            Target_id = target_id;
            Text = text;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Reporter_id: {Reporter_id}, Target_id: {Target_id}, Text: {Text}, Timestamp: {Timestamp}";
        }

    }
}
