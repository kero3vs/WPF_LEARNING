using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_DragAndDrop
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //ひとつのアイテムのみ選択可能
            List1.SelectionMode = SelectionMode.Single;

            List2.SelectionMode = SelectionMode.Single;

            for (int i = 0; i < 5; i++)
            {
                List1.Items.Add(i.ToString());
            }

            for (int i = 5; i < 10; i++)
            {
                List2.Items.Add(i.ToString());
            }

            //List2へのドロップを可能に
            List2.AllowDrop = true;

        }


        private void List1_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //MessageBox.Show("左ボタンが押されました！");
                ListBox lbx = (ListBox)sender;
                int itemIndex = lbx.SelectedIndex;
                if (itemIndex < 0) return;
                string itemText = (string) lbx.Items[itemIndex];

                DragDropEffects dde = DragDrop.DoDragDrop(lbx,itemText,DragDropEffects.All);
                if (dde == DragDropEffects.Move)
                {
                    lbx.Items.RemoveAt(itemIndex);
                }   
            
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
                MessageBox.Show("右ボタンが押されました！");
            }
        }

        private void List2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void List2_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
            {
                ListBox target = (ListBox)sender;
                string itemText = (string)e.Data.GetData(typeof(string));
                target.Items.Add(itemText);
            }
        }
    }
}
