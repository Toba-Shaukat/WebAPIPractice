using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class EmployeeParameters : RequestParameters
    {
        //Filtering 
        //signed int: int +/-
        //unsigned int +
        public uint MinAge { get; set; }
        public uint MaxAge { get; set; } = int.MaxValue;

        public bool ValidRange => MaxAge > MinAge;
    }
}
