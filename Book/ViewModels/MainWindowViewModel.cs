using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private string _title = "Главное окно программы";

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}
