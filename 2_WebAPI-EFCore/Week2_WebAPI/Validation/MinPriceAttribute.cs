﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Week2_WebAPI.Validation
{
    public class MinPriceAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (decimal)value >= 50;
        }
    }
}
