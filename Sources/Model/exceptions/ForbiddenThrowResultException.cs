﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.exceptions
{
    public class ForbiddenThrowResultException : Exception
    {
        public ForbiddenThrowResultException(string message) : base(message)
        {
        }
    }
}