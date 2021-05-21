using System;

namespace MediaStudio.Classes.MyException
{
    public class MyUnauthorizedException : Exception
    {
        public MyUnauthorizedException(string message)
            : base(message)
        {
        }
    }
}
