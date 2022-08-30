using Darp.DAQmx.Task.Channel;

namespace Darp.DAQmx.Task.Device;

public static class Usb6210
{
    /// <summary>
    /// Port for digital inputs. Supports 4 lines
    /// <list type="table">
    /// <item> <term>1</term><description>PFI 0/P0.0 (in)</description> </item>
    /// <item> <term>2</term><description>PFI 1/P0.1 (in)</description> </item>
    /// <item> <term>3</term><description>PFI 2/P0.2 (in)</description> </item>
    /// <item> <term>4</term><description>PFI 3/P0.3 (in)</description> </item>
    /// </list>
    /// </summary>
    public static readonly DigitalInputChannel P0 = new(0, 4);

    // 5    D GND

    /// <summary>
    /// Port for digital outputs. Supports 4 lines
    /// <list type="table">
    /// <item> <term>6</term><description>PFI 4/P1.0 (in)</description> </item>
    /// <item> <term>7</term><description>PFI 5/P1.1 (in)</description> </item>
    /// <item> <term>8</term><description>PFI 6/P1.2 (in)</description> </item>
    /// <item> <term>9</term><description>PFI 7/P1.3 (in)</description> </item>
    /// </list>
    /// </summary>
    public static readonly DigitalOutputChannel P1 = new(0, 4);

    // 10    +5 V
    // 11    D GND
    // 12    NC
    // 13    NC
    // 14    RESERVED

    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>15</term><description>AI 0 (AI 0+)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A0 = new(0);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>16</term><description>AI 8 (AI 0-)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A1 = new(1);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>17</term><description>AI 1 (AI 1+)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A2 = new(2);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>18</term><description>AI 9 (AI 1-)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A3 = new(3);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>19</term><description>AI 2 (AI 2+)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A4 = new(4);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>20</term><description>AI 10 (AI 2-)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A5 = new(5);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>21</term><description>AI 3 (AI 3+)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A6 = new(6);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>22</term><description>AI 11 (AI 3-)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A7 = new(7);

    // 23    AI SENSE

    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>24</term><description>AI 4 (AI 4+)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A8 = new(8);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>25</term><description>AI 12 (AI 4-)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A9 = new(9);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>26</term><description>AI 5 (AI 5+)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A10 = new(10);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>27</term><description>AI 13 (AI 5-)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A11 = new(11);

    // 28    AI GND

    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>29</term><description>AI 6 (AI 6+)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A12 = new(12);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>30</term><description>AI 14 (AI 6-)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A13 = new(13);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>31</term><description>AI 7 (AI 7+)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A14 = new(14);
    /// <summary>
    /// Port for analog input.
    /// <list type="table">
    /// <item> <term>32</term><description>AI 15 (AI 7-)</description> </item>
    /// </list>
    /// </summary>
    public static readonly AnalogInputChannel A15 = new(15);
}