using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class UIOpenData
{
    public int type;
    public int value;
    public object obj;

    public UIOpenData(int type, int value, object obj = null)
    {
        this.type = type;
        this.value = value;
        this.obj = obj;
    }
}