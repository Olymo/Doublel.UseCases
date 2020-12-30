using System;

namespace Doublel.UseCases
{
    public class Empty
    {
        private Empty() { }
        public static Empty Value => new Empty();
    }
}
