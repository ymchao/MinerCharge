namespace MinerLampMangement.Enum
{
    /// <summary>
    /// 充电柜  充电状态
    /// </summary>
    public enum MinerLampStatus
    {
        /// <summary>
        /// 未初始化
        /// </summary>
        UnInit = 0,

        /// <summary>
        /// 正在使用
        /// </summary>
        Using = 1,

        /// <summary>
        /// 正在充电
        /// </summary>
        Charging = 2,

        /// <summary>
        /// 已充满电
        /// </summary>
        Full = 3,

        /// <summary>
        /// 充电故障
        /// </summary>
        ChargeProblem = 4,

        /// <summary>
        /// 通信故障
        /// </summary>
        CommuncationProblem = 5,

        /// <summary>
        /// 多种故障
        /// </summary>
        MultiProblem = 6,

        //        /// <summary>
        //        /// 备用
        //        /// </summary>
        //        Spare = 4,
    }
}