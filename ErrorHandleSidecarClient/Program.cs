﻿using ErrorHandleSidecar.Protos;
using Grpc.Net.Client;

const string baseUrl = "http://localhost:5148";

using var channel = GrpcChannel.ForAddress(baseUrl);
var client = new ErrorHandler.ErrorHandlerClient(channel);

var errorRequest = new ErrorRequest { ErrorCode = "1005" };

var reply = await client.GetErrorResponseAsync(errorRequest);

Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");

Console.ReadKey();