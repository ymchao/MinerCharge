   M S B A M L   `   `   `  ���� M  I矿灯管理平台, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:MinerLampMangement.viewMinerLampMangement.view  *    $MinerLampMangement.view.QueryTimeout   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     7local%clr-namespace:MinerLampMangement.view       � Title$  超时下井统计��$	��650��$	��900�� � WindowStartupLocation$ CenterScreen=�.���  � RowDefinitions �� �� $	��13* ��� .+�� $��Center=�.+��� $,��&   超时阈值设定（小时）：  ��$��Center=��� -    ��Name$ TimeTextBox��$��80��   Textboxstyle#��[  �� -    ��Name$ 	SearchBtn��$��查询��$��2��  MyWpfButton#��[ $��10,0,0,0q�&   System.Windows.Controls.DataGrid  -     Name$ DataGrid��$��1��  DataGrid#��[   DataGridColumnHeader   ColumnHeaderStyle# [  �� AlternationCount$ 2��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 柜号��
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 1*   TextBlockstyle1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [    Binding �� *	GroupId+  $	 	柜门号��$
 1* # [  �� *
DeviceId+  $	 姓名��$
 2* # [  �� *Name+  $	 工号��$
 1* # [  �� *	JobNumber+  $	 部门��$
 2* # [  �� *
Department+  $	 班次��$
 1* # [  �� *Class+  $	 下井时间��$
 3* # [  �� *
StatusTime+  $	 超时总计��$
 3* # [  �� *DurationTime+�� -   $ ExportResultsBtn��$��导出Excel��$��2��#��[ 