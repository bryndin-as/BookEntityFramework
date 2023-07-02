using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindow => App.Services.GetRequiredService<MainWindowViewModel>();    
    }
}
