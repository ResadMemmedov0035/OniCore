﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OniCore.CrossCuttingConcerns.Exceptions.HttpProblemDetails
{
    public static class ProblemDetailsExtensions
    {
        public static string AsJson(this ProblemDetails details)
        {
            return JsonConvert.SerializeObject(details);
        }
    }
}
