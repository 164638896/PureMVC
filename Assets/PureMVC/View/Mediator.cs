using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 显示 比如UI
public abstract class Mediator
{
    public abstract string Name { get; }

    public abstract List<string> MsgList { get; }

    public abstract void Execute(INotifier inofifier);
}

