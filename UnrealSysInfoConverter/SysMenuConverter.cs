using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using vbAccelerator;
using vbAccelerator.Components;
using vbAccelerator.Components.Win32;

namespace UnrealSysInfoConverter
{
    public partial class SysMenuConverter : Form
    {

        List<string> SettingsList = null;


        public SysMenuConverter ( )
        {
            InitializeComponent( );
            SettingsList = new List<string>( );
            SettingsList.Add( "GLOBALSETTINGS" );
            SettingsList.Add( "TEXT" );
            SettingsList.Add( "TIME" );
            SettingsList.Add( "DATE" );
            SettingsList.Add( "MEMORY" );
            SettingsList.Add( "DISKMEM" );
            SettingsList.Add( "SPACE" );
            SettingsList.Add( "BATTERY" );
            SettingsList.Add( "BUTTON" );
            SettingsList.Add( "ICONXPBUTTON" );
            SettingsList.Add( "BITMAPBUTTON" );
            SettingsList.Add( "WINDOWSBUTTON" );
        }

        bool ReplaceSlash = false;

        public struct ReplaceTextSimpleStruct
        {
            public string OLD;
            public string NEW;
        }

        List<ReplaceTextSimpleStruct> TextForReplace = null;

        private void InputIniDir_TextChanged ( object sender , EventArgs e )
        {
            if ( ReplaceSlash )
                return;

            if ( InputIniDir.Text.Length > 0 )
            {
                if ( InputIniDir.Text [ InputIniDir.Text.Length - 1 ] == '\\' )
                {
                    ReplaceSlash = true;
                    InputIniDir.Text = InputIniDir.Text.Remove( InputIniDir.Text.Length - 1 );
                    ReplaceSlash = false;
                }
                if ( InputIniDir.Text.IndexOf( '/' ) > -1 )
                {
                    ReplaceSlash = true;
                    InputIniDir.Text = InputIniDir.Text.Replace( '/' , '\\' );
                    ReplaceSlash = false;
                }
            }
        }

        List<string> IniFiles = null;
        List<string> BitmapReplacedFiles = null;

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public Bitmap ResizeImage ( Image image , int width , int height )
        {
            var destRect = new Rectangle( 0 , 0 , width , height );
            var destImage = new Bitmap( width , height );

            destImage.SetResolution( image.HorizontalResolution , image.VerticalResolution );

            using ( var graphics = Graphics.FromImage( destImage ) )
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using ( var wrapMode = new ImageAttributes( ) )
                {
                    wrapMode.SetWrapMode( WrapMode.TileFlipXY );
                    graphics.DrawImage( image , destRect , 0 , 0 , image.Width , image.Height , GraphicsUnit.Pixel , wrapMode );
                }
            }

            return destImage;
        }

        private Image MyGetImageFromFile ( string fileName )
        {
            MemoryStream _memstr = new MemoryStream( File.ReadAllBytes( fileName ));
            Image _returnimg = Image.FromStream(  _memstr , true );
            _memstr.Close();
            _memstr.Dispose( );
            return _returnimg;
        }



        private static ImageCodecInfo GetEncoderInfo ( String mimeType )
        {
            int j;
            ImageCodecInfo [ ] encoders;
            encoders = ImageCodecInfo.GetImageEncoders( );
            for ( j = 0 ; j < encoders.Length ; ++j )
            {
                if ( encoders [ j ].MimeType == mimeType )
                    return encoders [ j ];
            }
            return null;
        }

        private void ProcessIniFile ( string path , double XMult , double YMult )
        {
            string [ ] IniData = File.ReadAllLines( path );
            string _IniDir = Path.GetDirectoryName( Path.GetFullPath( path ) ) + "\\";

            //  double XSizeMult = ( XMult + YMult ) / 2.0f;


            string _GetWidth = @"^\s*Width\s*=\s*(\w+)\s*$";
            string _GetHeight = @"^\s*Height\s*=\s*(\w+)\s*$";
            string _GetSize = @"^\s*Size(\w*?)\s*=\s*(\w+)\s*$";
            string _GetX = @"^\s*X\s*=\s*(\w+)\s*$";
            string _GetY = @"^\s*Y\s*=\s*(\w+)\s*$";
            string _GetX2 = @"^\s*x\s*=\s*(\w+)\s*$";
            string _GetY2 = @"^\s*y\s*=\s*(\w+)\s*$";
            string _GetBitmap = @"^\s*\w*?Bitmap\w*?\s*=\s*(.+)\s*$";
            string _GetIcon = @"^\s*Icon\w+\s*=\s*(.+)\s*$";
            for ( int i = 0 ; i < IniData.Length ; i++ )
            {
                if ( ReplaceIniText.Checked )
                {
                    foreach ( ReplaceTextSimpleStruct _CurrentRepalceData in TextForReplace )
                        IniData [ i ] = IniData [ i ].Replace( _CurrentRepalceData.OLD , _CurrentRepalceData.NEW );
                }

                if ( OutputXSize.Text == InputXSize.Text && OutputYSize.Text == InputYSize.Text )
                    continue;


                Match _GetWidthMatch = Regex.Match( IniData [ i ] , _GetWidth , RegexOptions.Multiline );

                if ( _GetWidthMatch.Success )
                {
                    int _oldwidth = 0;
                    int.TryParse( _GetWidthMatch.Groups [ 1 ].Value , out _oldwidth );
                    int _newwidth = ( int ) ( Convert.ToDouble( _oldwidth ) / XMult );
                    IniData [ i ] = "Width = " + _newwidth;
                }
                else
                {
                    Match _GetHeightMatch = Regex.Match( IniData [ i ] , _GetHeight , RegexOptions.Multiline );

                    if ( _GetHeightMatch.Success )
                    {
                        int _oldheight = 0;
                        int.TryParse( _GetHeightMatch.Groups [ 1 ].Value , out _oldheight );
                        int _newheight = ( int ) ( Convert.ToDouble( _oldheight ) / YMult );
                        IniData [ i ] = "Height = " + _newheight;
                    }
                    else
                    {
                        Match _GetSizeMatch = Regex.Match( IniData [ i ] , _GetSize , RegexOptions.Multiline );
                        if ( _GetSizeMatch.Success )
                        {
                            int _oldsize = 0;
                            int.TryParse( _GetSizeMatch.Groups [ 2 ].Value , out _oldsize );
                            int _newsize = ( int ) ( Convert.ToDouble( _oldsize ) / YMult );
                            IniData [ i ] = "Size" + _GetSizeMatch.Groups [ 1 ].Value + " = " + _newsize;
                        }
                        else
                        {
                            Match _GetXMatch = Regex.Match( IniData [ i ] , _GetX , RegexOptions.Multiline );
                            if ( _GetXMatch.Success )
                            {
                                int _oldXX = 0;
                                int.TryParse( _GetXMatch.Groups [ 1 ].Value , out _oldXX );
                                int _newXX = ( int ) ( Convert.ToDouble( _oldXX ) / XMult );
                                IniData [ i ] = "X = " + _newXX;
                            }
                            else
                            {
                                Match _GetYMatch = Regex.Match( IniData [ i ] , _GetY , RegexOptions.Multiline );
                                if ( _GetYMatch.Success )
                                {
                                    int _oldYY = 0;
                                    int.TryParse( _GetYMatch.Groups [ 1 ].Value , out _oldYY );
                                    int _newYY = ( int ) ( Convert.ToDouble( _oldYY ) / YMult );
                                    IniData [ i ] = "Y = " + _newYY;
                                }
                                else
                                {
                                    Match _GetX2Match = Regex.Match( IniData [ i ] , _GetX2 , RegexOptions.Multiline );
                                    if ( _GetX2Match.Success )
                                    {
                                        int _oldxx = 0;
                                        int.TryParse( _GetX2Match.Groups [ 1 ].Value , out _oldxx );
                                        int _newxx = ( int ) ( Convert.ToDouble( _oldxx ) / XMult );
                                        IniData [ i ] = "x = " + _newxx;
                                    }
                                    else
                                    {
                                        Match _GetY2Match = Regex.Match( IniData [ i ] , _GetY2 , RegexOptions.Multiline );
                                        if ( _GetY2Match.Success )
                                        {
                                            int _oldyy = 0;
                                            int.TryParse( _GetY2Match.Groups [ 1 ].Value , out _oldyy );
                                            int _newyy = ( int ) ( Convert.ToDouble( _oldyy ) / YMult );
                                            IniData [ i ] = "y = " + _newyy;
                                        }
                                        else
                                        {

                                            Match _GetBitmapMatch = Regex.Match( IniData [ i ] , _GetBitmap , RegexOptions.Multiline );
                                            string _bitmappath = string.Empty;
                                            if ( _GetBitmapMatch.Success )
                                            {
                                                try
                                                {
                                                    _bitmappath = _IniDir + _GetBitmapMatch.Groups [ 1 ].Value;
                                                    _bitmappath = Path.GetFullPath( _bitmappath );
                                                    if ( !IsReplaced( _bitmappath ) )
                                                    {
                                                        Bitmap _Bitmap = ( Bitmap ) MyGetImageFromFile( _bitmappath );
                                                        _Bitmap = ResizeImage( _Bitmap , ( int ) ( Convert.ToDouble( _Bitmap.Width ) / XMult ) , ( int ) ( Convert.ToDouble( _Bitmap.Height ) / YMult ) );

                                                        _Bitmap.Save( _bitmappath , ImageFormat.Bmp );

                                                        AddToReplaced( _bitmappath );
                                                    }
                                                }
                                                catch
                                                {
                                                    MessageBox.Show( "Не удалось получить доступ до одного из изображений.\r\n" + _bitmappath );
                                                }
                                            }
                                            else
                                            {
                                                Match _GetIconMatch = Regex.Match( IniData [ i ] , _GetIcon , RegexOptions.Multiline );
                                                if ( _GetIconMatch.Success )
                                                {
                                                    try
                                                    {
                                                        _bitmappath = _IniDir + _GetIconMatch.Groups [ 1 ].Value;
                                                        _bitmappath = Path.GetFullPath( _bitmappath );
                                                        if ( !IsReplaced( _bitmappath ) )
                                                        {
                                                            Bitmap _Bitmap = ( Bitmap ) MyGetImageFromFile( _bitmappath );
                                                            _Bitmap = ResizeImage( _Bitmap , ( int ) ( Convert.ToDouble( _Bitmap.Width ) / YMult ) , ( int ) ( Convert.ToDouble( _Bitmap.Height ) / YMult ) );
                                                            FileStream st = new FileStream( _bitmappath , FileMode.Create );

                                                            Icon.FromHandle( _Bitmap.GetHicon( ) ).Save( st );

                                                            st.Close( );
                                                            st.Dispose( );

                                                            IconEx _ExIcon = new IconEx( _bitmappath );
                                                            if ( _ExIcon.Items.Count > 0 )
                                                                _ExIcon.Items.Clear( );
                                                            IconDeviceImage _ICDImage = new IconDeviceImage( new Size( _Bitmap.Width , _Bitmap.Height ) , ColorDepth.Depth32Bit );

                                                            _ICDImage.IconImage = new Bitmap( _Bitmap );
                                                            //adds icondevicimage to the icon file
                                                            _ExIcon.Items.Add( _ICDImage );
                                                            //saves icon 
                                                            _ExIcon.Save( _bitmappath );
                                                            //   ImagingHelper.ConvertToIcon( _Bitmap , _IconStream , _Bitmap.Width , true );
                                                            AddToReplaced( _bitmappath );

                                                            _ICDImage.Dispose( );
                                                        }
                                                    }
                                                    catch
                                                    {
                                                        MessageBox.Show( "Не удалось получить доступ до одного из изображений.\r\n" + _bitmappath );
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                /* foreach ( string SettingsItem in SettingsList )
                 {
                     if ( Regex.Match( IniData [ i ] , @"\s*" + SettingsItem + @"\s*" ).Success )
                     {


                     }
                 }*/



            }


            ConvertEncodingForStrings( IniData , Encoding.UTF8 , Encoding.Unicode );
            File.WriteAllLines( path , IniData , Encoding.Unicode );

        }


        private void ConvertEncodingForStrings ( string [ ] strlist , Encoding oldenc , Encoding newenc )
        {
            for ( int i = 0 ; i < strlist.Length ; i++ )
            {
                strlist [ i ] = newenc.GetString( Encoding.Convert( oldenc , newenc , oldenc.GetBytes( strlist [ i ] ) ) );
            }
        }

        private bool IsReplaced ( string path )
        {
            foreach ( string _CurBmpPath in BitmapReplacedFiles )
            {
                if ( _CurBmpPath.ToLower( ) == path.ToLower( ) )
                    return true;
            }
            return false;
        }

        private void AddToReplaced ( string path )
        {
            bool needadd = true;
            foreach ( string _CurBmpPath in BitmapReplacedFiles )
            {
                if ( _CurBmpPath.ToLower( ) == path.ToLower( ) )
                    needadd = false;
            }
            if ( needadd )
            {

                BitmapReplacedFiles.Add( path );
            }
        }

        private void AutoDetectSize_Click ( object sender , EventArgs e )
        {
            string SysInfoSettingsPath = InputIniDir.Text + "\\" + "SysInfoSettings.ini";
            if ( File.Exists( SysInfoSettingsPath ) )
            {
                string [ ] GeneralIniFiles = File.ReadAllLines( SysInfoSettingsPath );



                if ( GeneralIniFiles.Length > 0 )
                {
                    string FirstIniPath = GeneralIniFiles [ 0 ].Replace( '/' , '\\' );
                    if ( File.Exists( InputIniDir.Text + "\\" + FirstIniPath ) )
                    {

                        string [ ] FirstIniData = File.ReadAllLines( InputIniDir.Text + "\\" + FirstIniPath );

                        string GetGlobalSetupString = @"\s*GLOBALSETTINGS";

                        string GetGlobalWidth = @"\s*Width\s*=\s*(\w+)";
                        bool GlobalWidthFound = false;
                        string GetGlobalHeight = @"\s*Height\s*=\s*(\w+)";
                        bool GlobalHeightFound = false;

                        for ( int i = 0 ; i < FirstIniData.Length ; i++ )
                        {
                            if ( Regex.Match( FirstIniData [ i ] , GetGlobalSetupString ).Success )
                            {

                                for ( int n = i ; n < FirstIniData.Length ; n++ )
                                {
                                    if ( !GlobalHeightFound )
                                    {
                                        Match GlobalHeightMatch = Regex.Match( FirstIniData [ n ] , GetGlobalHeight );
                                        if ( GlobalHeightMatch.Success )
                                        {

                                            int NewInputYSizeInt = 0;
                                            string NewInputYSizeStr = GlobalHeightMatch.Groups [ 1 ].Value;
                                            int.TryParse( NewInputYSizeStr , out NewInputYSizeInt );
                                            InputYSize.Text = NewInputYSizeInt.ToString( );
                                            GlobalHeightFound = true;
                                            if ( GlobalWidthFound )
                                            {
                                                i = n;
                                                break;
                                            }
                                            i = n;
                                        }
                                    }


                                    if ( !GlobalWidthFound )
                                    {
                                        Match GlobalWidthMatch = Regex.Match( FirstIniData [ n ] , GetGlobalWidth );
                                        if ( GlobalWidthMatch.Success )
                                        {
                                            int NewInputXSizeInt = 0;
                                            string NewInputXSizeStr = GlobalWidthMatch.Groups [ 1 ].Value;
                                            int.TryParse( NewInputXSizeStr , out NewInputXSizeInt );
                                            InputXSize.Text = NewInputXSizeInt.ToString( );
                                            GlobalWidthFound = true;
                                            if ( GlobalHeightFound )
                                            {
                                                i = n;
                                                break;
                                            }
                                            i = n;
                                        }
                                    }

                                }

                                break;
                            }
                        }
                    }
                }
                else
                    MessageBox.Show( "Не найден путь из SysInfoSettings.ini файла" );
            }
            else
                MessageBox.Show( "Не найден SysInfoSettings.ini файл" );
        }


        private void UpdatePercentSize ( )
        {
            try
            {
                int __XOld = int.Parse( InputXSize.Text );
                int __YOld = int.Parse( InputYSize.Text );
                int __XNew = int.Parse( OutputXSize.Text );
                int __YNew = int.Parse( OutputYSize.Text );
                int __XPercent = ( int ) ( ( ( double ) __XNew / ( double ) __XOld ) * 100 );
                int __YPercent = ( int ) ( ( ( double ) __YNew / ( double ) __YOld ) * 100 );

                string __DrawXYPercent = "X: " + __XPercent + "   ,   Y: " + __YPercent;
                ResizePercentage.Text = __DrawXYPercent;
            }
            catch
            {

            }
        }

        private void SysMenuConverter_Load ( object sender , EventArgs e )
        {
            UpdatePercentSize( );
        }

        private void InputXSize_TextChanged ( object sender , EventArgs e )
        {
            UpdatePercentSize( );
        }

        private void InputYSize_TextChanged ( object sender , EventArgs e )
        {
            UpdatePercentSize( );
        }

        private void OutputXSize_TextChanged ( object sender , EventArgs e )
        {
            UpdatePercentSize( );
        }

        private void OutputYSize_TextChanged ( object sender , EventArgs e )
        {
            UpdatePercentSize( );
        }

        private bool AddNewIniFileToIniList ( string filepath )
        {
            bool needadd = true;
            foreach ( string _IniFilePath in IniFiles )
            {
                if ( _IniFilePath.ToLower( ) == filepath.ToLower( ) )
                    needadd = false;
            }
            if ( needadd )
            {

                IniFiles.Add( filepath );
            }
            return needadd;
        }

        private void GenerateNewIniFiles ( )
        {
            string _GetNewIni = @"\s*NewIni\s*=\s*(.+)\s*$";

            // Maximum 100 try count
            for ( int n = 0 ; n < 50 ; n++ )
            {
                for ( int x = 0 ; x < IniFiles.Count ; x++ )
                {
                    string _CurrentIniPath = Path.GetFullPath( IniFiles [ x ] );
                    string _IniDir = Path.GetDirectoryName( _CurrentIniPath );

                    string [ ] NewIniFileData = File.ReadAllLines( _CurrentIniPath );
                    for ( int i = 0 ; i < NewIniFileData.Length ; i++ )
                    {
                        Match _GetNewIniMatch = Regex.Match( NewIniFileData [ i ] , _GetNewIni );
                        if ( _GetNewIniMatch.Success )
                        {

                            string _IniFilePath = Path.GetFullPath( _IniDir + "\\" + _GetNewIniMatch.Groups [ 1 ].Value );
                            string __CurrentIniDir = Path.GetDirectoryName( _IniFilePath );
                            string __CurrentIniName = Path.GetFileName( _IniFilePath );

                            if ( File.Exists( _IniFilePath ) )
                            {
                                AddNewIniFileToIniList( __CurrentIniDir + "\\" + __CurrentIniName );
                            }
                        }
                    }
                }
            }

        }


        private void Process_All_IniFiles ( )
        {
            double __XPercent = 0;
            double __YPercent = 0;
            bool allokay = false;
            try
            {
                int __XOld = int.Parse( InputXSize.Text );
                int __YOld = int.Parse( InputYSize.Text );
                int __XNew = int.Parse( OutputXSize.Text );
                int __YNew = int.Parse( OutputYSize.Text );
                __XPercent = Convert.ToDouble( __XOld ) / Convert.ToDouble( __XNew );
                __YPercent = Convert.ToDouble( __YOld ) / Convert.ToDouble( __YNew );
                allokay = true;
            }
            catch
            {
                MessageBox.Show( "Ошибка в размерах, исправьте и повторите попытку." );
            }
            if ( allokay )
            {
                for ( int i = 0 ; i < IniFiles.Count ; i++ )
                {
                    string _CurrentIniPath = Path.GetFullPath( IniFiles [ i ] );
                    ProcessIniFile( _CurrentIniPath , __XPercent , __YPercent );
                }

                MessageBox.Show( "Готово." );
            }

        }

        private void button3_Click ( object sender , EventArgs e )
        {
            UpdatePercentSize( );

            if ( IniFiles != null )
            {
                IniFiles.Clear( );
            }
            IniFiles = new List<string>( );


            if ( BitmapReplacedFiles != null )
            {
                BitmapReplacedFiles.Clear( );
            }
            BitmapReplacedFiles = new List<string>( );


            if ( TextForReplace == null )
            {
                TextForReplace = new List<ReplaceTextSimpleStruct>( );
            }




            string SysInfoSettingsPath = InputIniDir.Text + "\\" + "SysInfoSettings.ini";
            string _GetNewIni = @"\s*NewIni\s*=\s*(.+)\s*$";

            if ( File.Exists( SysInfoSettingsPath ) )
            {
                string [ ] GeneralIniFiles = File.ReadAllLines( SysInfoSettingsPath );

                foreach ( string NewIniFile in GeneralIniFiles )
                {
                    string FirstIniPath = Path.GetFullPath( InputIniDir.Text + "\\" + NewIniFile.Replace( '/' , '\\' ) );
                    string _IniDir = Path.GetDirectoryName( FirstIniPath );
                    string _FirstIniFilename = Path.GetFileName( FirstIniPath );
                    FirstIniPath = _IniDir + "\\" + _FirstIniFilename;
                    if ( File.Exists( FirstIniPath ) )
                    {
                        AddNewIniFileToIniList( FirstIniPath );
                        string [ ] NewIniFileData = File.ReadAllLines( FirstIniPath );
                        for ( int i = 0 ; i < NewIniFileData.Length ; i++ )
                        {
                            Match _GetNewIniMatch = Regex.Match( NewIniFileData [ i ] , _GetNewIni );
                            if ( _GetNewIniMatch.Success )
                            {
                                string _IniFilePath = Path.GetFullPath( _IniDir + "\\" + _GetNewIniMatch.Groups [ 1 ].Value );
                                string __CurrentIniDir = Path.GetDirectoryName( _IniFilePath );
                                string __CurrentIniName = Path.GetFileName( _IniFilePath );
                                if ( File.Exists( _IniFilePath ) )
                                {
                                    AddNewIniFileToIniList( __CurrentIniDir + "\\" + __CurrentIniName );
                                }
                            }
                        }
                    }


                    GenerateNewIniFiles( );
                    Process_All_IniFiles( );
                }
            }
            else
                MessageBox.Show( "Не найден SysInfoSettings.ini файл" );

        }

        private void SelectIniDir_Click ( object sender , EventArgs e )
        {
            FolderBrowserDialog selectdir = new FolderBrowserDialog( );
            selectdir.Description = "Выберите путь к папке содержащей SysInfoSettings.ini";
            selectdir.ShowNewFolderButton = true;
            if ( selectdir.ShowDialog( ) == System.Windows.Forms.DialogResult.OK )
            {
                InputIniDir.Text = selectdir.SelectedPath;
            }
        }

        private void Bitmap16bit_CheckedChanged ( object sender , EventArgs e )
        {

        }

        private byte [ ] ImageToByte ( Image img )
        {
            if ( img == null )
                return new List<byte>( ).ToArray( );

            List<byte> byteArray = new List<byte>( );
            using ( MemoryStream stream = new MemoryStream( ) )
            {
                // img.RotateFlip( RotateFlipType.RotateNoneFlipY );
                //MessageBox.Show( "1" );
                img.Save( stream , System.Drawing.Imaging.ImageFormat.Bmp );
                // MessageBox.Show( "2" );
                byte [ ] outarray = stream.ToArray( );
                //MessageBox.Show( "3" );
                byteArray.AddRange( outarray );
            }
            try
            {
                byteArray.RemoveRange( 0 , 54 );
            }
            catch
            {

            }
            return byteArray.ToArray( );
        }

        private void label2_MouseEnter ( object sender , EventArgs e )
        {
            label2.Text = "Source resolution";
        }

        private void label2_MouseLeave ( object sender , EventArgs e )
        {
            label2.Text = "Исходное разрешение:";
        }

        private void label3_MouseEnter ( object sender , EventArgs e )
        {
            label3.Text = "Out resolution";
        }

        private void label3_MouseLeave ( object sender , EventArgs e )
        {
            label3.Text = "Выберите разрешение:";
        }

        private void ReplaceIniText_MouseEnter ( object sender , EventArgs e )
        {
            ReplaceIniText.Text = "Replace Ini Text";
        }

        private void ReplaceIniText_DragLeave ( object sender , EventArgs e )
        {
            ReplaceIniText.Text = "Заменить текст в INI";
        }

        private void button3_MouseEnter ( object sender , EventArgs e )
        {
            button3.Text = "RESIZE!";
        }

        private void button3_MouseLeave ( object sender , EventArgs e )
        {
            button3.Text = "Выполнить!";
        }

        private void SelectIniDir_MouseEnter ( object sender , EventArgs e )
        {
            SelectIniDir.Text = "Open...";
        }

        private void SelectIniDir_MouseLeave ( object sender , EventArgs e )
        {
            SelectIniDir.Text = "Обзор...";
        }

        private void CreateReplaceList_MouseEnter ( object sender , EventArgs e )
        {
            CreateReplaceList.Text = "Create replace list...";
        }

        private void CreateReplaceList_MouseLeave ( object sender , EventArgs e )
        {
            CreateReplaceList.Text = "Создать список строк для замены.";
        }

        private void CreateReplaceList_Click ( object sender , EventArgs e )
        {
            if ( TextForReplace == null )
                TextForReplace = new List<ReplaceTextSimpleStruct>( );
            TextToReplace _GetNewReplaceData = new TextToReplace( TextForReplace.ToArray() );
            _GetNewReplaceData.ShowDialog( );
            TextForReplace = _GetNewReplaceData._CurReplData;
            _GetNewReplaceData.Dispose( );
        }
    }

}


