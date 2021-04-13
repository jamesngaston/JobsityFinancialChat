using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsityFinancialChat.Interfaces
{
    public interface IBotService
    {
        Task ParseMessage(string message);

        Task SendMessage(string message);
    }
}
