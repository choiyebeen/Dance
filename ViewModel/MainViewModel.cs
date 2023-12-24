using Dance.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using System.Windows.Input;

namespace Dance.ViewModel
{
    public class MainViewModel : ViewModelBase 
    {
        string m_file_path;
        Microsoft.Office.Interop.Excel.Application m_excel_app;
        Workbook m_workbook;
        Worksheet m_worksheet;
        int m_row_count;
        int m_column_count;

        Dictionary<string, List<string>> m_dic;
        Dictionary<string, List<string>> m_selected_right_listbox;
        List<string> m_song;

        public string Song1 { get; set; }
        public string Song1People { get; set; }
        public string Song2 { get; set; }
        public string Song2People { get; set; }
        public string Song3 { get; set; }
        public string Song3People { get; set; }
        public string Song4 { get; set; }
        public string Song4People { get; set; }
        public string Song5 { get; set; }
        public string Song5People { get; set; }
        public string Song6 { get; set; }
        public string Song6People { get; set; }
        public string Song7 { get; set; }
        public string Song7People { get; set; }
        public string Song8 { get; set; }
        public string Song8People { get; set; }
        public string Song9 { get; set; }
        public string Song9People { get; set; }
        public string Song10 { get; set; }
        public string Song10People { get; set; }
        public string Song11 { get; set; }
        public string Song11People { get; set; }
        public string Song12 { get; set; }
        public string Song12People { get; set; }
        public string Song13 { get; set; }
        public string Song13People { get; set; }
        public string Song14 { get; set; }
        public string Song14People { get; set; }
        public string Song15 { get; set; }
        public string Song15People { get; set; }


        public void InitExcel()
        {
            // 초기 엑셀 세팅
            m_file_path = "C:\\Dance\\표.xlsx";
            m_excel_app = new Microsoft.Office.Interop.Excel.Application();
            m_workbook = m_excel_app.Workbooks.Open(m_file_path); //열기
            m_worksheet = m_workbook.Sheets[1]; //첫 번째 시트
            Range range = m_worksheet.UsedRange;
            m_row_count = range.Rows.Count; //행
            m_column_count = range.Columns.Count; //영

            //노래 이름 담기
            for (int i = 1; i <= m_column_count; i++)
            {
                if (range.Cells[1, i] != null && range.Cells[1, i].Value2 != null) //첫번째 행에 있는 노래 가져옴
                {
                    string song_name = range.Cells[1, i].Value2.ToString();
                    m_song.Add(song_name);
                }
            }
            //사람이름 담고 각 사람이 어떤 노래를 가지고 있는지 "1"로 구분
            for (int i = 2; i <= m_row_count; i++)
            {
                // 사람 이름 담기
                if (range.Cells[i, 1] != null && range.Cells[i, 1].Value2 != null)
                {
                    string name = range.Cells[i, 1].Value2.ToString();
                    if (!m_dic.ContainsKey(name))
                    {
                        m_dic[name] = new List<string>();
                    }

                    // 각 사람별로 보유한 곡을 추가
                    for (int j = 2; j <= m_column_count; j++)
                    {
                        if (range.Cells[i, j] != null && range.Cells[i, j].Value2 != null && range.Cells[i, j].Value2.ToString() == "1")
                        {
                            string song_name = m_song[j - 2];                // 곡명 가져오기
                            m_dic[name].Add(song_name);
                        }
                    }
                }
            }

            // 엑셀 닫기
            m_workbook.Close(false);
            m_excel_app.Quit();

        }

        public MainViewModel()
        {
            m_dic = new Dictionary<string, List<string>>(); 
            m_song = new List<string>();
            m_selected_right_listbox = new Dictionary<string, List<string>>();


            CompleteSelectionCommand = new ViewModelCommand(ExecuteCompleteSelectionCommand);  // 선택 버튼

            InitExcel();
        }

        //왼쪽 박스에 들어갈 이름
        public List<string> Names
        {
            get { return m_dic.Keys.ToList(); }
        }

        // 오른쪽 박스에 들어갈 이름
        public List<string> SelectNames
        {
            get { return m_selected_right_listbox.Keys.ToList(); }
        }

        // 선택버튼
        public ICommand CompleteSelectionCommand { get; set; }

        private void ExecuteCompleteSelectionCommand(object obj)
        {
            // 노래명과 이름들을 저장할 딕셔너리
            Dictionary<string, List<string>> songAndMembers = new Dictionary<string, List<string>>();
            List<string> m_complete_song_list = new List<string>();
            List<string> m_complete_song_people_list = new List<string>();

            // m_dic에 남아있는 모든 노래명과 해당 노래를 가진 사람 이름
            foreach (var entry in m_dic)
            {
                foreach (var song in entry.Value)
                {
                    if (!songAndMembers.ContainsKey(song))
                    {
                        songAndMembers[song] = new List<string>();
                    }
                    songAndMembers[song].Add(entry.Key);
                }
            }

            while (songAndMembers.Any())
            {
                var topSong = songAndMembers.OrderByDescending(x => x.Value.Count).FirstOrDefault();
                var peopleInTopSong = new List<string>(topSong.Value);
                m_complete_song_list.Add(topSong.Key);
                m_complete_song_people_list.Add(string.Join(" ", topSong.Value));

                //중복 제외시 
                //foreach (var person in peopleInTopSong)
               // {
                  //  foreach (var song in songAndMembers.Keys.ToList())
                  //  {
                        //songAndMembers[song].Remove(person);
                  //  }
               // }

                // 곡명 제거
                songAndMembers.Remove(topSong.Key);

                // 노래가 더 이상 없는 경우 리스트에서 해당 노래명 제거
                songAndMembers = songAndMembers.Where(kvp => kvp.Value.Count > 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }

            Song1 = m_complete_song_list.ElementAtOrDefault(0);
            Song2 = m_complete_song_list.ElementAtOrDefault(1);
            Song3 = m_complete_song_list.ElementAtOrDefault(2);
            Song4 = m_complete_song_list.ElementAtOrDefault(3);
            Song5 = m_complete_song_list.ElementAtOrDefault(4);
            Song6 = m_complete_song_list.ElementAtOrDefault(5);
            Song7 = m_complete_song_list.ElementAtOrDefault(6);
            Song8 = m_complete_song_list.ElementAtOrDefault(7);
            Song9 = m_complete_song_list.ElementAtOrDefault(8);
            Song10 = m_complete_song_list.ElementAtOrDefault(9);
            Song11 = m_complete_song_list.ElementAtOrDefault(10);
            Song12 = m_complete_song_list.ElementAtOrDefault(11);
            Song13= m_complete_song_list.ElementAtOrDefault(12);
            Song14 = m_complete_song_list.ElementAtOrDefault(13);
            Song15 = m_complete_song_list.ElementAtOrDefault(14);

            Song1People = m_complete_song_people_list.ElementAtOrDefault(0);
            Song2People = m_complete_song_people_list.ElementAtOrDefault(1);
            Song3People = m_complete_song_people_list.ElementAtOrDefault(2);
            Song4People = m_complete_song_people_list.ElementAtOrDefault(3);
            Song5People = m_complete_song_people_list.ElementAtOrDefault(4);
            Song6People = m_complete_song_people_list.ElementAtOrDefault(5);
            Song7People = m_complete_song_people_list.ElementAtOrDefault(6);
            Song8People = m_complete_song_people_list.ElementAtOrDefault(7);
            Song9People = m_complete_song_people_list.ElementAtOrDefault(8);
            Song10People = m_complete_song_people_list.ElementAtOrDefault(9);
            Song11People = m_complete_song_people_list.ElementAtOrDefault(10);
            Song12People = m_complete_song_people_list.ElementAtOrDefault(11);
            Song13People = m_complete_song_people_list.ElementAtOrDefault(12);
            Song14People = m_complete_song_people_list.ElementAtOrDefault(13);
            Song15People = m_complete_song_people_list.ElementAtOrDefault(14);

            OnPropertyChanged(nameof(Song1));
            OnPropertyChanged(nameof(Song1People));
            OnPropertyChanged(nameof(Song2));
            OnPropertyChanged(nameof(Song2People));
            OnPropertyChanged(nameof(Song3));
            OnPropertyChanged(nameof(Song3People));
            OnPropertyChanged(nameof(Song4));
            OnPropertyChanged(nameof(Song4People));
            OnPropertyChanged(nameof(Song5));
            OnPropertyChanged(nameof(Song5People));
            OnPropertyChanged(nameof(Song6));
            OnPropertyChanged(nameof(Song6People));
            OnPropertyChanged(nameof(Song7));
            OnPropertyChanged(nameof(Song7People));
            OnPropertyChanged(nameof(Song8));
            OnPropertyChanged(nameof(Song8People));
            OnPropertyChanged(nameof(Song9));
            OnPropertyChanged(nameof(Song9People));
            OnPropertyChanged(nameof(Song10));
            OnPropertyChanged(nameof(Song10People));
            OnPropertyChanged(nameof(Song11));
            OnPropertyChanged(nameof(Song11People));
            OnPropertyChanged(nameof(Song12));
            OnPropertyChanged(nameof(Song12People));
            OnPropertyChanged(nameof(Song13));
            OnPropertyChanged(nameof(Song13People));
            OnPropertyChanged(nameof(Song14));
            OnPropertyChanged(nameof(Song14People));
            OnPropertyChanged(nameof(Song15));
            OnPropertyChanged(nameof(Song15People));
        }

        public void LeftToRight(List<string> selectedItems)
        {
            foreach (string item in selectedItems)
            {
                if (m_dic.ContainsKey(item))
                {
                    m_selected_right_listbox[item] = m_dic[item];
                    m_dic.Remove(item);
                }
            }
            OnPropertyChanged(nameof(Names));
            OnPropertyChanged(nameof(SelectNames));
        }

        public void RightToLeft(List<string> selectedItems)
        {
            foreach (string item in selectedItems)
            {
                if (m_selected_right_listbox.ContainsKey(item))
                {
                    m_dic[item] = m_selected_right_listbox[item];
                    m_selected_right_listbox.Remove(item);
                }
            }
            OnPropertyChanged(nameof(Names));
            OnPropertyChanged(nameof(SelectNames));
        }

    }
}
