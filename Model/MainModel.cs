using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Model
{
    public class MainModel
    {
        public ObservableCollection<string> NameList { get; set; }
        public ObservableCollection<string> SingList { get; set; }
        public MainModel()
        {
            //MainModel()을 객체화하면 자동으로 사용될 생성자
            NameList = new ObservableCollection<string>();
            SingList = new ObservableCollection<string>();
        }
    }
}
