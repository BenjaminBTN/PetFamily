using PetFamily.Application.Messaging;
using System.Threading.Channels;

namespace PetFamily.Infrastructure.MessageQueues;

public class MemoryMessageQueue<TMessage> : IMessageQueue<TMessage>
{
    private readonly Channel<TMessage> _channel = Channel.CreateUnbounded<TMessage>();

    public async Task<TMessage> ReadAsync(CancellationToken ct)
    {
        return await _channel.Reader.ReadAsync(ct);
    }

    public async Task WriteAsync(TMessage message, CancellationToken ct)
    {
        await _channel.Writer.WriteAsync(message, ct);
    }
}
