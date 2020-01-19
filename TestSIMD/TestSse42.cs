using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Intrinsics.X86;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace TestSIMD {
    [TestClass]
    public unsafe class TestSse42{
        public unsafe struct CRC32:IEquatable<CRC32> {
            private static readonly UInt32[] CRC32Table2 = new UInt32[256];
            private static readonly UInt32* CRC32Table = (UInt32*)GCHandle.Alloc(CRC32Table2,GCHandleType.Pinned).AddrOfPinnedObject();
            static CRC32() {
                const UInt32 poly = 0x1EDC6F41;
                for(UInt32 i = 0;i<256;i++) {
                    var c = i;
                    for(UInt32 j = 0;j<8;j++) {
                        c=(c&1)==1
                            ? 0x1EDC6F41^(c>>1)
                            : c>>1;
                    }
                    CRC32Table[i]=c;
                }
                //for(n=0;n<256;n++) {
                //    sres=n;
                //    for(k=0;k<8;k++)
                //        sres=(sres&1)==1 ? poly^(sres>>1) : (sres>>1);
                //    crcTable[n]=sres;
                //}
                //sres=0xFFFFFFFF;
            }
            public CRC32(UInt32 init) {
                this._HashCode=init;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void PrivateInput(UInt32 入力HashCode) {
                var hashCode = this._HashCode;
                hashCode=CRC32Table[(hashCode^入力HashCode)&0xFF]^(hashCode>>8); 入力HashCode>>=8;
                hashCode=CRC32Table[(hashCode^入力HashCode)&0xFF]^(hashCode>>8); 入力HashCode>>=8;
                hashCode=CRC32Table[(hashCode^入力HashCode)&0xFF]^(hashCode>>8); 入力HashCode>>=8;
                this._HashCode=CRC32Table[(hashCode^入力HashCode)&0xFF]^(hashCode>>8);
            }
            /// <summary>
            /// HashCodeを求めたい値
            /// </summary>
            /// <param name="value"></param>
            /// <typeparam name="T"></typeparam>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Input<T>(T value) => this.PrivateInput(value!=null ? (UInt32)value.GetHashCode() : 0);
            /// <summary>
            /// HashCodeを求めたい値UInt32専用
            /// </summary>
            /// <param name="value"></param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Input(UInt32 value) => this.PrivateInput(value);
            /// <summary>
            /// HashCodeを求めたい値Int32専用
            /// </summary>
            /// <param name="value"></param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Input(Int32 value) => this.Input((UInt32)value);
            /// <summary>
            /// HashCodeを求めたい値UInt16専用
            /// </summary>
            /// <param name="value"></param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Input(UInt16 value) => this.Input((UInt32)value);
            /// <summary>
            /// HashCodeを求めたい値Int16専用
            /// </summary>
            /// <param name="value"></param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Input(Int16 value) => this.Input((UInt32)value);
            /// <summary>
            /// HashCodeを求めたい値Byte専用
            /// </summary>
            /// <param name="value"></param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Input(Byte value) => this.Input((UInt32)value);
            /// <summary>
            /// HashCodeを求めたい値SByte専用
            /// </summary>
            /// <param name="value"></param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Input(SByte value) => this.Input((UInt32)value);
            private UInt32 _HashCode;
            /// <summary>
            /// 同じCRC型で内部のHashCode状態が一致するか
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override Boolean Equals(Object obj) => obj is CRC32 CRC32&&this.Equals(CRC32);
            /// <summary>
            /// 内部のHashCode状態が一致するか
            /// </summary>
            /// <param name="other"></param>
            /// <returns></returns>
            public Boolean Equals(CRC32 other) => this._HashCode==other._HashCode;
            /// <summary>
            /// 内部のHashCodeを返す
            /// </summary>
            /// <returns></returns>
            public override Int32 GetHashCode() => (Int32)this._HashCode;
            /// <summary>
            /// 内部のHashCode状態が一致するか
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static Boolean operator ==(CRC32 left,CRC32 right) => left.Equals(right);
            /// <summary>
            /// 内部のHashCode状態が不一致するか
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            /// <returns></returns>
            public static Boolean operator !=(CRC32 left,CRC32 right) => !left.Equals(right);
        }
        private const Int32 Count = 3;
        [TestMethod]
        public void Crc32() {
            //var left = Vector256.Create(a+0,a+1,a+2,a+3,a+4,a+5,a+6,a+7,a+8,a+9,a+10,a+11,a+12,a+13,a+14,a+15);
            //var right = Vector256.Create(0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15);
            var CRC32 = new CRC32(0);
            var actual_Crc32 = 0U;
            for(var a = 0U;a<2;a++) {
                CRC32.Input(a);
                actual_Crc32=Sse42.Crc32(actual_Crc32,a);
            }
            var expected_Crc32=CRC32.GetHashCode();
            Assert.AreEqual(expected_Crc32,actual_Crc32);
        }
    }
}
