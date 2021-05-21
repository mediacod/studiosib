using System;

namespace MediaStudio.Classes.MyException
{
    public class MyNotFoundException : Exception
    {
        public MyNotFoundException(string message)
            : base(message)
        {
        }
    }
}
