using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuBeefrri.Services.Interfaces
{
    public interface IMail
    {
        Task Send(string message);
    }
}
