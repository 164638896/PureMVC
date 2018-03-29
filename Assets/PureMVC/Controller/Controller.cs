using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 对command的管理
public class Controller
{
    private Dictionary<string, ICommand> msgToCommand;

    public Controller()
    {
        this.msgToCommand = new Dictionary<string, ICommand>();
    }

    public void ResiterCommand(string msg, ICommand command)
    {
        if (!this.msgToCommand.ContainsKey(msg))
        {
            this.msgToCommand.Add(msg, command);
        }
        else
        {
            //Debug.Log("已经存在命令");
        }
    }

    public void UnResiterCommand(string msg)
    {
        if (this.msgToCommand.ContainsKey(msg))
        {
            this.msgToCommand.Remove(msg);
        }
        else
        {
            //Debug.Log("找不到命令");
        }
    }

    public void ExcuteCommand(INotifier inotifier)
    {
        if (this.msgToCommand.ContainsKey(inotifier.msg))
        {
            msgToCommand[inotifier.msg].Excute(inotifier);
        }
    }
}

