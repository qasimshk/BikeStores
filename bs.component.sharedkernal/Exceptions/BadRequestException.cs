﻿using System;

namespace bs.component.sharedkernal.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
