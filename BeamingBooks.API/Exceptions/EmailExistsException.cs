﻿using System;

namespace BeamingBooks.API.Exceptions
{
    public class EmailExistsException : Exception
    {
        public EmailExistsException() : base() { }

        public EmailExistsException(string message) : base(message) { }

        public EmailExistsException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
