// Decompiled with JetBrains decompiler
// Type: vbAccelerator.Components.Win32.IconEx
// Assembly: IconEX, Version=1.0.2282.4, Culture=neutral, PublicKeyToken=null
// MVID: A87705EC-6221-4D07-83DD-3F9E72B9994B
// Assembly location: C:\Users\Администратор\Downloads\IconEX.dll

using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace vbAccelerator.Components.Win32
{
    public class IconEx : IDisposable
    {

        private IconDeviceImageCollection iconCollection;
        private string iconFile;

        public IconDeviceImageCollection Items
        {
            get
            {
                return this.iconCollection;
            }
            set
            {
            }
        }

        public string IconFile
        {
            get
            {
                return this.iconFile;
            }
        }

     

        public IconEx ( )
        {
        }

        public IconEx ( string iconFile )
        {
            this.loadFromFile( iconFile );
        }


        public void FromFile ( string iconFile )
        {
            this.loadFromFile( iconFile );
        }

       
        public void Save ( string iconFile )
        {
            FileStream fileStream = new FileStream( iconFile , FileMode.Create , FileAccess.Write , FileShare.Read );
            BinaryWriter binaryWriter = ( BinaryWriter ) null;
            try
            {
                binaryWriter = new BinaryWriter( ( Stream ) fileStream );
                this.writeIconFileHeader( binaryWriter );
                int num1 = 6 + 16 * this.iconCollection.Count;
                foreach ( IconDeviceImage iconDeviceImage in ( CollectionBase ) this.iconCollection )
                {
                    int num2 = iconDeviceImage.IconImageDataBytes( );
                    IconEx.ICONDIRENTRY icondirentry = new IconEx.ICONDIRENTRY( );
                    byte num3 = ( byte ) iconDeviceImage.IconSize.Width;
                    byte num4 = ( byte ) iconDeviceImage.IconSize.Height;
                    icondirentry.width = num3;
                    icondirentry.height = num4;
                    switch ( iconDeviceImage.ColorDepth )
                    {
                        case ColorDepth.Depth16Bit:
                            icondirentry.colorCount = ( byte ) 0;
                            icondirentry.wBitCount = ( short ) 16;
                            break;
                        case ColorDepth.Depth24Bit:
                            icondirentry.colorCount = ( byte ) 0;
                            icondirentry.wBitCount = ( short ) 24;
                            break;
                        case ColorDepth.Depth32Bit:
                            icondirentry.colorCount = ( byte ) 0;
                            icondirentry.wBitCount = ( short ) 32;
                            break;
                        case ColorDepth.Depth4Bit:
                            icondirentry.colorCount = ( byte ) 16;
                            icondirentry.wBitCount = ( short ) 4;
                            break;
                        case ColorDepth.Depth8Bit:
                            icondirentry.colorCount = byte.MaxValue;
                            icondirentry.wBitCount = ( short ) 8;
                            break;
                    }
                    icondirentry.wPlanes = ( short ) 1;
                    icondirentry.dwBytesInRes = num2;
                    icondirentry.dwImageOffset = num1;
                    icondirentry.Write( binaryWriter );
                    num1 += num2;
                }
                foreach ( IconDeviceImage iconDeviceImage in ( CollectionBase ) this.iconCollection )
                    iconDeviceImage.SaveIconBitmapData( binaryWriter );
            }
            catch ( Exception ex )
            {
                if ( ex is SystemException )
                    throw ex;
                throw new IconExException( ex.Message , ex );
            }
            finally
            {
                if ( binaryWriter != null )
                    binaryWriter.Close( );
            }
        }

        private void loadInitialise ( )
        {
            this.iconFile = "";
  
            this.iconCollection = new IconDeviceImageCollection( );
        }

      
        private void loadFromFile ( string iconFile )
        {
            this.loadInitialise( );
            FileStream fileStream = new FileStream( iconFile , FileMode.Open , FileAccess.Read , FileShare.Read );
            BinaryReader br = new BinaryReader( ( Stream ) fileStream );
            try
            {
                int length = this.readIconFileHeader( br );
                IconEx.ICONDIRENTRY [ ] icondirentryArray = new IconEx.ICONDIRENTRY [ length ];
                for ( int index = 0 ; index < length ; ++index )
                    icondirentryArray [ index ] = new IconEx.ICONDIRENTRY( br );
                IconDeviceImage [ ] icons = new IconDeviceImage [ length ];
                for ( int index = 0 ; index < length ; ++index )
                {
                    fileStream.Seek( ( long ) icondirentryArray [ index ].dwImageOffset , SeekOrigin.Begin );
                    byte [ ] numArray = new byte [ icondirentryArray [ index ].dwBytesInRes ];
                    br.Read( numArray , 0 , icondirentryArray [ index ].dwBytesInRes );
                    icons [ index ] = new IconDeviceImage( numArray );
                }
                this.iconCollection = new IconDeviceImageCollection( icons );
            }
            catch ( Exception ex )
            {
                if ( ex is SystemException )
                    throw ex;
                throw new IconExException( "Failed to read icon file." , ex );
            }
            finally
            {
                br.Close( );
            }
            this.iconFile = iconFile;
        }

        private int readResourceIconFileHeader ( IntPtr lPtr )
        {
            int num1 = ( int ) Marshal.ReadInt16( lPtr );
            int num2 = ( int ) Marshal.ReadInt16( lPtr , 2 );
            int num3 = ( int ) Marshal.ReadInt16( lPtr , 4 );
            if ( num1 == 0 && num2 == 1 && ( num3 > 0 && num3 < 1024 ) )
                return num3;
            throw new IconExException( "Invalid Icon File Header" );
        }

        private int readIconFileHeader ( BinaryReader br )
        {
            int num1 = ( int ) br.ReadInt16( );
            int num2 = ( int ) br.ReadInt16( );
            int num3 = ( int ) br.ReadInt16( );
            if ( num1 == 0 && num2 == 1 && ( num3 > 0 && num3 < 1024 ) )
                return num3;
            throw new IconExException( "Invalid Icon File Header" );
        }

        private void writeIconFileHeader ( BinaryWriter bw )
        {
            short num1 = 0;
            bw.Write( num1 );
            short num2 = 1;
            bw.Write( num2 );
            short num3 = ( short ) this.Items.Count;
            bw.Write( num3 );
        }

        public void Dispose ( )
        {
            if ( this.iconCollection == null )
                return;
            this.iconCollection.Dispose( );
            this.iconCollection = ( IconDeviceImageCollection ) null;
        }

        private struct ICONDIRENTRY
        {
            public byte width;
            public byte height;
            public byte colorCount;
            public byte reserved;
            public short wPlanes;
            public short wBitCount;
            public int dwBytesInRes;
            public int dwImageOffset;

            public ICONDIRENTRY ( BinaryReader br )
            {
                this.width = br.ReadByte( );
                this.height = br.ReadByte( );
                this.colorCount = br.ReadByte( );
                this.reserved = br.ReadByte( );
                this.wPlanes = br.ReadInt16( );
                this.wBitCount = br.ReadInt16( );
                this.dwBytesInRes = br.ReadInt32( );
                this.dwImageOffset = br.ReadInt32( );
            }

            public void Write ( BinaryWriter br )
            {
                br.Write( this.width );
                br.Write( this.height );
                br.Write( this.colorCount );
                br.Write( this.reserved );
                br.Write( this.wPlanes );
                br.Write( this.wBitCount );
                br.Write( this.dwBytesInRes );
                br.Write( this.dwImageOffset );
            }

            public override string ToString ( )
            {
                return string.Format( "Size: ({0},{1}), ColorCount: {2}, Planes: {3}, BitCount {4}, BytesInRes: {5}, ImageOffset {6}" , ( object ) this.width , ( object ) this.height , ( object ) this.colorCount , ( object ) this.wPlanes , ( object ) this.wBitCount , ( object ) this.dwBytesInRes , ( object ) this.dwImageOffset );
            }
        }

        private struct MEMICONDIRENTRY
        {
            public byte width;
            public byte height;
            public byte colorCount;
            public byte reserved;
            public short wPlanes;
            public short wBitCount;
            public int dwBytesInRes;
            public short nID;

            public MEMICONDIRENTRY ( IntPtr lPtr , int ofs )
            {
                this.width = Marshal.ReadByte( lPtr , ofs );
                this.height = Marshal.ReadByte( lPtr , ofs + 1 );
                this.colorCount = Marshal.ReadByte( lPtr , ofs + 2 );
                this.reserved = Marshal.ReadByte( lPtr , ofs + 3 );
                this.wPlanes = Marshal.ReadInt16( lPtr , ofs + 4 );
                this.wBitCount = Marshal.ReadInt16( lPtr , ofs + 6 );
                this.dwBytesInRes = Marshal.ReadInt32( lPtr , ofs + 8 );
                this.nID = Marshal.ReadInt16( lPtr , ofs + 12 );
            }

            public override string ToString ( )
            {
                return string.Format( "Size: ({0},{1}), ColorCount: {2}, Planes: {3}, BitCount {4}, BytesInRes: {5}, IconResourceID {6}" , ( object ) this.width , ( object ) this.height , ( object ) this.colorCount , ( object ) this.wPlanes , ( object ) this.wBitCount , ( object ) this.dwBytesInRes , ( object ) this.nID );
            }
        }
    }
}
