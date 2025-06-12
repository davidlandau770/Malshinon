using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;

namespace Malshinon
{
    internal class Alert
    {
        public int Id {  get; private set; }
        public int Target_id {  get; private set; }
        public DateTime Created_at {  get; private set; }
        public string Reason {  get; private set; }
        
        public Alert(int target_id, string reason)
        {
            Target_id = target_id;
            Reason = reason;
            Created_at = DateTime.Now;
        }
    }
}
