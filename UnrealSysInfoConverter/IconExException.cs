// Decompiled with JetBrains decompiler
// Type: vbAccelerator.Components.Win32.IconExException
// Assembly: IconEX, Version=1.0.2282.4, Culture=neutral, PublicKeyToken=null
// MVID: A87705EC-6221-4D07-83DD-3F9E72B9994B
// Assembly location: C:\Users\Администратор\Downloads\IconEX.dll

using System;

namespace vbAccelerator.Components.Win32
{
    public class IconExException : Exception
    {
        public IconExException ( )
        {
        }

        public IconExException ( string message )
            : base( message )
        {
        }

        public IconExException ( string message , Exception innerException )
            : base( message , innerException )
        {
        }
    }
}
