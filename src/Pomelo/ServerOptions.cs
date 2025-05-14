namespace Pomelo
{
    public class ServerOptions
    {
        public AddressBindType BindType { get; set; } = AddressBindType.Loopback;

        public int Port { get; set; } = 5566;

        public string SpecialAddress { get; set; } = "127.0.0.1";

        public string StartupWords { get; set; } = "Pomelo Server bind at {0} \r\n";

        public string ServerName { get; set; } = "Pomelo Server";

        public string? Certificate { get; set; }

        public string? CertificatePassword { get; set; }
    }
    public enum AddressBindType
    {
        Any, //0.0.0.0 Address.Any
        Loopback,
        InternalAddress,
        SpecialAddress
    }
}
