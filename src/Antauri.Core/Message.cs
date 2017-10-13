public struct Message
{
    private int Type {get;set;}
    private string Data {get;set;}

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