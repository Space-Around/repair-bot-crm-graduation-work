using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMGraduationWork
{
    public partial class CompletedRequestDGV
    {
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AcceptDate { get; set; }
        public DateTime CompleteDate { get; set; }
    }
}
