using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class UnsolvableMatrixException : Exception
    {
        public UnsolvableMatrixException(string message)
    : base(message) { }

    }
}
