using System.Collections.Generic;
using System.Globalization;

namespace Doublel.UseCases
{
    public interface IApplicationActor
    {
        int Identifier { get; }
        IEnumerable<string> AllowedUseCases { get; }
        bool IsAdmin { get; }
        string Identity { get; set; }
        CultureInfo Locale { get; set; }
    }
}
