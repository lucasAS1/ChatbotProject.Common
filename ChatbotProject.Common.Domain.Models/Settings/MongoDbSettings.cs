using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Security;

namespace ChatbotProject.Common.Domain.Models.Settings;

[ExcludeFromCodeCoverage]
public record MongoDbSettings
{
    public string Url { get; set; } = null!;
    public string Database { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; } = 27017;
    public Scheme Scheme { get; set; } = Scheme.MongoDb;
    public TimeSpan Timeout { get; set; } = new (0, 0, 60);
    public bool UseTls { get; set; } = false;
    public string User { get; set; }
    public string Password { get; set; }
    public string WriteConcern { get; set; }
    public bool ServerVersionSpecified { get; set; }
    public bool RetryWrites { get; set; }
    public string AuthMechanism { get; set; } = "SCRAM-SHA-1";
}

public enum Scheme
{
    [EnumMember(Value = "MongoDb")]
    MongoDb,
    [EnumMember(Value = "MongoDbSrv")]
    MongoDbSrv
}