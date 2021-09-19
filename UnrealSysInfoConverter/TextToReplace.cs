using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UnrealSysInfoConverter
{
    public partial class TextToReplace : Form
    {

        public List<SysMenuConverter.ReplaceTextSimpleStruct> _CurReplData = null;


        public TextToReplace ( SysMenuConverter.ReplaceTextSimpleStruct [ ] replacedata )
        {
            InitializeComponent( );
            _CurReplData = new List<SysMenuConverter.ReplaceTextSimpleStruct>( );
            if ( replacedata.Length > 0 )
            {
                _CurReplData.AddRange( replacedata );

                foreach( SysMenuConverter.ReplaceTextSimpleStruct _CurRepl in replacedata )
                {
                    TextReplacerData1.Items.Add( _CurRepl.OLD );
                    TextReplacerData2.Items.Add( _CurRepl.NEW );
                }

            }
        }

        private void label1_MouseEnter ( object sender , EventArgs e )
        {
            label1.Text = "Enter text and press \"ADD\":";
        }

        private void label1_MouseLeave ( object sender , EventArgs e )
        {
            label1.Text = "Введите текс и нажмите \"Добавить\":";
        }

        private void AddToReplaceList_MouseEnter ( object sender , EventArgs e )
        {
            AddToReplaceList.Text = "ADD";
        }

        private void AddToReplaceList_MouseLeave ( object sender , EventArgs e )
        {
            AddToReplaceList.Text = "Добавить в список для замены.";
        }

        private void RemoveSelectedFromList_MouseEnter ( object sender , EventArgs e )
        {
            RemoveSelectedFromList.Text = "Remove selected.";
        }

        private void RemoveSelectedFromList_MouseLeave ( object sender , EventArgs e )
        {
            RemoveSelectedFromList.Text = "Удалить из списка.";
        }

        private void GoToMainMenu_MouseEnter ( object sender , EventArgs e )
        {
            GoToMainMenu.Text = "OK";
        }

        private void GoToMainMenu_MouseLeave ( object sender , EventArgs e )
        {
            GoToMainMenu.Text = "Закрыть";
        }

        private void label2_MouseEnter ( object sender , EventArgs e )
        {
            label2.Text = "TO:";
        }

        private void label2_MouseLeave ( object sender , EventArgs e )
        {
            label2.Text = "В:";
        }

        private void RemoveSelectedFromList_Click ( object sender , EventArgs e )
        {
            if (TextReplacerData1.SelectedIndex > -1 && TextReplacerData2.SelectedIndex > -1 )
            {
                if ( TextReplacerData1.SelectedIndex == TextReplacerData2.SelectedIndex )
                {
                    int selectedid = TextReplacerData1.SelectedIndex;

                    TextReplacerData1.Items.RemoveAt( selectedid );
                    TextReplacerData2.Items.RemoveAt( selectedid );
                    _CurReplData.RemoveAt( selectedid );
                    return;
                }
            }
            MessageBox.Show( "Не выбран текст который нужно удалить\r\nВыберите в одном из списков." );
        }

        private void AddToReplaceList_Click ( object sender , EventArgs e )
        {
            TextReplacerData1.Items.Add( TextInputForReplace.Text );
            TextReplacerData2.Items.Add( TextOutputForReplace.Text );
            SysMenuConverter.ReplaceTextSimpleStruct _AddReplaceData = new SysMenuConverter.ReplaceTextSimpleStruct( );
            _AddReplaceData.OLD = TextInputForReplace.Text;
            _AddReplaceData.NEW = TextOutputForReplace.Text;
            _CurReplData.Add( _AddReplaceData );
        }

        private void GoToMainMenu_Click ( object sender , EventArgs e )
        {
            this.Close( );
        }
    }
}
