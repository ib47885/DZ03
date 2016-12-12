using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ03_ZAD1
{
    class TodoAccessDeniedException : Exception
    {
        public TodoAccessDeniedException(String message) : base(message)
        {


        }
    }
}
