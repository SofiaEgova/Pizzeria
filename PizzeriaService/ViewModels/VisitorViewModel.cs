using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.ViewModels
{
    [DataContract]
    public class VisitorViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string VisitorFIO { get; set; }
    }
}
