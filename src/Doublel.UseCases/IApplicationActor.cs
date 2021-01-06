using System.Collections.Generic;

namespace Doublel.UseCases
{
    public interface IApplicationActor
    {
        int Identifier { get; }
        IEnumerable<int> AllowedUseCaseIds { get; }
        bool IsAdmin { get; }
        string Identity { get; set; }
    }
}
