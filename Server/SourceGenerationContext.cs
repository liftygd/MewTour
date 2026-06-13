using System.Collections.Generic;
using System.Text.Json.Serialization;
using MewTour.Server;

namespace MewTour;

[JsonSourceGenerationOptions(WriteIndented = false, PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(List<ServerConfig>))]
[JsonSerializable(typeof(List<AuthModel>))]
internal partial class SourceGenerationContext : JsonSerializerContext { }