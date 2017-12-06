using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReactiveTest
{
    public class MainWindowViewModel : ReactiveObject
    {
        public ReactiveCommand OpenDialog { get; protected set; }

        public Interaction<Unit, int> GetNumberFromDialog { get; protected set; }

        protected int _myNumber = -5000;
        public int MyNumber
        {
            get { return _myNumber; }
            set { this.RaiseAndSetIfChanged(ref _myNumber, value); }
        }

        public MainWindowViewModel()
        {
            GetNumberFromDialog = new Interaction<Unit, int>();
            GetNumberFromDialog.RegisterHandler(interaction =>
            {
                var vm = new DialogWindowViewModel();

                // Get a reference to the view for this VM
                var view = Locator.Current.GetService<IViewFor<DialogWindowViewModel>>();

                // Set its VM to our current reference
                view.ViewModel = vm;

                var window = view as Window;
                var dialogResult = window.ShowDialog();
                
                // At this point, vm.SelectedNumber is expected be the number the user selected -
                // but instead, it always evaluates to 0.
                
                if (true == dialogResult)
                    interaction.SetOutput(vm.SelectedNumber);
                else
                    interaction.SetOutput(-1);
            });

            OpenDialog = ReactiveCommand.Create(() =>
            {
                GetNumberFromDialog.Handle(Unit.Default)
                    .Where(retVal => -1 != retVal) // If the dialog did not return true, don't update
                    .Subscribe(retVal =>
                    {
                        this.MyNumber = retVal;
                    });
            });
        }

        public static void Bootstrap()
        {
            Locator.CurrentMutable.Register(() => new DialogWindow(), typeof(IViewFor<DialogWindowViewModel>));
        }
    }
}
