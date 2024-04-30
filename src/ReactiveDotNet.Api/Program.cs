using Microsoft.AspNetCore.Mvc;
using ReactiveDotNet.Api.Hubs;
using ReactiveDotNet.Api.Messages;
using ReactiveDotNet.Api.Services;
using Spackle;
using System.Numerics;
using System.Threading.Channels;

var channel = Channel.CreateUnbounded<Range<BigInteger>>(new UnboundedChannelOptions
{
	SingleReader = true,
	SingleWriter = true,
});

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddSingleton(channel.Reader);
builder.Services.AddSingleton(channel.Writer);
builder.Services.AddHostedService<SequenceService>();

var app = builder.Build();

app.MapPost("/api", async ([FromBody] SequenceStatisticsPostMessage statistics, [FromServices] ChannelWriter<Range<BigInteger>> writer) =>
{
	await writer.WriteAsync(new Range<BigInteger>(statistics.Start, statistics.End));
	return Results.Accepted();
});

app.MapHub<SequenceHub>("/sequenceHub");

Console.WriteLine("Waiting for calls...");

await app.RunAsync();