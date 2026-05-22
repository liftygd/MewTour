using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MewTour;

[JsonSourceGenerationOptions(WriteIndented = false)]
[JsonSerializable(typeof(List<ServerConfig>))]
internal partial class SourceGenerationContext : JsonSerializerContext { }