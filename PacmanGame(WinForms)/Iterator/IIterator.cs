﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame_WinForms_.Iterator
{
    public interface IIterator<T>
    {
        bool HasNext();
        (int Index, T Item) Next();
    }
}
