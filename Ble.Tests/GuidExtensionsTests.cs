using Ble.Utils;
using Ble.Uuid;
using FluentAssertions;

namespace Ble.Tests;

public class GuidExtensionsTests
{
    private static readonly byte[] GuidBytes = { 0,0,0,0,0,0,0,16,128,0,0,128,95,155,52,251 };

    [Theory]
    [InlineData("FD4C")]
    [InlineData("fd4c")]
    [InlineData("FD4CFD4C")]
    [InlineData("FD4CFD4CFD4CFD4CFD4CFD4CFD4CFD4C")]
    public void TestToBleGuid(string byteString)
    {
        byte[] bytes = byteString.ToByteArray();
        byte[] expectedBytes = GuidBytes.ToArray();
        for (var i = 0; i < bytes.Length; i++) expectedBytes[i] = bytes[i];

        Guid guid = bytes.ToBleGuid();

        guid.ToByteArray().Should().BeEquivalentTo(expectedBytes);
    }

    [Fact]
    public void VerifyGattGuid()
    {
        var gattUuid = GattUuid.ClientCharacteristicConfiguration;
        byte[] expectedBytes = GuidBytes.ToArray();
        expectedBytes[0] = 02;
        expectedBytes[1] = 41;

        Guid guid = gattUuid.ToBleGuid();

        guid.ToByteArray().Should().BeEquivalentTo(expectedBytes);
    }
}