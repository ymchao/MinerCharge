   M S B A M L   `   `   `  ����M  I矿灯管理平台, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:MinerLampMangement.viewMinerLampMangement.view  5   	   *    $MinerLampMangement.view.QueryTimeout   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:MinerLampMangement.view     5   	     � Title$  超时下井统计��5   	   $	��650��6   $	��900��6%    � WindowStartupLocation$ CenterScreen=�61    � Icon$ ../Image/QueryTimeout.png��6V   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      $	��13* �6   �� 5      .+�5   
   � $��Center=�6   .+�5      �� $,��&   超时阈值设定（小时）：  ��6   $��Center=�60   �� 5      -   5         ��Name$ TimeTextBox��5      $��80��6+      Textboxstyle#��[  66   6_   �� 5      -   5         ��Name$ 	SearchBtn��5      $��查询��6(   $��2��65     MyWpfButton#��[ 6B   $��10,0,0,0q�6g   6�   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DataGrid��5      $��1��6%     DataGrid#��[ 62     DataGridColumnHeader   ColumnHeaderStyle# [ 5       �� AlternationCount$ 2��5      	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 柜号��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 61     TextBlockstyle1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 5   1      Binding 5   1   �� *	GroupId+5   1     5      $
 	柜门号��6%   $ 1* 62   # [ 5   1    5   1   �� *
DeviceId+5   1     5       $
 姓名��6%   $ 2* 61   # [ 5"   1    5!   1   �� *Name+5"   1     5#      $
 工号��6%   $ 1* 61   # [ 5%   1    5$   1   �� *	JobNumber+5%   1     5&      $
 部门��6$   $ 2* 60   # [ 5(   1    5'   1   �� *
Department+5(   1     5)      $
 班次��6%   $ 1* 61   # [ 5+   1    5*   1   �� *Class+5+   1     5,      $
 下井时间��6%   $ 3* 63   # [ 5.   1    5-   1   �� *
StatusTime+5.   1     5/      $
 超时总计��6%   $ 3* 63   # [ 51   1    50   1   �� *DurationTime+51   1   53      �� 54   
   -   5        $ ExportResultsBtn��54      $��导出Excel��6+   $��2��6=   #��[ 6J   6�   55      56      