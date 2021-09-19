// Decompiled with JetBrains decompiler
// Type: vbAccelerator.Components.Win32.IconDeviceImage
// Assembly: IconEX, Version=1.0.2282.4, Culture=neutral, PublicKeyToken=null
// MVID: A87705EC-6221-4D07-83DD-3F9E72B9994B
// Assembly location: C:\Users\Администратор\Downloads\IconEX.dll

using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace vbAccelerator.Components.Win32
{
    public class IconDeviceImage : IDisposable
    {
        private ColorDepth colorDepth = ColorDepth.Depth4Bit;
        private IntPtr hIcon = IntPtr.Zero;
        private const int DIB_RGB_COLORS = 0;
        private const int DIB_PAL_COLORS = 1;
        private const int DIB_PAL_INDICES = 2;
        private const int DIB_PAL_PHYSINDICES = 2;
        private const int DIB_PAL_LOGINDICES = 4;
        private const int BI_RGB = 0;
        private const int BI_RLE4 = 2;
        private const int BI_RLE8 = 1;
        private const short IMAGE_ICON = 1;
        private Size size;
        private byte [ ] data;

        public IntPtr Handle
        {
            get
            {
                return this.hIcon;
            }
        }

        public Size IconSize
        {
            get
            {
                return this.size;
            }
        }

        public ColorDepth ColorDepth
        {
            get
            {
                return this.colorDepth;
            }
        }

        public Bitmap MaskImage
        {
            get
            {
                IntPtr hBmp = IntPtr.Zero;
                return this.getIconBitmap( true , false , ref hBmp );
            }
            set
            {
                this.setMaskBitsFromBitmap( value );
            }
        }

        public Bitmap IconImage
        {
            get
            {
                IntPtr hBmp = IntPtr.Zero;
                return this.getIconBitmap( false , false , ref hBmp );
            }
            set
            {
                this.setImageBitsFromBitmap( value );
            }
        }

        public Icon Icon
        {
            get
            {
                return Icon.FromHandle( this.hIcon );
            }
        }

        public IconDeviceImage ( Size size , ColorDepth colorDepth )
        {
            this.setDeviceImage( size , colorDepth );
            this.createIcon( );
        }

        public IconDeviceImage ( Icon icon )
        {
        }

        internal IconDeviceImage ( byte [ ] b )
        {
            this.data = new byte [ b.Length ];
            for ( int index = 0 ; index < b.Length ; ++index )
                this.data [ index ] = b [ index ];
            IconDeviceImage.BITMAPINFOHEADER bitmapinfoheader = new IconDeviceImage.BITMAPINFOHEADER( this.data );
            this.size.Width = bitmapinfoheader.biWidth;
            this.size.Height = bitmapinfoheader.biHeight / 2;
            switch ( bitmapinfoheader.biBitCount )
            {
                case 16:
                    this.colorDepth = ColorDepth.Depth16Bit;
                    break;
                case 24:
                    this.colorDepth = ColorDepth.Depth24Bit;
                    break;
                case 32:
                    this.colorDepth = ColorDepth.Depth32Bit;
                    break;
                case 1:
                case 4:
                    this.colorDepth = ColorDepth.Depth4Bit;
                    break;
                case 8:
                    this.colorDepth = ColorDepth.Depth8Bit;
                    break;
            }
            this.createIcon( );
        }

        [DllImport( "gdi32" )]
        private static extern int SetDIBitsToDevice ( IntPtr hdc , int X , int Y , int dx , int dy , int SrcX , int SrcY , int Scan , int NumScans , IntPtr Bits , IntPtr BitsInfo , int wUsage );

        [DllImport( "gdi32" )]
        public static extern int GetDIBits ( IntPtr hdc , IntPtr hBitmap , int nStartScan , int nNumScans , IntPtr Bits , IntPtr BitsInfo , int wUsage );

        [DllImport( "gdi32" )]
        private static extern IntPtr CreateCompatibleDC ( IntPtr hdc );

        [DllImport( "gdi32" , CharSet = CharSet.Auto )]
        private static extern IntPtr CreateDC ( [MarshalAs( UnmanagedType.LPTStr )] string lpDriverName , IntPtr lpDeviceName , IntPtr lpOutput , IntPtr lpInitData );

        [DllImport( "gdi32" )]
        private static extern IntPtr CreateCompatibleBitmap ( IntPtr hdc , int width , int height );

        [DllImport( "gdi32" )]
        private static extern IntPtr SelectObject ( IntPtr hdc , IntPtr hObject );

        [DllImport( "gdi32" )]
        private static extern int DeleteObject ( IntPtr hObject );

        [DllImport( "gdi32" )]
        private static extern int DeleteDC ( IntPtr hdc );

        [DllImport( "user32" )]
        private static extern int DestroyIcon ( IntPtr hIcon );

        [DllImport( "user32" )]
        private static extern IntPtr CreateIconIndirect ( ref IconDeviceImage.ICONINFO piconInfo );

        private void setMaskBitsFromBitmap ( Bitmap bm )
        {
            IntPtr dc = IconDeviceImage.CreateDC( "DISPLAY" , IntPtr.Zero , IntPtr.Zero , IntPtr.Zero );
            IntPtr compatibleDc = IconDeviceImage.CreateCompatibleDC( dc );
            IconDeviceImage.DeleteDC( dc );
            IntPtr hbitmap = bm.GetHbitmap( );
            IconDeviceImage.BITMAPINFOHEADER bmInfoHeader = new IconDeviceImage.BITMAPINFOHEADER( this.size , this.colorDepth );
            IconDeviceImage.RGBQUAD rgbquad = new IconDeviceImage.RGBQUAD( );
            IntPtr num1 = Marshal.AllocCoTaskMem( bmInfoHeader.biSize + Marshal.SizeOf( ( object ) rgbquad ) * 2 );
            Marshal.WriteInt32( num1 , Marshal.SizeOf( ( object ) bmInfoHeader ) );
            Marshal.WriteInt32( num1 , 4 , this.size.Width );
            Marshal.WriteInt32( num1 , 8 , this.size.Height );
            Marshal.WriteInt16( num1 , 12 , ( short ) 1 );
            Marshal.WriteInt16( num1 , 14 , ( short ) 1 );
            Marshal.WriteInt32( num1 , 16 , 0 );
            Marshal.WriteInt32( num1 , 20 , 0 );
            Marshal.WriteInt32( num1 , 24 , 0 );
            Marshal.WriteInt32( num1 , 28 , 0 );
            Marshal.WriteInt32( num1 , 32 , 0 );
            Marshal.WriteInt32( num1 , 36 , 0 );
            Marshal.WriteInt32( num1 , 40 , 0 );
            Marshal.WriteByte( num1 , 44 , byte.MaxValue );
            Marshal.WriteByte( num1 , 45 , byte.MaxValue );
            Marshal.WriteByte( num1 , 46 , byte.MaxValue );
            Marshal.WriteByte( num1 , 47 , ( byte ) 0 );
            int num2 = this.MaskImageSize( bmInfoHeader );
            IntPtr num3 = Marshal.AllocCoTaskMem( num2 );
            IconDeviceImage.GetDIBits( compatibleDc , hbitmap , 0 , this.size.Height , num3 , num1 , 0 );
            Marshal.Copy( num3 , this.data , this.MaskImageIndex( bmInfoHeader ) , num2 );
            Marshal.FreeCoTaskMem( num3 );
            Marshal.FreeCoTaskMem( num1 );
            IconDeviceImage.DeleteObject( hbitmap );
            IconDeviceImage.DeleteDC( compatibleDc );
            this.createIcon( );
        }

        private void setImageBitsFromBitmap ( Bitmap bm )
        {
            IntPtr dc = IconDeviceImage.CreateDC( "DISPLAY" , IntPtr.Zero , IntPtr.Zero , IntPtr.Zero );
            IntPtr compatibleDc = IconDeviceImage.CreateCompatibleDC( dc );
            IconDeviceImage.DeleteDC( dc );
            IntPtr hbitmap = bm.GetHbitmap( );
            IconDeviceImage.BITMAPINFOHEADER bmInfoHeader = new IconDeviceImage.BITMAPINFOHEADER( this.size , this.colorDepth );
            int num1 = this.XorImageIndex( bmInfoHeader );
            int num2 = this.XorImageSize( bmInfoHeader );
            IntPtr num3 = Marshal.AllocCoTaskMem( num1 );
            Marshal.Copy( this.data , 0 , num3 , num1 );
            Marshal.WriteInt32( num3 , 8 , bmInfoHeader.biHeight / 2 );
            IntPtr num4 = Marshal.AllocCoTaskMem( num2 );
            IconDeviceImage.GetDIBits( compatibleDc , hbitmap , 0 , this.size.Height , num4 , num3 , 0 );
            Marshal.Copy( num4 , this.data , num1 , num2 );
            Marshal.FreeCoTaskMem( num4 );
            Marshal.FreeCoTaskMem( num3 );
            IconDeviceImage.DeleteObject( hbitmap );
            IconDeviceImage.DeleteDC( compatibleDc );
            this.createIcon( );
        }

        private void setDeviceImage ( Size size , ColorDepth colorDepth )
        {
            this.size = size;
            this.colorDepth = colorDepth;
            IconDeviceImage.BITMAPINFOHEADER bmInfoHeader = new IconDeviceImage.BITMAPINFOHEADER( size , colorDepth );
            this.data = new byte [ this.MaskImageIndex( bmInfoHeader ) + this.MaskImageSize( bmInfoHeader ) ];
            BinaryWriter bw = new BinaryWriter( ( Stream ) new MemoryStream( this.data , 0 , this.data.Length , true ) );
            bmInfoHeader.Write( bw );
            switch ( this.colorDepth )
            {
                case ColorDepth.Depth4Bit:
                    this.write16ColorPalette( bw );
                    break;
                case ColorDepth.Depth8Bit:
                    this.write256ColorPalette( bw );
                    break;
            }
            bw.Close( );
        }

        private void write16ColorPalette ( BinaryWriter bw )
        {
            this.writeColor( bw , Color.Black );
            this.writeColor( bw , Color.White );
            this.writeColor( bw , Color.Red );
            this.writeColor( bw , Color.Green );
            this.writeColor( bw , Color.Blue );
            this.writeColor( bw , Color.Yellow );
            this.writeColor( bw , Color.Magenta );
            this.writeColor( bw , Color.Cyan );
            this.writeColor( bw , Color.Gray );
            this.writeColor( bw , Color.DarkRed );
            this.writeColor( bw , Color.DarkGreen );
            this.writeColor( bw , Color.DarkBlue );
            this.writeColor( bw , Color.Olive );
            this.writeColor( bw , Color.Purple );
            this.writeColor( bw , Color.Teal );
            this.writeColor( bw , Color.DarkGray );
        }

        private void write256ColorPalette ( BinaryWriter bw )
        {
            Array values = Enum.GetValues( KnownColor.ActiveBorder.GetType( ) );
            int num = 0;
            foreach ( KnownColor color in values )
            {
                this.writeColor( bw , Color.FromKnownColor( color ) );
                ++num;
                if ( num > ( int ) byte.MaxValue )
                    break;
            }
        }

        private void writeColor ( BinaryWriter bw , Color color )
        {
            new IconDeviceImage.RGBQUAD( color ).Write( bw );
        }

        private Bitmap getIconBitmap ( bool mask , bool returnHandle , ref IntPtr hBmp )
        {
            Bitmap bitmap = ( Bitmap ) null;
            IconDeviceImage.BITMAPINFOHEADER bmInfoHeader = new IconDeviceImage.BITMAPINFOHEADER( this.data );
            if ( mask )
            {
                IntPtr compatibleDc = IconDeviceImage.CreateCompatibleDC( IntPtr.Zero );
                hBmp = IconDeviceImage.CreateCompatibleBitmap( compatibleDc , bmInfoHeader.biWidth , bmInfoHeader.biHeight / 2 );
                IntPtr hObject = IconDeviceImage.SelectObject( compatibleDc , hBmp );
                IconDeviceImage.RGBQUAD rgbquad = new IconDeviceImage.RGBQUAD( );
                IntPtr num1 = Marshal.AllocCoTaskMem( bmInfoHeader.biSize + Marshal.SizeOf( ( object ) rgbquad ) * 2 );
                Marshal.WriteInt32( num1 , Marshal.SizeOf( ( object ) bmInfoHeader ) );
                Marshal.WriteInt32( num1 , 4 , bmInfoHeader.biWidth );
                Marshal.WriteInt32( num1 , 8 , bmInfoHeader.biHeight / 2 );
                Marshal.WriteInt16( num1 , 12 , ( short ) 1 );
                Marshal.WriteInt16( num1 , 14 , ( short ) 1 );
                Marshal.WriteInt32( num1 , 16 , 0 );
                Marshal.WriteInt32( num1 , 20 , 0 );
                Marshal.WriteInt32( num1 , 24 , 0 );
                Marshal.WriteInt32( num1 , 28 , 0 );
                Marshal.WriteInt32( num1 , 32 , 0 );
                Marshal.WriteInt32( num1 , 36 , 0 );
                Marshal.WriteInt32( num1 , 40 , 0 );
                Marshal.WriteByte( num1 , 44 , byte.MaxValue );
                Marshal.WriteByte( num1 , 45 , byte.MaxValue );
                Marshal.WriteByte( num1 , 46 , byte.MaxValue );
                Marshal.WriteByte( num1 , 47 , ( byte ) 0 );
                int num2 = this.MaskImageSize( bmInfoHeader );
                IntPtr num3 = Marshal.AllocCoTaskMem( num2 );
                Marshal.Copy( this.data , this.MaskImageIndex( bmInfoHeader ) , num3 , num2 );
                IconDeviceImage.SetDIBitsToDevice( compatibleDc , 0 , 0 , bmInfoHeader.biWidth , bmInfoHeader.biHeight / 2 , 0 , 0 , 0 , bmInfoHeader.biHeight / 2 , num3 , num1 , 0 );
                Marshal.FreeCoTaskMem( num3 );
                Marshal.FreeCoTaskMem( num1 );
                IconDeviceImage.SelectObject( compatibleDc , hObject );
                IconDeviceImage.DeleteObject( compatibleDc );
            }
            else
            {
                IntPtr dc = IconDeviceImage.CreateDC( "DISPLAY" , IntPtr.Zero , IntPtr.Zero , IntPtr.Zero );
                IntPtr compatibleDc = IconDeviceImage.CreateCompatibleDC( dc );
                hBmp = IconDeviceImage.CreateCompatibleBitmap( dc , bmInfoHeader.biWidth , bmInfoHeader.biHeight / 2 );
                IconDeviceImage.DeleteDC( dc );
                IntPtr hObject = IconDeviceImage.SelectObject( compatibleDc , hBmp );
                int num1 = this.XorImageIndex( bmInfoHeader );
                int num2 = this.XorImageSize( bmInfoHeader );
                IntPtr num3 = Marshal.AllocCoTaskMem( num1 );
                Marshal.Copy( this.data , 0 , num3 , num1 );
                Marshal.WriteInt32( num3 , 8 , bmInfoHeader.biHeight / 2 );
                IntPtr num4 = Marshal.AllocCoTaskMem( num2 );
                Marshal.Copy( this.data , num1 , num4 , num2 );
                IconDeviceImage.SetDIBitsToDevice( compatibleDc , 0 , 0 , bmInfoHeader.biWidth , bmInfoHeader.biHeight / 2 , 0 , 0 , 0 , bmInfoHeader.biHeight / 2 , num4 , num3 , 0 );
                Marshal.FreeCoTaskMem( num4 );
                Marshal.FreeCoTaskMem( num3 );
                IconDeviceImage.SelectObject( compatibleDc , hObject );
                IconDeviceImage.DeleteObject( compatibleDc );
            }
            if ( !returnHandle )
                bitmap = Image.FromHbitmap( hBmp );
            return bitmap;
        }

        private int MaskImageIndex ( IconDeviceImage.BITMAPINFOHEADER bmInfoHeader )
        {
            return this.XorImageIndex( bmInfoHeader ) + this.XorImageSize( bmInfoHeader );
        }

        private int XorImageSize ( IconDeviceImage.BITMAPINFOHEADER bmInfoHeader )
        {
            return bmInfoHeader.biHeight / 2 * this.WidthBytes( bmInfoHeader.biWidth * ( int ) bmInfoHeader.biBitCount * ( int ) bmInfoHeader.biPlanes );
        }

        private int MaskImageSize ( IconDeviceImage.BITMAPINFOHEADER bmInfoHeader )
        {
            return bmInfoHeader.biHeight / 2 * this.WidthBytes( bmInfoHeader.biWidth );
        }

        private int WidthBytes ( int width )
        {
            return ( width + 31 ) / 32 * 4;
        }

        private int XorImageIndex ( IconDeviceImage.BITMAPINFOHEADER bmInfoHeader )
        {
            IconDeviceImage.RGBQUAD rgbquad = new IconDeviceImage.RGBQUAD( );
            return Marshal.SizeOf( ( object ) bmInfoHeader ) + this.dibNumColors( bmInfoHeader ) * Marshal.SizeOf( ( object ) rgbquad );
        }

        private int dibNumColors ( IconDeviceImage.BITMAPINFOHEADER bmInfoHeader )
        {
            int num = 0;
            if ( bmInfoHeader.biClrUsed != 0 )
            {
                num = bmInfoHeader.biClrUsed;
            }
            else
            {
                switch ( bmInfoHeader.biBitCount )
                {
                    case 1:
                        num = 2;
                        break;
                    case 4:
                        num = 16;
                        break;
                    case 8:
                        num = 256;
                        break;
                }
            }
            return num;
        }

        internal int IconImageDataBytes ( )
        {
            return this.data.Length;
        }

        internal void SaveIconBitmapData ( BinaryWriter bw )
        {
            bw.Write( this.data , 0 , this.data.Length );
        }

        private void createIcon ( )
        {
            if ( this.hIcon != IntPtr.Zero )
            {
                IconDeviceImage.DestroyIcon( this.hIcon );
                this.hIcon = IntPtr.Zero;
            }
            IconDeviceImage.ICONINFO piconInfo = new IconDeviceImage.ICONINFO( );
            piconInfo.fIcon = 1;
            this.getIconBitmap( false , true , ref piconInfo.hBmColor );
            this.getIconBitmap( true , true , ref piconInfo.hBmMask );
            this.hIcon = IconDeviceImage.CreateIconIndirect( ref piconInfo );
            IconDeviceImage.DeleteObject( piconInfo.hBmColor );
            IconDeviceImage.DeleteObject( piconInfo.hBmMask );
        }

        public void Dispose ( )
        {
            if ( !( this.hIcon != IntPtr.Zero ) )
                return;
            IconDeviceImage.DestroyIcon( this.hIcon );
            this.hIcon = IntPtr.Zero;
        }

        private struct ICONINFO
        {
            public int fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hBmMask;
            public IntPtr hBmColor;
        }

        private struct BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;

            public BITMAPINFOHEADER ( Size size , ColorDepth colorDepth )
            {
                this.biSize = 0;
                this.biWidth = size.Width;
                this.biHeight = size.Height * 2;
                this.biPlanes = ( short ) 1;
                this.biCompression = 0;
                this.biSizeImage = 0;
                this.biXPelsPerMeter = 0;
                this.biYPelsPerMeter = 0;
                this.biClrUsed = 0;
                this.biClrImportant = 0;
                switch ( colorDepth )
                {
                    case ColorDepth.Depth16Bit:
                        this.biBitCount = ( short ) 16;
                        break;
                    case ColorDepth.Depth24Bit:
                        this.biBitCount = ( short ) 24;
                        break;
                    case ColorDepth.Depth32Bit:
                        this.biBitCount = ( short ) 32;
                        break;
                    case ColorDepth.Depth4Bit:
                        this.biBitCount = ( short ) 4;
                        break;
                    case ColorDepth.Depth8Bit:
                        this.biBitCount = ( short ) 8;
                        break;
                    default:
                        this.biBitCount = ( short ) 4;
                        break;
                }
                this.biSize = Marshal.SizeOf( this.GetType( ) );
            }

            public BITMAPINFOHEADER ( byte [ ] data )
            {
                BinaryReader binaryReader = new BinaryReader( ( Stream ) new MemoryStream( data , false ) );
                this.biSize = binaryReader.ReadInt32( );
                this.biWidth = binaryReader.ReadInt32( );
                this.biHeight = binaryReader.ReadInt32( );
                this.biPlanes = binaryReader.ReadInt16( );
                this.biBitCount = binaryReader.ReadInt16( );
                this.biCompression = binaryReader.ReadInt32( );
                this.biSizeImage = binaryReader.ReadInt32( );
                this.biXPelsPerMeter = binaryReader.ReadInt32( );
                this.biYPelsPerMeter = binaryReader.ReadInt32( );
                this.biClrUsed = binaryReader.ReadInt32( );
                this.biClrImportant = binaryReader.ReadInt32( );
                binaryReader.Close( );
            }

            public void Write ( BinaryWriter bw )
            {
                bw.Write( this.biSize );
                bw.Write( this.biWidth );
                bw.Write( this.biHeight );
                bw.Write( this.biPlanes );
                bw.Write( this.biBitCount );
                bw.Write( this.biCompression );
                bw.Write( this.biSizeImage );
                bw.Write( this.biXPelsPerMeter );
                bw.Write( this.biYPelsPerMeter );
                bw.Write( this.biClrUsed );
                bw.Write( this.biClrImportant );
            }

            public override string ToString ( )
            {
                return string.Format( "biSize: {0}, biWidth: {1}, biHeight: {2}, biPlanes: {3}, biBitCount: {4}, biCompression: {5}, biSizeImage: {6}, biXPelsPerMeter: {7}, biYPelsPerMeter {8}, biClrUsed {9}, biClrImportant {10}" , ( object ) this.biSize , ( object ) this.biWidth , ( object ) this.biHeight , ( object ) this.biPlanes , ( object ) this.biBitCount , ( object ) this.biCompression , ( object ) this.biSizeImage , ( object ) this.biXPelsPerMeter , ( object ) this.biYPelsPerMeter , ( object ) this.biClrUsed , ( object ) this.biClrImportant );
            }
        }

        private struct RGBQUAD
        {
            public byte rgbBlue;
            public byte rgbGreen;
            public byte rgbRed;
            public byte rgbReserved;

            public RGBQUAD ( byte r , byte g , byte b , byte alpha )
            {
                this.rgbBlue = b;
                this.rgbGreen = g;
                this.rgbRed = r;
                this.rgbReserved = ( byte ) 0;
            }

            public RGBQUAD ( Color c )
            {
                this.rgbBlue = c.B;
                this.rgbGreen = c.G;
                this.rgbRed = c.R;
                this.rgbReserved = ( byte ) 0;
            }

            public void Write ( BinaryWriter bw )
            {
                bw.Write( this.rgbBlue );
                bw.Write( this.rgbGreen );
                bw.Write( this.rgbRed );
                bw.Write( this.rgbReserved );
            }

            public override string ToString ( )
            {
                return string.Format( "rgbBlue: {0}, rgbGreen: {1}, rgbRed: {2}" , ( object ) this.rgbBlue , ( object ) this.rgbGreen , ( object ) this.rgbRed );
            }
        }
    }
}
