using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 主要的逻辑
public interface ICommand
{
    void Excute(INotifier inotifier);
}
