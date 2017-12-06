using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveTest
{
    public class DialogWindowViewModel : ReactiveObject
    {
        private ReactiveList<int> _numbers;
        public ReactiveList<int> Numbers
        {
            get { return _numbers; }
            set { this.RaiseAndSetIfChanged(ref _numbers, value); }
        }

        private int _selectedNumber;
        public int SelectedNumber
        {
            get { return _selectedNumber; }
            set { this.RaiseAndSetIfChanged(ref _selectedNumber, value); }
        }

        private bool? _dialogResult;
        public bool? DialogResult
        {
            get { return _dialogResult; }
            set { this.RaiseAndSetIfChanged(ref _dialogResult, value); }
        }

        public ReactiveCommand Close { get; set; }

        public DialogWindowViewModel()
        {
            Numbers = new ReactiveList<int>();
            for (int i = 0; i <= 10; i++)
                Numbers.Add(i);

            Close = ReactiveCommand.Create(() =>
            {
                this.DialogResult = true;
            });
        }
    }
}
