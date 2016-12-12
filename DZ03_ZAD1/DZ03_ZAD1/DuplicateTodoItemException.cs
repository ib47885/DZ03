using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ03_ZAD1
{
    class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException(String message) : base(message)
        {


        }
    }
}
