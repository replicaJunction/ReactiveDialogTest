using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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

namespace ReactiveTest
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window, IViewFor<DialogWindowViewModel>
    {
        public DialogWindow()
        {
            InitializeComponent();
            this.ViewModel = new DialogWindowViewModel();
            this.DataContext = this.ViewModel;

            this.ViewModel
                .WhenAnyValue(x => x.DialogResult)
                .Where(x => null != x)
                .Subscribe(val =>
                {
                    this.DialogResult = val;
                    this.Close();
                });
        }

        public DialogWindowViewModel ViewModel { get; set; }
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (DialogWindowViewModel)value;
        }
    }
}
