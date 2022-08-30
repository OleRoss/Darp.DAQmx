using Darp.DAQmx.Task.Analog.Channel;
using Darp.DAQmx.Task.Counter;
using Darp.DAQmx.Task.Digital.Channel;

namespace Darp.DAQmx.Device;

public static class Usb6001
{
    // +++++----- LEFT SIDE -----++++++

    // L1    AI GND

    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>L2</term><description>AI 0 (AI 0+)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel AI0 = new(0);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>L3</term><description>AI 4 (AI 0-)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel AI4 = new(4);

    // L4    AI GND

    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>L5</term><description>AI 1 (AI 1+)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel AI1 = new(1);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>L6</term><description>AI 5 (AI 1-)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel AI5 = new(5);

    // L7    AI GND

    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>L8</term><description>AI 2 (AI 2+)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel AI2 = new(2);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>L9</term><description>AI 6 (AI 2-)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel AI6 = new(6);

    // L10    AI GND

    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>L11</term><description>AI 3 (AI 3+)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel AI3 = new(3);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>L12</term><description>AI 7 (AI 3-)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel AI7 = new(7);

    // L13    AI GND

    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>L14</term><description>AO 0</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogChannel AO0 = new(0);    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>L15</term><description>AO 1</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogChannel AO1 = new(1);

    // L16    AO GND

    // +++++----- RIGHT SIDE -----++++++

    /// <summary>
    /// Port for digital inputs. Supports 8 lines
    /// <list type="table">
    /// <item> <term>R1</term><description>P0.0</description> </item>
    /// <item> <term>R2</term><description>P0.1</description> </item>
    /// <item> <term>R3</term><description>P0.2</description> </item>
    /// <item> <term>R4</term><description>P0.3</description> </item>
    /// <item> <term>R5</term><description>P0.4</description> </item>
    /// <item> <term>R6</term><description>P0.5</description> </item>
    /// <item> <term>R7</term><description>P0.6</description> </item>
    /// <item> <term>R8</term><description>P0.7</description> </item>
    /// </list>
    /// </summary>
    public static readonly DigitalInputChannel P0 = new(0, 8);

    /// <summary>
    /// Port for digital inputs. Supports 4 lines
    /// <list type="table">
    /// <item> <term>R9</term><description>P1.0</description> </item>
    /// <item> <term>R10</term><description>P1.1 / PFI 1</description> </item>
    /// <item> <term>R11</term><description>P1.2</description> </item>
    /// <item> <term>R12</term><description>P1.3</description> </item>
    /// </list>
    /// </summary>
    public static readonly DigitalInputChannel P1 = new(0, 4);
    /// <summary>
    /// R10    PFI port
    /// </summary>
    public static readonly ProgrammableFunctionChannel PFI1 = new(1);

    /// <summary>
    /// Port for digital inputs. Supports 1 line
    /// <list type="table">
    /// <item> <term>R13</term><description>P2.0 / PFI 0</description> </item>
    /// </list>
    /// </summary>
    public static readonly DigitalInputChannel P2 = new(0, 1);
    /// <summary>
    /// R13    PFI port
    /// </summary>
    public static readonly ProgrammableFunctionChannel PFI0 = new(0);

    // R14    D GND
    // R15    + 5V
    // R16    D GND

}