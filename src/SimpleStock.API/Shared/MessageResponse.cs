namespace SimpleStock.API.Shared;

public class MessageResponse
{
    public MessageResponse(string message)
    {
        Message = message;
        Moment = DateTime.Now;
    }

    public MessageResponse(string message, DateTimeOffset moment)
    {
        Message = message;
        Moment = moment;
    }

    public string Message { get; private set; }
    public DateTimeOffset Moment { get; private set; }
}
