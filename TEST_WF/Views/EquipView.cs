using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEST_WF.Views
{
    public partial class EquipView : Form, IEquipView
    {
        string searchValue; 
        bool isEdit;
        bool isSuccessful;
        string message;

        string IEquipView.cd_dist_obsv { get => cddistobsvDataGridViewTextBoxColumn.HeaderText; set => cddistobsvDataGridViewTextBoxColumn.HeaderText = value; }
        string IEquipView.nm_dist_obsv { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IEquipView.lastDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IEquipView.lastStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IEquipView.data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IEquipView.searchValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IEquipView.isEdit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IEquipView.isSuccessful { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IEquipView.message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public EquipView()
        {
            InitializeComponent();
        }

        public void SetEquipListBindingSource(BindingSource bindingSource)
        {
            throw new NotImplementedException();
        }
    }

    public interface IEquipView
    {
        // Properties
        string cd_dist_obsv { get; set; }
        string nm_dist_obsv { get; set; }
        string lastDate { get; set; }
        string lastStatus { get; set; }
        string data { get; set; }

        string searchValue { get; set; }
        bool isEdit { get; set; }
        bool isSuccessful { get; set; }
        string message { get; set; }

        // Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        // Methods
        void SetEquipListBindingSource(BindingSource bindingSource);
        void Show();
    }
}
