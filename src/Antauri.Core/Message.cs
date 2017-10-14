public struct Message
{
    public int Type {get;set;}
    public string Data {get;set;}

    public Message(int type)
    {
        Type = type;
        Data = "";
    }

    public Message(int type, string data)
    {
        Type = type;
        Data = data;
    }
}