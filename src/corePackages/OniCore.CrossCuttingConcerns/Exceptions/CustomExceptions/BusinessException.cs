﻿namespace OniCore.CrossCuttingConcerns.Exceptions.CustomExceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}
