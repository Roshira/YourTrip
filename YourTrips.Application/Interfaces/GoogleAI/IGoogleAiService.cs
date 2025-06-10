using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.Interfaces.GoogleAI
{
    public interface IGoogleAiService
    {
        Task<string> GenerateTextAsync(string prompt);
    }
}
