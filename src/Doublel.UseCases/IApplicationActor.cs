using System;
using System.Collections.Generic;
using System.Text;

namespace Doublel.UseCases
{
    public interface IApplicationActor
    {
        object Identifier { get; }
        IEnumerable<int> AllowedUseCaseIds { get; }
        bool IsAdmin { get; }
        string Identity { get; set; }
    }
}
