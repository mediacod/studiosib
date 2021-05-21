using System;

namespace MediaStudio.Classes.MyException
{
    public class MyBadRequestException : Exception
    {
        public MyBadRequestException(string message)
            : base(message)
        {
        }
    }
}
