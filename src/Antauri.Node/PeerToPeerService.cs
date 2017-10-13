using System;
using Antauri.Core;

public class PeerToPeerService
{
    private readonly BlockChain blockChain;

    public PeerToPeerService(BlockChain blockChain)
    {
        this.blockChain = blockChain ?? throw new System.ArgumentNullException(nameof(blockChain));
    }

    public void Write() => throw new NotImplementedException();
    public void Broadcast() => throw new NotImplementedException();
    public void ConnectToPeer() => throw new NotImplementedException();
    public void HandleMessage() => throw new NotImplementedException();
    public void ResponseLatestMessages() => throw new NotImplementedException();
}
