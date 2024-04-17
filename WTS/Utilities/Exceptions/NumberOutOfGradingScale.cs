using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTS.Utilities.Exceptions
{
    //In case a value is out of a grading scale, such as 1-10
    public class NumberOutOfGradingScale : ApplicationException
    {
        private int value;

        public NumberOutOfGradingScale() : base("Value is not graded at all!")
        { 
        
        }

        public NumberOutOfGradingScale(string reason) : base(reason) 
        { 
        
        }

        public NumberOutOfGradingScale(string reason, Exception innerException, int value) : base(reason, innerException)
        {
            this.value = value;
        }

        public int Value
        {
            get { return value; }
        }
    }
}
