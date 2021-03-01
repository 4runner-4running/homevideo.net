using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideo.Net.Services.Contracts
{
    public interface IService
    {
        Guid Id { get; set; }
        string Name { get; set; }
        bool Running { get; set; }
    }
}
