using Dance.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.ViewModel
{
    public class MainViewModel : ViewModelBase 
    {
        MainModel m_main_model;

        public MainViewModel()
        {
            m_main_model = new MainModel();

            LoadTextFile();

        }

        public void LoadTextFile()
        {
            string name_File_path = "C:\\Dance\\이름.txt";
            string sing_File_path = "C:\\Dance\\노래.txt";

            if (File.Exists(name_File_path))
            {
                var lines = File.ReadAllLines(name_File_path);
                foreach(var one_line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(one_line))
                    {
                        NameList.Add(one_line);
                    }
                }
            }

            if (File.Exists(sing_File_path))
            {
                var lines = File.ReadAllLines(sing_File_path);
                foreach (var one_line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(one_line))
                    {
                        SingList.Add(one_line);
                    }
                }
            }

            // DataGrid에 열 갯수를 SingList와 같게, 행 갯수를 NameList와 같게 생성
            int rows = NameList.Count();
            int cloumns = SingList.Count();

        }


        public ObservableCollection<string> NameList
        {
            get => m_main_model.NameList;
            set
            {
                m_main_model.NameList = value;
                OnPropertyChanged(nameof(NameList));
            }
        }

        public ObservableCollection<string> SingList
        {
            get => m_main_model.SingList;
            set
            {
                m_main_model.SingList = value;
                OnPropertyChanged(nameof(SingList));
            }
        }


    }
}
