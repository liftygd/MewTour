using System.Collections.Generic;
using System.Text.Json.Serialization;
using MewTour.Server;

namespace MewTour;

[JsonSourceGenerationOptions(WriteIndented = false, PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(List<ServerConfig>))]
[JsonSerializable(typeof(List<AuthModel>))]
[JsonSerializable(typeof(List<ErrorModel>))]
[JsonSerializable(typeof(List<string>))]
internal partial class SourceGenerationContext : JsonSerializerContext { }