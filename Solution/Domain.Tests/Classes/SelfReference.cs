﻿using Domain.Core;

namespace Domain.Tests.Classes
{
    internal class SelfReference : ValueObject<SelfReference>
    {
        public SelfReference()
        {
        }

        public SelfReference(SelfReference value)
        {
            Value = value;
        }

        public SelfReference Value { get; set; }
    }
}
