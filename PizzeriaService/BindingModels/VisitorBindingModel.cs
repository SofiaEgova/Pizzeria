using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService.BindingModels
{
    [DataContract]
    public class VisitorBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string VisitorFIO { get; set; }
    }
}
