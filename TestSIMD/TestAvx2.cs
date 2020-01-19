using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace TestSIMD {
    [TestClass]
    public unsafe class TestAvx2 {
        //private static Vector256<T>Add<T>(T left,T right)where T:struct{

        //}
        private const Int32 Count = 10000;
        [TestMethod]
        public void Add1() {
            //var left = Vector256.Create(a+0,a+1,a+2,a+3,a+4,a+5,a+6,a+7,a+8,a+9,a+10,a+11,a+12,a+13,a+14,a+15);
            //var right = Vector256.Create(0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15);
            for(var left0 = 0;left0<1;left0++) {
                var left1 = (Byte)left0;
                var left2 = Vector256.Create(left1);
                for(var right0 = 0;right0<1;right0++) {
                    var right1 = (Byte)right0;
                    var right2 = Vector256.Create(right1);
                    var actual = Avx2.Add(left2,right2);
                    for(var index = 0;index<32;index++) {
                        Assert.AreEqual((Byte)(left0+right0),actual.GetElement(index));
                    }
                }
            }
        }
        [TestMethod]
        public void Add2_Byte() {
            for(var left_lower0 = 0;left_lower0<1;left_lower0++) {
                var left_lower1 = (Byte)left_lower0;
                var left_lower2 = Vector128.Create(left_lower1);
                for(var left_upper0 = 0;left_upper0<1;left_upper0++) {
                    var left_upper1 = (Byte)left_upper0;
                    var left_upper2 = Vector128.Create(left_upper1);
                    var left3 = Vector256.Create(left_lower2,left_upper2);
                    for(var right_lower0 = 0;right_lower0<1;right_lower0++) {
                        var right_lower1 = (Byte)right_lower0;
                        var right_lower2 = Vector128.Create(right_lower1);
                        for(var right_upper0 = 0;right_upper0<1;right_upper0++) {
                            var right_upper1 = (Byte)right_upper0;
                            var right_upper2 = Vector128.Create(right_upper1);
                            var right3 = Vector256.Create(right_lower2,right_upper2);
                            var actual = Avx2.Add(left3,right3);
                            var expected_upper0 = (UInt64)(left_upper0+right_upper0);
                            var expected_upper1 =
                                (expected_upper0<<0)|
                                (expected_upper0<<8)|
                                (expected_upper0<<16)|
                                (expected_upper0<<24)|
                                (expected_upper0<<32)|
                                (expected_upper0<<40)|
                                (expected_upper0<<48)|
                                (expected_upper0<<56);
                            var expected_upper2 = Vector128.Create(expected_upper1);
                            var expected_lower0 = (UInt64)(left_lower0+right_lower0);
                            var expected_lower1 =
                                (expected_lower0<<0)|
                                (expected_lower0<<8)|
                                (expected_lower0<<16)|
                                (expected_lower0<<24)|
                                (expected_lower0<<32)|
                                (expected_lower0<<40)|
                                (expected_lower0<<48)|
                                (expected_lower0<<56);
                            var expected_lower2 = Vector128.Create(expected_lower1);
                            var expected3 = Vector256.Create(expected_lower2,expected_upper2).AsByte();
                            for(var index = 0;index<32;index++) {
                                Assert.AreEqual(expected3.GetElement(index),actual.GetElement(index));
                            }
                        }
                    }
                }
            }
        }
        [TestMethod]
        public void Add2_Double() {
            for(var left_lower0 = 0;left_lower0<1;left_lower0++) {
                var left_lower1 = (Double)left_lower0;
                var left_lower2 = Vector128.Create(left_lower1);
                for(var left_upper0 = 0;left_upper0<1;left_upper0++) {
                    var left_upper1 = (Double)left_upper0;
                    var left_upper2 = Vector128.Create(left_upper1);
                    var left3 = Vector256.Create(left_lower2,left_upper2);
                    for(var right_lower0 = 0;right_lower0<1;right_lower0++) {
                        var right_lower1 = (Double)right_lower0;
                        var right_lower2 = Vector128.Create(right_lower1);
                        for(var right_upper0 = 0;right_upper0<1;right_upper0++) {
                            var right_upper1 = (Double)right_upper0;
                            var right_upper2 = Vector128.Create(right_upper1);
                            var right3 = Vector256.Create(right_lower2,right_upper2);
                            var actual = Avx2.Add(left3,right3);
                            var expected_upper0 = (UInt64)(left_upper0+right_upper0);
                            var expected_upper1 = (Double)expected_upper0;
                            var expected_upper2 = Vector128.Create(expected_upper1);
                            var expected_lower0 = (UInt64)(left_lower0+right_lower0);
                            var expected_lower1 = (Double)expected_lower0;
                            var expected_lower2 = Vector128.Create(expected_lower1);
                            var expected3 = Vector256.Create(expected_lower2,expected_upper2).AsDouble();
                            for(var index = 0;index<4;index++) {
                                Assert.AreEqual(expected3.GetElement(index),actual.GetElement(index));
                            }
                        }
                    }
                }
            }
        }
        [TestMethod]
        public void Add2_Single() {
            for(var left_lower0 = 0;left_lower0<1;left_lower0++) {
                var left_lower1 = (Single)left_lower0;
                var left_lower2 = Vector128.Create(left_lower1);
                for(var left_upper0 = 0;left_upper0<1;left_upper0++) {
                    var left_upper1 = (Single)left_upper0;
                    var left_upper2 = Vector128.Create(left_upper1);
                    var left3 = Vector256.Create(left_lower2,left_upper2);
                    for(var right_lower0 = 0;right_lower0<1;right_lower0++) {
                        var right_lower1 = (Single)right_lower0;
                        var right_lower2 = Vector128.Create(right_lower1);
                        for(var right_upper0 = 0;right_upper0<1;right_upper0++) {
                            var right_upper1 = (Single)right_upper0;
                            var right_upper2 = Vector128.Create(right_upper1);
                            var right3 = Vector256.Create(right_lower2,right_upper2);
                            var actual = Avx2.Add(left3,right3);
                            var expected_upper0 = (UInt64)(left_upper0+right_upper0);
                            var expected_upper1 = (Single)expected_upper0;
                            var expected_upper2 = Vector128.Create(expected_upper1);
                            var expected_lower0 = (UInt64)(left_lower0+right_lower0);
                            var expected_lower1 = (Single)expected_lower0;
                            var expected_lower2 = Vector128.Create(expected_lower1);
                            var expected3 = Vector256.Create(expected_lower2,expected_upper2).AsSingle();
                            for(var index = 0;index<8;index++) {
                                Assert.AreEqual(expected3.GetElement(index),actual.GetElement(index));
                            }
                        }
                    }
                }
            }
        }
        [TestMethod]
        public void Add2_Int32() {
            for(var left_lower0 = 0;left_lower0<1;left_lower0++) {
                var left_lower1 = left_lower0;
                var left_lower2 = Vector128.Create(left_lower1);
                for(var left_upper0 = 0;left_upper0<1;left_upper0++) {
                    var left_upper1 = left_upper0;
                    var left_upper2 = Vector128.Create(left_upper1);
                    var left3 = Vector256.Create(left_lower2,left_upper2);
                    for(var right_lower0 = 0;right_lower0<1;right_lower0++) {
                        var right_lower1 = right_lower0;
                        var right_lower2 = Vector128.Create(right_lower1);
                        for(var right_upper0 = 0;right_upper0<1;right_upper0++) {
                            var right_upper1 = right_upper0;
                            var right_upper2 = Vector128.Create(right_upper1);
                            var right3 = Vector256.Create(right_lower2,right_upper2);
                            var actual = Avx2.Add(left3,right3);
                            var expected_upper0 = (UInt64)(left_upper0+right_upper0);
                            var expected_upper1 = (Int32)expected_upper0;
                            var expected_upper2 = Vector128.Create(expected_upper1);
                            var expected_lower0 = (UInt64)(left_lower0+right_lower0);
                            var expected_lower1 = (Int32)expected_lower0;
                            var expected_lower2 = Vector128.Create(expected_lower1);
                            var expected3 = Vector256.Create(expected_lower2,expected_upper2).AsInt32();
                            for(var index = 0;index<8;index++) {
                                Assert.AreEqual(expected3.GetElement(index),actual.GetElement(index));
                            }
                        }
                    }
                }
            }
        }
        [TestMethod]
        public void Add2_Int64() {
            for(var left_lower0 = 0;left_lower0<1;left_lower0++) {
                var left_lower1 = (Int64)left_lower0;
                var left_lower2 = Vector128.Create(left_lower1);
                for(var left_upper0 = 0;left_upper0<1;left_upper0++) {
                    var left_upper1 = (Int64)left_upper0;
                    var left_upper2 = Vector128.Create(left_upper1);
                    var left3 = Vector256.Create(left_lower2,left_upper2);
                    for(var right_lower0 = 0;right_lower0<1;right_lower0++) {
                        var right_lower1 = (Int64)right_lower0;
                        var right_lower2 = Vector128.Create(right_lower1);
                        for(var right_upper0 = 0;right_upper0<1;right_upper0++) {
                            var right_upper1 = (Int64)right_upper0;
                            var right_upper2 = Vector128.Create(right_upper1);
                            var right3 = Vector256.Create(right_lower2,right_upper2);
                            var actual = Avx2.Add(left3,right3);
                            var expected_upper0 = (UInt64)(left_upper0+right_upper0);
                            var expected_upper1 = (Int64)expected_upper0;
                            var expected_upper2 = Vector128.Create(expected_upper1);
                            var expected_lower0 = (UInt64)(left_lower0+right_lower0);
                            var expected_lower1 = (Int64)expected_lower0;
                            var expected_lower2 = Vector128.Create(expected_lower1);
                            var expected3 = Vector256.Create(expected_lower2,expected_upper2).AsInt64();
                            for(var index = 0;index<4;index++) {
                                Assert.AreEqual(expected3.GetElement(index),actual.GetElement(index));
                            }
                        }
                    }
                }
            }
        }
        [TestMethod]
        public void Add2_SByte() {
            for(var left_lower0 = 0;left_lower0<1;left_lower0++) {
                var left_lower1 = (SByte)left_lower0;
                var left_lower2 = Vector128.Create(left_lower1);
                for(var left_upper0 = 0;left_upper0<1;left_upper0++) {
                    var left_upper1 = (SByte)left_upper0;
                    var left_upper2 = Vector128.Create(left_upper1);
                    var left3 = Vector256.Create(left_lower2,left_upper2);
                    for(var right_lower0 = 0;right_lower0<1;right_lower0++) {
                        var right_lower1 = (SByte)right_lower0;
                        var right_lower2 = Vector128.Create(right_lower1);
                        for(var right_upper0 = 0;right_upper0<1;right_upper0++) {
                            var right_upper1 = (SByte)right_upper0;
                            var right_upper2 = Vector128.Create(right_upper1);
                            var right3 = Vector256.Create(right_lower2,right_upper2);
                            var actual = Avx2.Add(left3,right3);
                            var expected_upper0 = (UInt64)(left_upper0+right_upper0);
                            var expected_upper1 =
                                (expected_upper0<<0)|
                                (expected_upper0<<8)|
                                (expected_upper0<<16)|
                                (expected_upper0<<24)|
                                (expected_upper0<<32)|
                                (expected_upper0<<40)|
                                (expected_upper0<<48)|
                                (expected_upper0<<56);
                            var expected_upper2 = Vector128.Create(expected_upper1);
                            var expected_lower0 = (UInt64)(left_lower0+right_lower0);
                            var expected_lower1 =
                                (expected_lower0<<0)|
                                (expected_lower0<<8)|
                                (expected_lower0<<16)|
                                (expected_lower0<<24)|
                                (expected_lower0<<32)|
                                (expected_lower0<<40)|
                                (expected_lower0<<48)|
                                (expected_lower0<<56);
                            var expected_lower2 = Vector128.Create(expected_lower1);
                            var expected3 = Vector256.Create(expected_lower2,expected_upper2).AsSByte();
                            for(var index = 0;index<32;index++) {
                                Assert.AreEqual(expected3.GetElement(index),actual.GetElement(index));
                            }
                        }
                    }
                }
            }
        }
        [TestMethod]
        public void Add2_Int16() {
            for(var left_0_0 = 0;left_0_0<1;left_0_0++) {
                var left_0_1 = (Int16)left_0_0;
                var left_64_0 = Vector64.Create(left_0_1);
                for(var left_1_0 = 0;left_1_0<1;left_1_0++) {
                    var left_1_1 = (Int16)left_1_0;
                    var left_64_1 = Vector64.Create(left_1_1);
                    var left_128_0 = Vector128.Create(left_64_0,left_64_1);
                    for(var left_2_0 = 0;left_2_0<1;left_2_0++) {
                        var left_2_1 = (Int16)left_2_0;
                        var left_64_2 = Vector64.Create(left_2_1);
                        for(var left_3_0 = 0;left_3_0<1;left_3_0++) {
                            var left_3_1 = (Int16)left_3_0;
                            var left_64_3 = Vector64.Create(left_3_1);
                            var left_128_1 = Vector128.Create(left_64_2,left_64_3);
                            var left_256 = Vector256.Create(left_128_0,left_128_1);
                            for(var right_0_0 = 0;right_0_0<1;right_0_0++) {
                                var right_0_1 = (Int16)right_0_0;
                                var right_64_0 = Vector64.Create(right_0_1);
                                for(var right_1_0 = 0;right_1_0<1;right_1_0++) {
                                    var right_1_1 = (Int16)right_1_0;
                                    var right_64_1 = Vector64.Create(right_1_1);
                                    var right_128_0 = Vector128.Create(right_64_0,right_64_1);
                                    for(var right_2_0 = 0;right_2_0<1;right_2_0++) {
                                        var right_2_1 = (Int16)right_2_0;
                                        var right_64_2 = Vector64.Create(right_2_1);
                                        for(var right_3_0 = 0;right_3_0<1;right_3_0++) {
                                            var right_3_1 = (Int16)right_3_0;
                                            var right_64_3 = Vector64.Create(right_3_1);
                                            var right_128_1 = Vector128.Create(right_64_2,right_64_3);
                                            var right_256 = Vector256.Create(right_128_0,right_128_1);
                                            var actual = Avx2.Add(left_256,right_256);
                                            //var expected_upper0 = (UInt64)(left_0_1+left_1_1+actual_5_1+actual_4_1);
                                            //var expected_upper1 =
                                            //    (expected_upper0<<0)|
                                            //    (expected_upper0<<16)|
                                            //    (expected_upper0<<32)|
                                            //    (expected_upper0<<48);
                                            //var expected_upper2 = Vector128.Create(expected_upper1);
                                            //var expected_lower0 = (UInt64)(actual_7_1+actual_6_1+actual_5_1+actual_4_1);
                                            //var expected_lower1 =
                                            //    (expected_lower0<<0)|
                                            //    (expected_lower0<<16)|
                                            //    (expected_lower0<<32)|
                                            //    (expected_lower0<<48);
                                            //var expected_lower2 = Vector128.Create(expected_lower1);
                                            //var expected3 = Vector256.Create(expected_lower2,expected_upper2).AsInt16();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// o[0]=a[0]+a[1],o[1]=a[2]+a[3]
        /// o[8]=b[0]+b[1],o[9]=b[2]+b[3]
        /// </summary>
        [TestMethod]
        public void 水平加算Int16() {
            for(var a = 0;a<1;a++) {
                var operand0 = Vector256.Create(0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15);
                var operand1 = Vector256.Create(15,14,13,12,11,10,9,8,7,6,5,4,3,2,1,0);
                for(var b = 0;b<1;b++) {
                    var result = Avx2.HorizontalAdd(operand0,operand1);
                }
            }
            for(var a = 0;a<1;a++) {
                var operand0 = Vector256.Create(0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15);
                var operand1 = Vector256.Create(15,14,13,12,11,10,9,8,7,6,5,4,3,2,1,0);
                var result = Vector256.Create(
                    (Byte)(operand0.GetElement(0)+operand0.GetElement(1)),(Byte)(operand0.GetElement(2)+operand0.GetElement(3)),(Byte)(operand0.GetElement(4)+operand0.GetElement(5)),(Byte)(operand0.GetElement(6)+operand0.GetElement(7)),(Byte)(operand0.GetElement(8)+operand0.GetElement(9)),(Byte)(operand0.GetElement(10)+operand0.GetElement(11)),(Byte)(operand0.GetElement(12)+operand0.GetElement(13)),(Byte)(operand0.GetElement(14)+operand0.GetElement(15)),
                    (Byte)(operand1.GetElement(0)+operand1.GetElement(1)),(Byte)(operand1.GetElement(2)+operand1.GetElement(3)),(Byte)(operand1.GetElement(4)+operand1.GetElement(5)),(Byte)(operand1.GetElement(6)+operand1.GetElement(7)),(Byte)(operand1.GetElement(8)+operand1.GetElement(9)),(Byte)(operand1.GetElement(10)+operand1.GetElement(11)),(Byte)(operand1.GetElement(12)+operand1.GetElement(13)),(Byte)(operand1.GetElement(14)+operand1.GetElement(15))
                );
            }
        }
        private const Int32 要素数 = 1<<26;
        private static Int16[] データ作成() {
            var data = new Int16[要素数];
            for(var a = 0;a<要素数;a++) {
                data[a]=(Int16)a;
            }
            return data;
        }
        [StructLayout(LayoutKind.Sequential),DebuggerDisplay("{B}")]
        struct ABC {
            public Int32 A, B, C;
        }
        //static Int32 StructOfArray(ABC[] ABC配列,Int32[] Bオフセット配列,Int32[] B配列) {
        //    Int32 y = 0;
        //    //ar source256= MemoryMarshal.Cast<Int32,Record>(index);
        //    var iv = MemoryMarshal.Cast<Int32,Vector256<Int32>>(Bオフセット配列)[0];
        //    var rv = MemoryMarshal.Cast<Int32,Vector256<Int32>>(B配列);

        //    unsafe {
        //        fixed(ABC* pABC配列 = ABC配列) {
        //            for(var i = 0;i<rv.Length;i++) {
        //                rv[i]=Avx2.GatherVector256((Int32*)&pABC配列[i*Vector256<Int32>.Count],iv,1);
        //            }
        //        }
        //    }
        //    // move past everything we've processed via SIMD
        //    y+=rv.Length*Vector256<Int32>.Count;
        //    // now do anything left, which includes anything not aligned to 256 bits,
        //    // plus the "no AVX2" scenario
        //    var result = y;
        //    var end = B配列.Length; // hoist, since this is not the JIT recognized pattern
        //    for(;y<end;y++) {
        //        B配列[y]=ABC配列[y].B;
        //    }
        //    return result;
        //}
        static void AOSからSOA(ABC[] AOS,Vector256<Int32> index,Vector256<Int32>[] SOA) {
            fixed(ABC* pAOS= AOS) {
                for(var i = 0;i<SOA.Length;i++) {
                    SOA[i]=Avx2.GatherVector256((Int32*)&pAOS[i*Vector256<Int32>.Count],index,1);
                }
            }
        }
        private const Int32 レコード数 = 1*1000*1000;//300000;
        private const Int32 繰り返し数 = 1;
        private readonly static Int32 Int32ベクタ長 = Vector256<Int32>.Count;
        private static ABC[] AOS作成() {
            Console.WriteLine("AOS作成開始");
            var AOS = new ABC[レコード数];
            for(var i = 0;i<AOS.Length;i++) {
                AOS[i].B=(Byte)i;
            }
            Console.WriteLine("AOS作成終了");
            return AOS;
        }
        [TestMethod]
        public void AOSからSIMD合計ループ8展開() {
            var AOS = AOS作成();
            var Bオフセット = Marshal.OffsetOf<ABC>(nameof(ABC.B)).ToInt32();
            var index =Vector256.Create(
                sizeof(ABC)*0+Bオフセット,
                sizeof(ABC)*1+Bオフセット,
                sizeof(ABC)*2+Bオフセット,
                sizeof(ABC)*3+Bオフセット,
                sizeof(ABC)*4+Bオフセット,
                sizeof(ABC)*5+Bオフセット,
                sizeof(ABC)*6+Bオフセット,
                sizeof(ABC)*7+Bオフセット
            );
            var Count= (レコード数+Int32ベクタ長 -1)/Int32ベクタ長;
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
            for(var a = 0;a<Int32ベクタ長 ;a++) {
                Sum+=Vector256Sum0.GetElement(a);
                Sum+=Vector256Sum1.GetElement(a);
                Sum+=Vector256Sum2.GetElement(a);
                Sum+=Vector256Sum3.GetElement(a);
                Sum+=Vector256Sum4.GetElement(a);
                Sum+=Vector256Sum5.GetElement(a);
                Sum+=Vector256Sum6.GetElement(a);
                Sum+=Vector256Sum7.GetElement(a);
            }
            Console.WriteLine($"Sum={Sum}");
            Console.WriteLine($"for {繰り返し数} loops: {watch.ElapsedMilliseconds}ms");
            Console.WriteLine();
        }
        [TestMethod]
        public void AOSからSIMD合計ループ1展開() {
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
            var watch = Stopwatch.StartNew();
            fixed(ABC* pAOS = AOS) {
                for(var a = 0;a<繰り返し数;a++) {
                    for(var b = 0;b<Count;b++) {
                        Vector256Sum0=Avx2.Add(Vector256Sum0,Avx2.GatherVector256((Int32*)&pAOS[b*Int32ベクタ長],index,1));
                    }
                }
            }
            watch.Stop();
            var Sum = 0;
            for(var a = 0;a<Int32ベクタ長;a++) {
                Sum+=Vector256Sum0.GetElement(a);
            }
            Console.WriteLine($"Sum={Sum}");
            Console.WriteLine($"for {繰り返し数} loops: {watch.ElapsedMilliseconds}ms");
            Console.WriteLine();
        }
        [TestMethod]
        public void AOSからScalar合計() {
            var AOS = AOS作成();
            var Sum = 0;
            var watch = Stopwatch.StartNew();
            fixed(ABC* pAOS = AOS) {
                for(var a = 0;a<繰り返し数;a++) {
                    for(var b = 0;b<レコード数;b++) {
                        Sum+=AOS[b].B;
                    }
                }
            }
            watch.Stop();
            Console.WriteLine($"Sum={Sum}");
            Console.WriteLine($"for {繰り返し数} loops: {watch.ElapsedMilliseconds}ms");
            Console.WriteLine();
        }
        [TestMethod]
        public void 水平加算Int16長い配列並列Simd() {
            var Int16配列 = データ作成();
            var 演算回数 = 0;
            fixed(Int16 *pInt16配列= &Int16配列[0]) {
                var pVector256配列0 = (Vector256<Int16>*)pInt16配列;
                Parallel.ForEach(Partitioner.Create(0,要素数/(sizeof(Vector256<Int16>)/sizeof(Int16)),2),(Action<Tuple<Int32,Int32>,ParallelLoopState>)((range,loopState) => {
                    var pVector256配列1 = pVector256配列0;
                    var 演算回数0 = 0;
                    for(var b = 0;b<TestAvx2.繰り返し数;b++) {
                        for(var a = range.Item1;a<range.Item2;a+=2) {
                            var Vector256_0 = pVector256配列1[a+0];
                            var Vector256_1 = pVector256配列1[a+1];
                            var result = Avx2.HorizontalAdd(Vector256_0,Vector256_1);
                            //Debug.Print(result.ToString());
                            演算回数0++;
                        }
                    }
                    Interlocked.Add(ref 演算回数,演算回数0);
                }));
            }
            Debug.Print(演算回数.ToString());
        }
        [TestMethod]
        public void 水平加算Int16長い配列並列Scaler() {
            var Int16配列 = データ作成();
            var 演算回数 = 0;
            fixed(Int16* pInt16配列0 = &Int16配列[0]) {
                var pInt16配列1 = pInt16配列0;
                Parallel.ForEach(Partitioner.Create(0,要素数),(Action<Tuple<Int32,Int32>,ParallelLoopState>)((range,loopState) => {
                    var pInt16配列2 = pInt16配列1;
                    var 演算回数0 = 0;
                    for(var b = 0;b<TestAvx2.繰り返し数;b++) {
                        for(var a = range.Item1;a<range.Item2;a+=32) {
                            var v0 = pInt16配列2[a+0x00]+pInt16配列2[a+0x01];
                            var v1 = pInt16配列2[a+0x02]+pInt16配列2[a+0x03];
                            var v2 = pInt16配列2[a+0x04]+pInt16配列2[a+0x05];
                            var v3 = pInt16配列2[a+0x06]+pInt16配列2[a+0x07];
                            var v4 = pInt16配列2[a+0x08]+pInt16配列2[a+0x09];
                            var v5 = pInt16配列2[a+0x0A]+pInt16配列2[a+0x0B];
                            var v6 = pInt16配列2[a+0x0C]+pInt16配列2[a+0x0D];
                            var v7 = pInt16配列2[a+0x0E]+pInt16配列2[a+0x0F];
                            var v8 = pInt16配列2[a+0x10]+pInt16配列2[a+0x11];
                            var v9 = pInt16配列2[a+0x12]+pInt16配列2[a+0x13];
                            var vA = pInt16配列2[a+0x14]+pInt16配列2[a+0x15];
                            var vB = pInt16配列2[a+0x16]+pInt16配列2[a+0x17];
                            var vC = pInt16配列2[a+0x18]+pInt16配列2[a+0x19];
                            var vD = pInt16配列2[a+0x1A]+pInt16配列2[a+0x1B];
                            var vE = pInt16配列2[a+0x1C]+pInt16配列2[a+0x1D];
                            var vF = pInt16配列2[a+0x1E]+pInt16配列2[a+0x1F];
                            //Debug.Print(v0.ToString());
                            //Debug.Print(v1.ToString());
                            //Debug.Print(v2.ToString());
                            //Debug.Print(v3.ToString());
                            //Debug.Print(v4.ToString());
                            //Debug.Print(v5.ToString());
                            //Debug.Print(v6.ToString());
                            //Debug.Print(v7.ToString());
                            //Debug.Print(v8.ToString());
                            //Debug.Print(v9.ToString());
                            //Debug.Print(vA.ToString());
                            //Debug.Print(vB.ToString());
                            //Debug.Print(vC.ToString());
                            //Debug.Print(vD.ToString());
                            //Debug.Print(vE.ToString());
                            //Debug.Print(vF.ToString());
                            演算回数0++;
                        }
                    }
                    Interlocked.Add(ref 演算回数,演算回数0);
                }));
            }
            Debug.Print(演算回数.ToString());
        }
        [TestMethod]
        public void 水平加算Int16長い配列直列Simd() {
            var Int16配列 = データ作成();
            var 演算回数 = 0;
            fixed(Int16* pInt16配列 = &Int16配列[0]) {
                var pVector256配列 = (Vector256<Int16>*)pInt16配列;
                var Vector256配列_Length = 要素数/(sizeof(Vector256<Int16>)/sizeof(Int16));
                for(var b = 0;b<繰り返し数;b++) {
                    for(var a = 0;a<Vector256配列_Length;a+=2) {
                        var Vector256_0 = pVector256配列[a+0];
                        var Vector256_1 = pVector256配列[a+1];
                        var result = Avx2.HorizontalAdd(Vector256_0,Vector256_1);
                        //Debug.Print(result.ToString());
                        演算回数++;
                    }
                }
            }
            Debug.Print(演算回数.ToString());
        }
        [TestMethod]
        public void 水平加算Int16長い配列直列Scaler() {
            var Int16配列 = データ作成();
            var 演算回数 = 0;
            fixed(Int16* pInt16配列 = &Int16配列[0]) {
                for(var b = 0;b<繰り返し数;b++) {
                    for(var a = 0;a<要素数;a+=32) {
                        var v0 = pInt16配列[a+0x00]+pInt16配列[a+0x01];
                        var v1 = pInt16配列[a+0x02]+pInt16配列[a+0x03];
                        var v2 = pInt16配列[a+0x04]+pInt16配列[a+0x05];
                        var v3 = pInt16配列[a+0x06]+pInt16配列[a+0x07];
                        var v4 = pInt16配列[a+0x08]+pInt16配列[a+0x09];
                        var v5 = pInt16配列[a+0x0A]+pInt16配列[a+0x0B];
                        var v6 = pInt16配列[a+0x0C]+pInt16配列[a+0x0D];
                        var v7 = pInt16配列[a+0x0E]+pInt16配列[a+0x0F];
                        var v8 = pInt16配列[a+0x10]+pInt16配列[a+0x11];
                        var v9 = pInt16配列[a+0x12]+pInt16配列[a+0x13];
                        var vA = pInt16配列[a+0x14]+pInt16配列[a+0x15];
                        var vB = pInt16配列[a+0x16]+pInt16配列[a+0x17];
                        var vC = pInt16配列[a+0x18]+pInt16配列[a+0x19];
                        var vD = pInt16配列[a+0x1A]+pInt16配列[a+0x1B];
                        var vE = pInt16配列[a+0x1C]+pInt16配列[a+0x1D];
                        var vF = pInt16配列[a+0x1E]+pInt16配列[a+0x1F];
                        //Debug.Print(v0.ToString());
                        //Debug.Print(v1.ToString());
                        //Debug.Print(v2.ToString());
                        //Debug.Print(v3.ToString());
                        //Debug.Print(v4.ToString());
                        //Debug.Print(v5.ToString());
                        //Debug.Print(v6.ToString());
                        //Debug.Print(v7.ToString());
                        //Debug.Print(v8.ToString());
                        //Debug.Print(v9.ToString());
                        //Debug.Print(vA.ToString());
                        //Debug.Print(vB.ToString());
                        //Debug.Print(vC.ToString());
                        //Debug.Print(vD.ToString());
                        //Debug.Print(vE.ToString());
                        //Debug.Print(vF.ToString());
                        演算回数++;
                    }
                }
            }
            Debug.Print(演算回数.ToString());
        }
        [TestMethod]
        public void 垂直加算Int16() {
            for(var a = 0;a<1;a++) {
                var operand0 = Vector256.Create(0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15);
                var operand1 = Vector256.Create(15,14,13,12,11,10,9,8,7,6,5,4,3,2,1,0);
                for(var b = 0;b<1;b++) {
                    var result = Avx2.Add(operand0,operand1);
                }
            }
        }
        [TestMethod]
        public void FMA() {
            for(var a = 0;a<1;a++) {
                var operand0 = Vector256.Create(0.0,1.0,2.0,3.0);
                var operand1 = Vector256.Create(3.0,2.0,1.0,0.0);
                var operand2 = Vector256.Create(1.0,1.0,1.0,1.0);
                for(var b = 0;b<1;b++) {
                    var result = Fma.MultiplyAdd(operand0,operand1,operand2);
                }
            }
        }
    }
}
