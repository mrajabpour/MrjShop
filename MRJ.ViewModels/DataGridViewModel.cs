using System.Collections.Generic;

namespace MRJ.ViewModels
{
    public class DataGridViewModel<T>
    {
        public List<T> Records { get; set; }
        public int TotalCount { get; set; }
    }
}
