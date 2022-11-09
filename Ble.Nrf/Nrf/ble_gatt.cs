// ----------------------------------------------------------------------------
// <auto-generated>
// This is autogenerated code by CppSharp.
// Do not edit this file or all your changes will be lost after re-generation.
// </auto-generated>
// ----------------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;
using System.Security;
using __CallingConvention = global::System.Runtime.InteropServices.CallingConvention;
using __IntPtr = global::System.IntPtr;

namespace NrfBleDriver
{
    /// <summary>BLE GATT connection configuration parameters, set with</summary>
    /// <remarks>NRF_ERROR_INVALID_PARAM att_mtu is smaller than</remarks>
    /// <summary>GATT Characteristic Properties.</summary>
    /// <summary>GATT Characteristic Extended Properties.</summary>
    /// <summary>BLE GATT connection configuration parameters, set with</summary>
    /// <remarks>NRF_ERROR_INVALID_PARAM att_mtu is smaller than</remarks>
    public unsafe partial class BleGattConnCfgT : IDisposable
    {
        [StructLayout(LayoutKind.Sequential, Size = 2)]
        public partial struct __Internal
        {
            internal ushort att_mtu;

            [SuppressUnmanagedCodeSecurity, DllImport("NrfBleDriver", EntryPoint = "??0ble_gatt_conn_cfg_t@@QEAA@AEBU0@@Z", CallingConvention = __CallingConvention.Cdecl)]
            internal static extern __IntPtr cctor(__IntPtr __instance, __IntPtr __0);
        }

        public __IntPtr __Instance { get; protected set; }

        internal static readonly new global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::NrfBleDriver.BleGattConnCfgT> NativeToManagedMap =
            new global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::NrfBleDriver.BleGattConnCfgT>();

        internal static void __RecordNativeToManagedMapping(IntPtr native, global::NrfBleDriver.BleGattConnCfgT managed)
        {
            NativeToManagedMap[native] = managed;
        }

        internal static bool __TryGetNativeToManagedMapping(IntPtr native, out global::NrfBleDriver.BleGattConnCfgT managed)
        {
    
            return NativeToManagedMap.TryGetValue(native, out managed);
        }

        protected bool __ownsNativeInstance;

        internal static BleGattConnCfgT __CreateInstance(__IntPtr native, bool skipVTables = false)
        {
            return new BleGattConnCfgT(native.ToPointer(), skipVTables);
        }

        internal static BleGattConnCfgT __GetOrCreateInstance(__IntPtr native, bool saveInstance = false, bool skipVTables = false)
        {
            if (native == __IntPtr.Zero)
                return null;
            if (__TryGetNativeToManagedMapping(native, out var managed))
                return (BleGattConnCfgT)managed;
            var result = __CreateInstance(native, skipVTables);
            if (saveInstance)
                __RecordNativeToManagedMapping(native, result);
            return result;
        }

        internal static BleGattConnCfgT __CreateInstance(__Internal native, bool skipVTables = false)
        {
            return new BleGattConnCfgT(native, skipVTables);
        }

        private static void* __CopyValue(__Internal native)
        {
            var ret = Marshal.AllocHGlobal(sizeof(__Internal));
            *(__Internal*) ret = native;
            return ret.ToPointer();
        }

        private BleGattConnCfgT(__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            __RecordNativeToManagedMapping(__Instance, this);
        }

        protected BleGattConnCfgT(void* native, bool skipVTables = false)
        {
            if (native == null)
                return;
            __Instance = new __IntPtr(native);
        }

        public BleGattConnCfgT()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(global::NrfBleDriver.BleGattConnCfgT.__Internal));
            __ownsNativeInstance = true;
            __RecordNativeToManagedMapping(__Instance, this);
        }

        public BleGattConnCfgT(global::NrfBleDriver.BleGattConnCfgT __0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(global::NrfBleDriver.BleGattConnCfgT.__Internal));
            __ownsNativeInstance = true;
            __RecordNativeToManagedMapping(__Instance, this);
            *((global::NrfBleDriver.BleGattConnCfgT.__Internal*) __Instance) = *((global::NrfBleDriver.BleGattConnCfgT.__Internal*) __0.__Instance);
        }

        public void Dispose()
        {
            Dispose(disposing: true, callNativeDtor : __ownsNativeInstance );
        }

        partial void DisposePartial(bool disposing);

        internal protected virtual void Dispose(bool disposing, bool callNativeDtor )
        {
            if (__Instance == IntPtr.Zero)
                return;
            NativeToManagedMap.TryRemove(__Instance, out _);
            DisposePartial(disposing);
            if (__ownsNativeInstance)
                Marshal.FreeHGlobal(__Instance);
            __Instance = IntPtr.Zero;
        }

        /// <summary>
        /// <para>Maximum size of ATT packet the SoftDevice can send or receive.</para>
        /// <para>The default and minimum value is</para>
        /// </summary>
        /// <remarks>s</remarks>
        public ushort AttMtu
        {
            get
            {
                return ((__Internal*)__Instance)->att_mtu;
            }

            set
            {
                ((__Internal*)__Instance)->att_mtu = value;
            }
        }
    }

    /// <summary>GATT Characteristic Properties.</summary>
    public unsafe partial class BleGattCharPropsT : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 1)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal byte broadcast;

            [FieldOffset(0)]
            internal byte read;

            [FieldOffset(0)]
            internal byte write_wo_resp;

            [FieldOffset(0)]
            internal byte write;

            [FieldOffset(0)]
            internal byte notify;

            [FieldOffset(0)]
            internal byte indicate;

            [FieldOffset(0)]
            internal byte auth_signed_wr;

            [SuppressUnmanagedCodeSecurity, DllImport("NrfBleDriver", EntryPoint = "??0ble_gatt_char_props_t@@QEAA@AEBU0@@Z", CallingConvention = __CallingConvention.Cdecl)]
            internal static extern __IntPtr cctor(__IntPtr __instance, __IntPtr __0);
        }

        public __IntPtr __Instance { get; protected set; }

        internal static readonly new global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::NrfBleDriver.BleGattCharPropsT> NativeToManagedMap =
            new global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::NrfBleDriver.BleGattCharPropsT>();

        internal static void __RecordNativeToManagedMapping(IntPtr native, global::NrfBleDriver.BleGattCharPropsT managed)
        {
            NativeToManagedMap[native] = managed;
        }

        internal static bool __TryGetNativeToManagedMapping(IntPtr native, out global::NrfBleDriver.BleGattCharPropsT managed)
        {
    
            return NativeToManagedMap.TryGetValue(native, out managed);
        }

        protected bool __ownsNativeInstance;

        internal static BleGattCharPropsT __CreateInstance(__IntPtr native, bool skipVTables = false)
        {
            return new BleGattCharPropsT(native.ToPointer(), skipVTables);
        }

        internal static BleGattCharPropsT __GetOrCreateInstance(__IntPtr native, bool saveInstance = false, bool skipVTables = false)
        {
            if (native == __IntPtr.Zero)
                return null;
            if (__TryGetNativeToManagedMapping(native, out var managed))
                return (BleGattCharPropsT)managed;
            var result = __CreateInstance(native, skipVTables);
            if (saveInstance)
                __RecordNativeToManagedMapping(native, result);
            return result;
        }

        internal static BleGattCharPropsT __CreateInstance(__Internal native, bool skipVTables = false)
        {
            return new BleGattCharPropsT(native, skipVTables);
        }

        private static void* __CopyValue(__Internal native)
        {
            var ret = Marshal.AllocHGlobal(sizeof(__Internal));
            *(__Internal*) ret = native;
            return ret.ToPointer();
        }

        private BleGattCharPropsT(__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            __RecordNativeToManagedMapping(__Instance, this);
        }

        protected BleGattCharPropsT(void* native, bool skipVTables = false)
        {
            if (native == null)
                return;
            __Instance = new __IntPtr(native);
        }

        public BleGattCharPropsT()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(global::NrfBleDriver.BleGattCharPropsT.__Internal));
            __ownsNativeInstance = true;
            __RecordNativeToManagedMapping(__Instance, this);
        }

        public BleGattCharPropsT(global::NrfBleDriver.BleGattCharPropsT __0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(global::NrfBleDriver.BleGattCharPropsT.__Internal));
            __ownsNativeInstance = true;
            __RecordNativeToManagedMapping(__Instance, this);
            *((global::NrfBleDriver.BleGattCharPropsT.__Internal*) __Instance) = *((global::NrfBleDriver.BleGattCharPropsT.__Internal*) __0.__Instance);
        }

        public void Dispose()
        {
            Dispose(disposing: true, callNativeDtor : __ownsNativeInstance );
        }

        partial void DisposePartial(bool disposing);

        internal protected virtual void Dispose(bool disposing, bool callNativeDtor )
        {
            if (__Instance == IntPtr.Zero)
                return;
            NativeToManagedMap.TryRemove(__Instance, out _);
            DisposePartial(disposing);
            if (__ownsNativeInstance)
                Marshal.FreeHGlobal(__Instance);
            __Instance = IntPtr.Zero;
        }

        /// <summary>Broadcasting of the value permitted.</summary>
        public byte Broadcast
        {
            get
            {
                return ((__Internal*)__Instance)->broadcast;
            }

            set
            {
                ((__Internal*)__Instance)->broadcast = value;
            }
        }

        /// <summary>Reading the value permitted.</summary>
        public byte Read
        {
            get
            {
                return ((__Internal*)__Instance)->read;
            }

            set
            {
                ((__Internal*)__Instance)->read = value;
            }
        }

        /// <summary>Writing the value with Write Command permitted.</summary>
        public byte WriteWoResp
        {
            get
            {
                return ((__Internal*)__Instance)->write_wo_resp;
            }

            set
            {
                ((__Internal*)__Instance)->write_wo_resp = value;
            }
        }

        /// <summary>Writing the value with Write Request permitted.</summary>
        public byte Write
        {
            get
            {
                return ((__Internal*)__Instance)->write;
            }

            set
            {
                ((__Internal*)__Instance)->write = value;
            }
        }

        /// <summary>Notification of the value permitted.</summary>
        public byte Notify
        {
            get
            {
                return ((__Internal*)__Instance)->notify;
            }

            set
            {
                ((__Internal*)__Instance)->notify = value;
            }
        }

        /// <summary>Indications of the value permitted.</summary>
        public byte Indicate
        {
            get
            {
                return ((__Internal*)__Instance)->indicate;
            }

            set
            {
                ((__Internal*)__Instance)->indicate = value;
            }
        }

        /// <summary>Writing the value with Signed Write Command permitted.</summary>
        public byte AuthSignedWr
        {
            get
            {
                return ((__Internal*)__Instance)->auth_signed_wr;
            }

            set
            {
                ((__Internal*)__Instance)->auth_signed_wr = value;
            }
        }
    }

    /// <summary>GATT Characteristic Extended Properties.</summary>
    public unsafe partial class BleGattCharExtPropsT : IDisposable
    {
        [StructLayout(LayoutKind.Explicit, Size = 1)]
        public partial struct __Internal
        {
            [FieldOffset(0)]
            internal byte reliable_wr;

            [FieldOffset(0)]
            internal byte wr_aux;

            [SuppressUnmanagedCodeSecurity, DllImport("NrfBleDriver", EntryPoint = "??0ble_gatt_char_ext_props_t@@QEAA@AEBU0@@Z", CallingConvention = __CallingConvention.Cdecl)]
            internal static extern __IntPtr cctor(__IntPtr __instance, __IntPtr __0);
        }

        public __IntPtr __Instance { get; protected set; }

        internal static readonly new global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::NrfBleDriver.BleGattCharExtPropsT> NativeToManagedMap =
            new global::System.Collections.Concurrent.ConcurrentDictionary<IntPtr, global::NrfBleDriver.BleGattCharExtPropsT>();

        internal static void __RecordNativeToManagedMapping(IntPtr native, global::NrfBleDriver.BleGattCharExtPropsT managed)
        {
            NativeToManagedMap[native] = managed;
        }

        internal static bool __TryGetNativeToManagedMapping(IntPtr native, out global::NrfBleDriver.BleGattCharExtPropsT managed)
        {
    
            return NativeToManagedMap.TryGetValue(native, out managed);
        }

        protected bool __ownsNativeInstance;

        internal static BleGattCharExtPropsT __CreateInstance(__IntPtr native, bool skipVTables = false)
        {
            return new BleGattCharExtPropsT(native.ToPointer(), skipVTables);
        }

        internal static BleGattCharExtPropsT __GetOrCreateInstance(__IntPtr native, bool saveInstance = false, bool skipVTables = false)
        {
            if (native == __IntPtr.Zero)
                return null;
            if (__TryGetNativeToManagedMapping(native, out var managed))
                return (BleGattCharExtPropsT)managed;
            var result = __CreateInstance(native, skipVTables);
            if (saveInstance)
                __RecordNativeToManagedMapping(native, result);
            return result;
        }

        internal static BleGattCharExtPropsT __CreateInstance(__Internal native, bool skipVTables = false)
        {
            return new BleGattCharExtPropsT(native, skipVTables);
        }

        private static void* __CopyValue(__Internal native)
        {
            var ret = Marshal.AllocHGlobal(sizeof(__Internal));
            *(__Internal*) ret = native;
            return ret.ToPointer();
        }

        private BleGattCharExtPropsT(__Internal native, bool skipVTables = false)
            : this(__CopyValue(native), skipVTables)
        {
            __ownsNativeInstance = true;
            __RecordNativeToManagedMapping(__Instance, this);
        }

        protected BleGattCharExtPropsT(void* native, bool skipVTables = false)
        {
            if (native == null)
                return;
            __Instance = new __IntPtr(native);
        }

        public BleGattCharExtPropsT()
        {
            __Instance = Marshal.AllocHGlobal(sizeof(global::NrfBleDriver.BleGattCharExtPropsT.__Internal));
            __ownsNativeInstance = true;
            __RecordNativeToManagedMapping(__Instance, this);
        }

        public BleGattCharExtPropsT(global::NrfBleDriver.BleGattCharExtPropsT __0)
        {
            __Instance = Marshal.AllocHGlobal(sizeof(global::NrfBleDriver.BleGattCharExtPropsT.__Internal));
            __ownsNativeInstance = true;
            __RecordNativeToManagedMapping(__Instance, this);
            *((global::NrfBleDriver.BleGattCharExtPropsT.__Internal*) __Instance) = *((global::NrfBleDriver.BleGattCharExtPropsT.__Internal*) __0.__Instance);
        }

        public void Dispose()
        {
            Dispose(disposing: true, callNativeDtor : __ownsNativeInstance );
        }

        partial void DisposePartial(bool disposing);

        internal protected virtual void Dispose(bool disposing, bool callNativeDtor )
        {
            if (__Instance == IntPtr.Zero)
                return;
            NativeToManagedMap.TryRemove(__Instance, out _);
            DisposePartial(disposing);
            if (__ownsNativeInstance)
                Marshal.FreeHGlobal(__Instance);
            __Instance = IntPtr.Zero;
        }

        /// <summary>Writing the value with Queued Write operations permitted.</summary>
        public byte ReliableWr
        {
            get
            {
                return ((__Internal*)__Instance)->reliable_wr;
            }

            set
            {
                ((__Internal*)__Instance)->reliable_wr = value;
            }
        }

        /// <summary>Writing the Characteristic User Description descriptor permitted.</summary>
        public byte WrAux
        {
            get
            {
                return ((__Internal*)__Instance)->wr_aux;
            }

            set
            {
                ((__Internal*)__Instance)->wr_aux = value;
            }
        }
    }
}
