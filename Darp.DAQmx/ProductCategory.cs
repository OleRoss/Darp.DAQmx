namespace Darp.DAQmx;

  /// <summary>Indicates the product category of the device. This category corresponds to the category displayed in MAX when creating NI-DAQmx simulated devices.</summary>
  /// <remarks>Indicates the product category of the device. This category corresponds to the category displayed in MAX when creating NI-DAQmx simulated devices.  Use this enumeration to get or set the value of <see cref="P:NationalInstruments.DAQmx.Device.ProductCategory" />.</remarks>
  public enum ProductCategory
  {
    /// <summary>Unknown category.</summary>
    Unknown = 12588, // 0x0000312C
    /// <summary>E Series DAQ.</summary>
    ESeriesDaq = 14642, // 0x00003932
    /// <summary>M Series DAQ.</summary>
    MSeriesDaq = 14643, // 0x00003933
    /// <summary>S Series DAQ.</summary>
    SSeriesDaq = 14644, // 0x00003934
    /// <summary>SC Series DAQ.</summary>
    SCSeriesDaq = 14645, // 0x00003935
    /// <summary>USB DAQ.</summary>
    UsbDaq = 14646, // 0x00003936
    /// <summary>AO Series.</summary>
    AOSeries = 14647, // 0x00003937
    /// <summary>Digital I/O.</summary>
    DigitalIO = 14648, // 0x00003938
    /// <summary>Dynamic Signal Acquisition.</summary>
    DynamicSignalAcquisition = 14649, // 0x00003939
    /// <summary>Switches.</summary>
    Switches = 14650, // 0x0000393A
    /// <summary>CompactDAQ chassis.</summary>
    CompactDaqChassis = 14658, // 0x00003942
    /// <summary>C Series I/O module.</summary>
    CSeriesModule = 14659, // 0x00003943
    /// <summary>SCXI module.</summary>
    ScxiModule = 14660, // 0x00003944
    /// <summary>TIO Series.</summary>
    TioSeries = 14661, // 0x00003945
    /// <summary>B Series DAQ.</summary>
    BSeriesDaq = 14662, // 0x00003946
    /// <summary>SCC Connector Block.</summary>
    SccConnectorBlock = 14704, // 0x00003970
    /// <summary>SCC Module.</summary>
    SccModule = 14705, // 0x00003971
    /// <summary>NI ELVIS.</summary>
    NIElvis = 14755, // 0x000039A3
    /// <summary>Network DAQ.</summary>
    NetworkDAQ = 14829, // 0x000039ED
    /// <summary>X Series DAQ.</summary>
    XSeriesDaq = 15858, // 0x00003DF2
    /// <summary>SC Express.</summary>
    SCExpress = 15886, // 0x00003E0E
    /// <summary>CompactRIO Chassis.</summary>
    CompactRioChassis = 16144, // 0x00003F10
    /// <summary>FieldDAQ.</summary>
    FieldDaq = 16151, // 0x00003F17
  }