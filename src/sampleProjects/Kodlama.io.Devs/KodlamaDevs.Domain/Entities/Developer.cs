using OniCore.Persistence.Repositories;
using OniCore.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodlamaDevs.Domain.Entities
{
    public class Developer : User
    {
        public string GithubAddress { get; set; }
    }
}
