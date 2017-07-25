using System;
using System.Windows.Controls;

namespace MinerLampMangement.view.viewbean
{
    class DataGridFirstColum
    {
        /// <summary>
        /// 为datagrid首列添加id号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

    }
}
