using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_WF.Models
{
    public class EquipModel
    {
        // Fields
        private string cd_dist_obsv;
        private string nm_dist_obsv;
        private string lastDate; 
        private string lastStatus;
        private string data;

        // Properties
        public string Cd_dist_obsv { get => cd_dist_obsv; set => cd_dist_obsv = value; }
        public string Nm_dist_obsv { get => nm_dist_obsv; set => nm_dist_obsv = value; }
        public string LastDate { get => lastDate; set => lastDate = value; }
        public string LastStatus { get => lastStatus; set => lastStatus = value; }
        public string Data { get => data; set => data = value; }
    }

    public interface IEpuipModel
    {
        IEnumerable<EquipModel> GetALL();
        IEnumerable<EquipModel> GetByValue(); // Search
    }
}
