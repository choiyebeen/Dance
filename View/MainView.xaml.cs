using Dance.ViewModel;
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
using System.Windows.Shapes;

namespace Dance.View
{
    /// <summary>
    /// MainView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = LeftListBox.SelectedItems.Cast<string>().ToList(); // LeftListBox.SelectedItems 가 비하인드 코드만 사용 가능
            var viewModel = DataContext as MainViewModel;
            viewModel?.LeftToRight(selectedItems); //LeftToRight가 뷰모델에 있어야 함
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = RightListBox.SelectedItems.Cast<string>().ToList();
            var viewModel = DataContext as MainViewModel;
            viewModel?.RightToLeft(selectedItems);
        }
    }
}
