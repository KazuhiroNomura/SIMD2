using System;
//using System.Runtime.Intrinsics.X86;
using System.Runtime.Intrinsics;
using TestSIMD;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
namespace SIMDPerformance {
    [StructLayout(LayoutKind.Sequential), DebuggerDisplay("{B}")]
    struct ABC {
        public Int32 A, B, C;
    }
    unsafe class Program {
        private const Int32 レコード数 = 1*1000*1000;//300000;
        private const Int32 繰り返し数 = 1;
        private readonly static Int32 Int32ベクタ長 = Vector256<Int32>.Count;
        static void Main() {
            var TestAvx2 = new TestAvx2();
            var TestSse42 = new TestSse42();
            //TestSse42.Crc32();
            static ABC[] AOS作成() {
                Console.WriteLine("AOS作成開始");
                var AOS = new ABC[レコード数];
                for(var i = 0;i<AOS.Length;i++) {
                    AOS[i].B=(Byte)i;
                }
                Console.WriteLine("AOS作成終了");
                return AOS;
            }
            var AOS = AOS作成();
            var Bオフセット = Marshal.OffsetOf<ABC>(nameof(ABC.B)).ToInt32();
            var index = Vector256.Create(
                sizeof(ABC)*0+Bオフセット,
                sizeof(ABC)*1+Bオフセット,
                sizeof(ABC)*2+Bオフセット,
                sizeof(ABC)*3+Bオフセット,
                sizeof(ABC)*4+Bオフセット,
                sizeof(ABC)*5+Bオフセット,
                sizeof(ABC)*6+Bオフセット,
                sizeof(ABC)*7+Bオフセット
            );
            var Count = (レコード数+Int32ベクタ長-1)/Int32ベクタ長;
            Vector256<Int32> Vector256Sum0 = default;
            Vector256<Int32> Vector256Sum1 = default;
            Vector256<Int32> Vector256Sum2 = default;
            Vector256<Int32> Vector256Sum3 = default;
            Vector256<Int32> Vector256Sum4 = default;
            Vector256<Int32> Vector256Sum5 = default;
            Vector256<Int32> Vector256Sum6 = default;
            Vector256<Int32> Vector256Sum7 = default;
            var watch = Stopwatch.StartNew();
            fixed(ABC* pAOS = AOS) {
                for(var a = 0;a<繰り返し数;a++) {
                    for(var b = 0;b<Count;b+=Int32ベクタ長) {
                        //Trace.WriteLine("ReadKey()1");
                        //Console.ReadKey();
                        //Trace.WriteLine("ReadKey()2");
                        //Debugger.Break();
                        var Vector256_0 = Avx2.GatherVector256((Int32*)&pAOS[(b+0)*Int32ベクタ長],index,1);
                        var Vector256_1 = Avx2.GatherVector256((Int32*)&pAOS[(b+1)*Int32ベクタ長],index,1);
                        var Vector256_2 = Avx2.GatherVector256((Int32*)&pAOS[(b+2)*Int32ベクタ長],index,1);
                        var Vector256_3 = Avx2.GatherVector256((Int32*)&pAOS[(b+3)*Int32ベクタ長],index,1);
                        var Vector256_4 = Avx2.GatherVector256((Int32*)&pAOS[(b+4)*Int32ベクタ長],index,1);
                        var Vector256_5 = Avx2.GatherVector256((Int32*)&pAOS[(b+5)*Int32ベクタ長],index,1);
                        var Vector256_6 = Avx2.GatherVector256((Int32*)&pAOS[(b+6)*Int32ベクタ長],index,1);
                        var Vector256_7 = Avx2.GatherVector256((Int32*)&pAOS[(b+7)*Int32ベクタ長],index,1);
                        Vector256Sum0=Avx2.Add(Vector256Sum0,Vector256_0);
                        Vector256Sum1=Avx2.Add(Vector256Sum1,Vector256_1);
                        Vector256Sum2=Avx2.Add(Vector256Sum2,Vector256_2);
                        Vector256Sum3=Avx2.Add(Vector256Sum3,Vector256_3);
                        Vector256Sum4=Avx2.Add(Vector256Sum4,Vector256_4);
                        Vector256Sum5=Avx2.Add(Vector256Sum5,Vector256_5);
                        Vector256Sum6=Avx2.Add(Vector256Sum6,Vector256_6);
                        Vector256Sum7=Avx2.Add(Vector256Sum7,Vector256_7);
                    }
                }
            }
            watch.Stop();
            var Sum = 0;
            for(var a = 0;a<Int32ベクタ長;a++) {
                Sum+=Vector256Sum0.GetElement(a);
                Sum+=Vector256Sum1.GetElement(a);
                Sum+=Vector256Sum2.GetElement(a);
                Sum+=Vector256Sum3.GetElement(a);
                Sum+=Vector256Sum4.GetElement(a);
                Sum+=Vector256Sum5.GetElement(a);
                Sum+=Vector256Sum6.GetElement(a);
                Sum+=Vector256Sum7.GetElement(a);
            }
            Debug.WriteLine($"Sum={Sum}");
            Debug.WriteLine($"for {繰り返し数} loops: {watch.ElapsedMilliseconds}ms");
            return;
            TestAvx2.AOSからSIMD合計ループ8展開();
            //TestAvx2.Add2_Byte();
            //TestAvx2.Add2_Double();
            //TestAvx2.Add2_Single();
            //TestAvx2.Add2_Int32();
            //TestAvx2.Add2_Int64();
            //TestAvx2.Add2_SByte();
            TestAvx2.Add2_Int16();
            TestAvx2.Add1();
        }
    }
}
