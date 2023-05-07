using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wLib.DB;

namespace wLib.UI
{
    public partial class TitlePanel : UserControl , INotifyPropertyChanged
    {
        AREA_CODE_T area_code = new AREA_CODE_T();

        private string _areaCode = "255";
        public string AreaCode
        {
            get => _areaCode;
            set
            {
                _areaCode = value;
                AreaName = area_code[value];
            }
        }

        private string _areaName = "ㅇㅇ시";
        public string AreaName 
        {
            get => _areaName;
            set
            {
                _areaName = value;
                OnNotifyPropertyChanged();
            }
        }

        private string _systemName = "(ㅇㅇㅇ시스템)";
        public string SystemName 
        {
            get => _systemName;
            set
            {
                _systemName = value;
                OnNotifyPropertyChanged();
            }
        }

        private string _programName = "ㅇㅇ프로그램";
        public string ProgramName
        {
            get => _programName;
            set
            {
                _programName = value;
                OnNotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnNotifyPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public TitlePanel()
        {
            InitializeComponent();

            bindingSource.DataSource = this;
        }
    }
}
