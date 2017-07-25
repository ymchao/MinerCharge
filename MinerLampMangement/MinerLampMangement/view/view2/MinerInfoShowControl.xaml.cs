using System;
using System.Windows.Controls;
using MinerLampMangement.Model;
using MongoDB.Bson;


namespace MinerLampMangement.view.view2
{
    /// <summary>
    /// MinerInfoShowControl.xaml 的交互逻辑
    /// </summary>
    public partial class MinerInfoShowControl : UserControl
    {
        private ObjectId id;
        private string attendanceid;
        private int num;

        public MinerInfoShowControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置员工的信息到界面中
        /// </summary>
        /// <param name="minerInfo"></param>
        public void SetMinerInfoShow(MinerInfo minerInfo)
        {
            NameBox.Text = minerInfo.Name;
            SexBox.Text = minerInfo.Sex;
            BloodBox.Text = minerInfo.Blood;
            PoliticalStatusBox.Text = minerInfo.PoliticalStatus;
            NationalBox.Text = minerInfo.National;
            BornDateBox.Text = minerInfo.BornDate.ToString();
            MaritalStatusBox.Text = minerInfo.MaritalStatus;
            EducationBox.Text = minerInfo.Education;
            IdentityCardIdBox.Text = minerInfo.IdentityCardId;
            PhoneNumbox.Text = minerInfo.PhoneNum;
            NativePlaceBox.Text = minerInfo.NativePlace;
            AddressBox.Text = minerInfo.Address;
            OthersBox.Text = minerInfo.Others;
            //tm卡编号就是前两位为柜号，后三位为柜门号
            TmNumberBox.Text = (minerInfo.GroupId * 1000 + minerInfo.DeviceId).ToString();
            CabinetIdBox.Text = minerInfo.GroupId.ToString();
            DoorIdBox.Text = minerInfo.DeviceId.ToString();
            JobNumberBox.Text = minerInfo.JobNumber;
            DepartmentBox.Text = minerInfo.Department;
            PositionBox.Text = minerInfo.Position;
            ContractTypesBox.Text = minerInfo.ContractTypes;
            LampTypesBox.Text = minerInfo.LampTypes;
            ClassesBox.Text = minerInfo.Classes;
            JoinTimeBox.Text = minerInfo.JoinTime.ToString();
            EquipLampTimeBox.Text = minerInfo.EquipLampTime.ToString();
            LampChargingCountBox.Text = null;
            SelfRescuerIdBox.Text = minerInfo.SelfRescuerId;
            EquipSelfRescuerTypeBox.Text = minerInfo.EquipSelfRescuerTime.ToString();
            //把objectid和AttendanceInfoObjectId存起来
            id = minerInfo.Id;
        }

        /// <summary>
        /// 提取页面员工信息
        /// </summary>
        /// <returns></returns>
        public MinerInfo GetMinerInfo()
        {
            //防止将充电次数改成非int类型，这样改成非int类型不会报错，默认成0
            int.TryParse(LampChargingCountBox.Text.Trim(), out num);

            var minerInfo = new MinerInfo();
            minerInfo.Name = NameBox.Text.Trim();
            minerInfo.Sex = SexBox.Text;
            minerInfo.Blood = BloodBox.Text.Trim();
            minerInfo.PoliticalStatus = PoliticalStatusBox.Text;
            minerInfo.National = NationalBox.Text.Trim();
            minerInfo.BornDate = Convert.ToDateTime(BornDateBox.Text);
            minerInfo.MaritalStatus = MaritalStatusBox.Text;
            minerInfo.Education = EducationBox.Text.Trim();
            minerInfo.IdentityCardId = IdentityCardIdBox.Text.Trim();
            minerInfo.PhoneNum = PhoneNumbox.Text.Trim();
            minerInfo.NativePlace = NativePlaceBox.Text.Trim();
            minerInfo.Address = AddressBox.Text.Trim();
            minerInfo.Others = OthersBox.Text.Trim();
            minerInfo.GroupId = Convert.ToInt32(CabinetIdBox.Text.Trim());
            minerInfo.DeviceId = Convert.ToInt32(DoorIdBox.Text.Trim());
            minerInfo.JobNumber = JobNumberBox.Text.Trim();
            minerInfo.Department = DepartmentBox.Text.Trim();
            minerInfo.Position = PositionBox.Text.Trim();
            minerInfo.ContractTypes = ContractTypesBox.Text.Trim();
            minerInfo.LampTypes = LampTypesBox.Text.Trim();
            minerInfo.Classes = ClassesBox.Text;
            minerInfo.JoinTime = Convert.ToDateTime(JoinTimeBox.Text);
            minerInfo.EquipLampTime = Convert.ToDateTime(EquipLampTimeBox.Text);
            minerInfo.SelfRescuerId = SelfRescuerIdBox.Text.Trim();
            minerInfo.EquipSelfRescuerTime = Convert.ToDateTime(EquipSelfRescuerTypeBox.Text);
            minerInfo.Id = id;
            return minerInfo;
        }
    }
}