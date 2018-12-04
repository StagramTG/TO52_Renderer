using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public struct MessageData<DataType>
{
    public int Type { get; set; }
    public DataType Data { get; set; }

    public MessageData(int ptype, DataType pdata)
    {
        Type = ptype;
        Data = pdata;
    }
}

