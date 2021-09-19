// Decompiled with JetBrains decompiler
// Type: vbAccelerator.Components.Win32.IconDeviceImageCollection
// Assembly: IconEX, Version=1.0.2282.4, Culture=neutral, PublicKeyToken=null
// MVID: A87705EC-6221-4D07-83DD-3F9E72B9994B
// Assembly location: C:\Users\Администратор\Downloads\IconEX.dll

using System;
using System.Collections;

namespace vbAccelerator.Components.Win32
{
    public class IconDeviceImageCollection : CollectionBase , IDisposable
    {
        public IconDeviceImage this [ int index ]
        {
            get
            {
                return ( IconDeviceImage ) this.InnerList [ index ];
            }
        }

        public IconDeviceImageCollection ( )
        {
        }

        public IconDeviceImageCollection ( IconDeviceImage [ ] icons )
        {
            foreach ( object obj in icons )
                this.InnerList.Add( obj );
        }

        public void Add ( IconDeviceImage icon )
        {
            foreach ( IconDeviceImage iconDeviceImage in this.InnerList )
            {
                if ( icon.IconSize.Equals( ( object ) iconDeviceImage.IconSize ) && icon.ColorDepth.Equals( ( object ) iconDeviceImage.ColorDepth ) )
                    throw new IconExException( "An Icon Device Image with the same size and colour depth already exists in this icon" );
            }
            this.InnerList.Add( ( object ) icon );
        }

        public void Dispose ( )
        {
            if ( this.InnerList == null )
                return;
            foreach ( IconDeviceImage iconDeviceImage in this.InnerList )
                iconDeviceImage.Dispose( );
            this.InnerList.Clear( );
        }
    }
}
