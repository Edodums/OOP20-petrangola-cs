using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_petrangola_cs.models
{
    public interface ICards
    {
        ICombination Combination { get; set; }

        bool Community { get; set; }

    }
}
